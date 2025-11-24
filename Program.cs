    using Books_Auth.Repositories;
    using Books_Auth.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Security.Claims;
    using System.Text;
    using Books_Auth.Data;
    using Books_Auth.Repositories;
    using Books_Auth.Services;
    using DotNetEnv;


    var builder = WebApplication.CreateBuilder(args);

    // ---------- 1. Controllers ----------
    builder.Services.AddControllers();

    // ---------- 2. Swagger / OpenAPI ----------
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // ---------- 3. CORS ----------
    builder.Services.AddCors(opt =>
    {
        opt.AddPolicy("AllowAll", p => p
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
    });

    // ---------- 4. JWT desde appsettings.json ----------
    var jwt = builder.Configuration.GetSection("Jwt");
    var keyBytes = Encoding.UTF8.GetBytes(jwt["Key"]!);

    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwt["Issuer"],
                ValidAudience = jwt["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),

                RoleClaimType = ClaimTypes.Role,
                ClockSkew = TimeSpan.Zero
            };
        });

    // ---------- 5. Authorization ----------
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminOnly", p => p.RequireRole("Admin"));
    });

    // ---------- 6. Database ----------
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
    );

    // ---------- 7. Dependency Injection ----------
    // Auth
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();

    // Books
    builder.Services.AddScoped<IBookRepository, BookRepository>();
    builder.Services.AddScoped<IBookService, BookService>();

    // ---------- 8. Build ----------
    var app = builder.Build();

    // ---------- 9. Swagger ----------
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // ---------- 10. Middlewares ----------
    app.UseHttpsRedirection();
    app.UseCors("AllowAll");
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
