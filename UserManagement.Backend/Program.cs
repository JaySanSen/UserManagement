
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using UserManagement.Backend.Data;
using UserManagement.Backend.Interfaces;
using UserManagement.Backend.JWTToken;
using UserManagement.Backend.Services;

namespace UserManagement.Backend
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);
      Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().WriteTo.File("Logs/Log.txt", rollingInterval: RollingInterval.Minute).CreateLogger();
      builder.Services.AddDbContext<EmployeeDBContext>(options =>
      {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
      });
      builder.Services.AddControllers();
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserManagement.Backend", Version = "v1" });

    // Add security definition for Bearer token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
      builder.Services.AddScoped<IEmployeeAction, EmployeeAction>();
      builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
  };
});

      // Add JwtTokenService for dependency injection
      builder.Services.AddSingleton<JwtTokenService>(new JwtTokenService(
          builder.Configuration["Jwt:SecretKey"],
          builder.Configuration["Jwt:Issuer"],
          builder.Configuration["Jwt:Audience"]
      ));

      var app = builder.Build();

      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();
      app.UseAuthentication();

      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}
