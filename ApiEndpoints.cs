using System.Diagnostics;
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
        MapEquipment(app);
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
                .CountAsync(e => e.Workshop.ChiefId == uid && e.EqStatus == "в ремонте");
            var eqCountString = totalEqCount + " / " + brokeEqCount;

            //заявки
            var statuses = new[] { "создана",
			"назначена",
			"в работе",
			"ожидает материалов",
			"выполнена",
			"закрыта",
			"отменена" };
            var countActiveOrders = await db.WorkOrders
                .CountAsync(o => o.MasterId == uid && statuses.Contains(o.StatusWo));
            var countActiveOrdersLost = await db.WorkOrders
                .CountAsync(o => o.MasterId == uid && statuses.Contains(o.StatusWo) && o.DateEnd < DateOnly.FromDateTime(DateTime.UtcNow));

            var nowPlanOrders = await db.WorkOrders
                .CountAsync(o => o.DateStart == DateOnly.FromDateTime(DateTime.Now) && o.MasterId == uid);
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
                
        }).RequireAuthorization();

        g.MapGet("/active-orders-master", async (JwtService jwt, HttpContext ctx, AppDbContext db) =>
        {
            var uid = jwt.GetUserId(ctx.User);

            var orders = await db.WorkOrders
                .Where(x => x.MasterId == uid)
                .OrderByDescending(x => x.DateCreate)
                .Take(10)
                .Select(x => new TableOrderResponse(
                    x.Id.ToString(),
                    x.Eq.Name,
                    x.Priority == "высокий" ? ("rgb(255, 0, 0)") : null,
                    x.StatusWo,
                    x.StatusWo == "создана" ? ("rgb(17, 255, 0)") : null,
                    x.Brigade.Id + " " + x.Brigade.Title,
                    x.DateStart.ToString(),
                    x.Reason,
                    x.Master.Id + " " + x.Master.LastName
                ))
                .ToListAsync();

            return Api.Ok(orders);
        }).RequireAuthorization();

        g.MapGet("/upcoming-to", async (JwtService jwt, HttpContext ctx, AppDbContext db) =>
        {
            var uid = jwt.GetUserId(ctx.User);

            var tos = await db.SheduleTos.Where(x => x.Eq.Workshop.MasterId == uid)
                .Select(x => new ScheduleToResponse
                (
                    x.EqId,
                    x.Eq.Name,
                    x.To.Title,
                    x.Date.ToString(),
                    x.Date >= DateOnly.FromDateTime(DateTime.Now) ? "запланировано" : "просрочено",
                    x.Date >= DateOnly.FromDateTime(DateTime.Now) ? null : "rgb(255, 0, 0)"
                )).ToListAsync();

            return Api.Ok(tos);
        }).RequireAuthorization();


        g.MapGet("/history-to/{eqId:int}", async (int eqId, AppDbContext db, JwtService jwt, HttpContext ctx) =>
        {
            var uid = jwt.GetUserId(ctx.User);

            var equipment = await db.Equipment
                .Where(e => e.Id == eqId && e.Workshop.Master!.Id == uid)
                .Select(e => new { e.Id, e.Name })
                .FirstOrDefaultAsync();

            if (equipment is null)
                return Api.NotFound("Оборудовавние не найдено");

            var history = await db.SheduleTos
                .Where(x => x.EqId == eqId)
                .OrderByDescending(x => x.Date)
                .Select(x => new HistoryTo(
                    x.Id,
                    x.To.Title,
                    x.Date.ToString()
                ))
                .ToListAsync();

            return Api.Ok(new
            {
                eqId = equipment.Id,
                equipmentName = equipment.Name,
                history
            });
        }).RequireAuthorization();
    }
    static void MapEquipment(WebApplication app)
    {
        var g = app.MapGroup("/equipment").WithTags("Equipment");

        g.MapGet("/", async (string? eqStatus, string? modelStr,  HttpContext ctx, JwtService jwt, AppDbContext db) =>
        {
            var uid = jwt.GetUserId(ctx.User);

            // зелёный - в работе, жёлтый - в ремонте, оранжевый - в простое, серый - выведено

            var equipment = await db.Equipment.Where(x => x.Workshop.MasterId == uid)
                .Select(x => new
                {
                    x.InventNumber,
                    x.Name,
                    x.Model,
                    x.Manufacture,
                    x.DateStartWork,
                    x.EqStatus,
                    StatusColor = x.EqStatus == "в работе" ? "rgb(0, 255, 0)"
                                : x.EqStatus == "в ремонте" ? "rgb(255, 255, 0)"
                                : x.EqStatus == "в простое" ? "rgb(255, 100, 0)"
                                : x.EqStatus == "выведено" ? "rgb(102, 102, 102)"
                                : null
                    
                }).ToListAsync();

            if (!String.IsNullOrWhiteSpace(eqStatus))
            {
                equipment = equipment.Where(x => x.EqStatus == eqStatus).ToList();
            }
            if (!String.IsNullOrWhiteSpace(modelStr))
            {
                equipment = equipment.Where(x => x.Model!.ToLower().Contains(modelStr.ToLower())).ToList();
            }

            return Api.Ok(equipment);
        }).RequireAuthorization();

        g.MapGet("/work-orders/{eqId:Int}", async (int eqId, HttpContext ctx, JwtService jwt, AppDbContext db) =>
        {

            var orders = await db.WorkOrders
                .Where(x => x.EqId == eqId)
                .OrderByDescending(x => x.DateCreate)
                .Take(10)
                .Select(x => new TableOrderResponse(
                    x.Id.ToString(),
                    x.Eq.Name,
                    x.Priority == "high" ? ("rgb(255, 0, 0)") : null,
                    x.StatusWo,
                    x.StatusWo == "new" ? ("rgb(17, 255, 0)") : null,
                    x.Brigade.Id + " " + x.Brigade.Title,
                    x.DateStart.ToString(),
                    x.Reason,
                    x.Master.Id + " " + x.Master.LastName
                ))
                .ToListAsync();

            return Api.Ok(orders);
        }).RequireAuthorization();


        g.MapPost("/", async (EqRequest eq, AppDbContext db, HttpContext ctx, JwtService jwt) =>
        {
            var uid = jwt.GetUserId(ctx.User);

            var ws = await db.Workshops.Where(x => x.Title == eq.Workshop).FirstOrDefaultAsync();
            if (ws is null) return Api.NotFound("Склад не найден");

            var s = new[] {"в работе","в ремонте", "в простое","выведено"};
            if (!s.Contains(eq.Status)) return Api.NotFound("Статус не валиден");

            var e = new Equipment
            {
                InventNumber = eq.InvNumber,
                Name = eq.Name,
                Model = eq.Name,
                SerialNumber = eq.SerialNumber,
                DateStartExp = eq.ExpStart,
                DateStartWork = eq.WorkStart,
                Manufacture = eq.Name,
                WarrancyTime = eq.WarrancyDate,
                WorkshopId = ws!.Id,
                EqStatus = eq.Status
            };

            await db.Equipment.AddAsync(e);
            await db.SaveChangesAsync(); 
            
            return Api.Created(e, $"equipment/{e.Id}");
        }).RequireAuthorization();

        g.MapDelete("/{id:Int}", async (int id, HttpContext ctx, JwtService jwt, AppDbContext db) =>
        {
            var uid = jwt.GetUserId(ctx.User);

            var eq = await db.Equipment.Where(x => x.Workshop.MasterId == uid && x.Id == id).FirstOrDefaultAsync();

            if (eq is null) Api.NotFound("Оборудование не найдено");  

            var orders = await db.WorkOrders.CountAsync(x => x.EqId == id);
            if (orders != 0) Api.Conflict("Оборудование нельзя удалить, так как у него есть связанные заявки");

            db.Remove(eq);
            await db.SaveChangesAsync();

            return Api.Ok<object?>(null, "Удалено");
        }).RequireAuthorization();

        g.MapPut("/{id:Int}", async (int id, EqRequest req, JwtService jwt, AppDbContext db, HttpContext ctx) =>
        {
            var eq = await db.Equipment.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (eq == null) Api.NotFound("Оборудование не найдено");

            if (req.ExpStart != null) eq.DateStartExp = req.ExpStart;
            if (req.WorkStart != null) eq.DateStartWork = req.ExpStart;
            if (req.Model != null) eq.Model = req.Model;
            if (req.Name != null) eq.Name = req.Name;
            if (req.Manifacture != null) eq.Manufacture = req.Manifacture;
            if (req.SerialNumber != null) eq.SerialNumber = req.SerialNumber;
            if (req.Status != null) eq.EqStatus = req.Status;
            if (req.Workshop != null) eq.WorkshopId = await db.Workshops.Where(x => x.Title == req.Workshop)
                                                                .Select(x => x.Id)
                                                                .FirstOrDefaultAsync();

            await db.SaveChangesAsync();
            return Api.Ok(eq);
        }).RequireAuthorization();

    }
}
    


