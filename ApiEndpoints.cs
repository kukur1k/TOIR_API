using Microsoft.EntityFrameworkCore;
using TOIR_API.Data;
using TOIR_API.Models;
using TOIR_API.Services;
using BC = BCrypt.Net.BCrypt;

namespace TOIR_API;

public static class ApiEndpoints
{
    public static void MapAll(WebApplication app)
    {
        MapAuth(app);
        MapDashBoard(app);
    }


    static void MapAuth(WebApplication app)
    {
        var g = app.MapGroup("/auth").WithTags("Auth").RequireAuthorization();

        g.MapPost("/login", async (LoginRequest req, AppDbContext db, JwtService jwt) =>
        {
            var err = ValidationService.Check(ValidationService.Validate(req));
            if (err is not null) return err;

            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == req.Email);

            if (user is null || !BC.Verify(req.Password, user.PasswordHash)){
                return Api.Fail("Неверный Email или пароль", 401, "INVALID_CREDENTIALS");
            }

            var oldTokens = await db.RefreshTokens
                .Where(t => t.UserId == user.Id && t.IsRevoked != true)
                .ToListAsync();
            oldTokens.ForEach(t => t.IsRevoked = true);

            var refresh = await CreateRefreshToken(user.Id, db);
            return Api.Ok(new AuthResponce(
                jwt.Generate(user), refresh.Token, user.Role
            ));

        }).AllowAnonymous();

        g.MapPost("/register", async (RegisterRequest req, AppDbContext db, JwtService jwt) =>
        {
            var err = ValidationService.Check(ValidationService.Validate(req));
            if (err is not null) return err;

            var user = await db.Users
                .FirstOrDefaultAsync(u => u.Email == req.Email);

            if (user is not null)
                return Api.Fail("Данный Email уже зарегестрирован", 401, "INVALID_CREDENTIALS");

            var newUser = new User()
            {
                Email = req.Email,
                PasswordHash = BC.HashPassword(req.Password),
                FirstName = req.FirstName,
                LastName = req.LastName,
                Patronymic = req.MiddleName,
                Phone = req.Phone,
                Role = "master"
            };

            await db.Users.AddAsync(newUser);
            await db.SaveChangesAsync();
            var refresh = await CreateRefreshToken(newUser.Id, db);
            return Api.Ok(new AuthResponce(
                jwt.Generate(newUser), refresh.Token, newUser.Role
            ));
        }).AllowAnonymous();

        g.MapPost("/logout", async (RefreshRequest req, AppDbContext db) =>
        {
            if (string.IsNullOrWhiteSpace(req.RefreshToken))
                return Api.BadRequest("RefreshToken обязателен");

            var token = await db.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token == req.RefreshToken && t.IsRevoked != true);

            if (token is not null)
            {
                token.IsRevoked = true;
                await db.SaveChangesAsync();
            }
            return Api.Ok<object?>(null, "Выход выполнен");
        });

        g.MapPost("/refresh", async (RefreshRequest req, AppDbContext db, JwtService jwt) =>
        {
            if (string.IsNullOrWhiteSpace(req.RefreshToken))
                return Api.BadRequest("RefreshToken обязателен");

            var token = await db.RefreshTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Token == req.RefreshToken);

            if (token is null || token.IsRevoked == true || token.ExpiresAt < DateTimeOffset.UtcNow)
            {
                return Api.Fail("Токен недействителен или истёк", 401, "INVALID_REFRESH_TOKEN");
            }

            token.IsRevoked = true;
            var newRefresh = await CreateRefreshToken(token.UserId!.Value, db);

            return Api.Ok(new AuthResponce(
                jwt.Generate(token.User),
                newRefresh.Token,
                token.User.Role
            ));
        });
    }

    static async Task<RefreshToken> CreateRefreshToken(int userId, AppDbContext db)
    {
        var token = new RefreshToken
        {
            UserId = userId,
            Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()) +
                    Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
            ExpiresAt = DateTime.UtcNow.AddDays(30),
        };
        db.RefreshTokens.Add(token);
        await db.SaveChangesAsync();
        return token;
    }

    static void MapDashBoard(WebApplication app)
    {
        var g = app.MapGroup("/dashboard").WithTags("Dashboard").RequireAuthorization();

        g.MapGet("/kpi", async (AppDbContext db, JwtService jwt, HttpContext ctx) =>
        {
            var uid = jwt.GetUserId(ctx.User);

            //оборудование
            var totalEqCount = await db.Equipment
                .CountAsync(e => e.Workshop.MasterId == uid);
            var brokeEqCount = await db.Equipment
                .CountAsync(e => e.Workshop.ChiefId == uid && e.EqStatus == "in_repair");
            var eqCountString = totalEqCount + " / " + brokeEqCount;

            //заявки
            var statuses = new[] { "new", "in_pcocess", "complete" };
            var countActiveOrders = await db.WorkOrders
                .CountAsync(o => o.MasterId == uid && statuses.Contains(o.StatusWo));
            var countActiveOrdersLost = await db.WorkOrders
                .CountAsync(o => o.MasterId == uid && statuses.Contains(o.StatusWo) && o.DateEnd < DateTime.UtcNow);

            var nowPlanOrders = await db.WorkOrders
                .CountAsync(o => o.DateStart == DateTime.Now && o.MasterId == uid);
            var nowCreateOrders = await db.WorkOrders
                .CountAsync(o => o.DateCreate == DateTime.Now && o.MasterId == uid);

            return Api.Ok(new {
               totalEqCount,
               brokeEqCount,
               eqCountString,
               countActiveOrders,
               countActiveOrdersLost,
               nowPlanOrders,
               nowCreateOrders
        });
                
        });

    }
}



