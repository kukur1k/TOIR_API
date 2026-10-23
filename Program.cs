using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TOIR_API;
using TOIR_API.Data;
using TOIR_API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(o => 
    o.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo {Title = "TOIR_API", Version = "1.0"});

    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat="JWT"
    });

    //понизил версию OpenApi до 1.6 чтобы юзать .Models и swashBuckle до 6.4.0
    s.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
        },
        new List<string>()
        }
    });
});  


builder.Services.AddCors(x =>
    x.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Configuration.GetValue<bool>("EnableSwagger"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/auth").MapIdentityApi<AppUser>();

ApiEndpoints.MapAll(app);

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

  
app.Run();
