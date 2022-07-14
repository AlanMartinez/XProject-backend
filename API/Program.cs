using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using XProject.API.Middleware;
using XProject.Application.Interfaces;
using XProject.Application.Services.User;
using XProject.Core.Interfaces;
using XProject.Infrastructure;
using XProject.Infrastructure.Filters;
using XProject.Infrastructure.Interfaces;
using XProject.Infrastructure.Options;
using XProject.Infrastructure.Repositories;
using XProject.Infrastructure.Services;

public class Program
{
    static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers(options =>
            options.Filters.Add<GlobalExceptionFilter>()
        ).AddNewtonsoftJson(options =>
            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
             );
        ;
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Inser token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type= ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
            });
        });
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });


        builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //services
        builder.Services.AddTransient(typeof(IAuthorizationService), typeof(AuthorizationService));
        builder.Services.AddTransient(typeof(IUserService), typeof(UserService));

        //
        builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        builder.Services.AddScoped(typeof(ISecurityRepository), typeof(SecurityRepository));
        builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        builder.Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
        builder.Services.AddSingleton<IPasswordService, PasswordService>();

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.Configure<PasswordOptions>(builder.Configuration.GetSection("PasswordOptions"));
        builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection("Authentication"));


        builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL")));

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                //ValidateIssuerSigningKey = true,
                //ValidIssuer = builder.Configuration.GetSection("Authentication:Issuer").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Authentication:SecretKey").Value))
            };
        });



        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("CorsPolicy");

        app.UseMiddleware<LoggerMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}