using CMSApi.Application;
//using CMSApi.Application.DTO.AuthDto;
using CMSApi.Application.Interfaces;
using CMSApi.Application.Services;
using CMSApi.Application.Services.Email;
using CMSApi.AuthExtension;
using CMSApi.Domain.Entities;
using CMSApi.Infrastructure;
using CMSApi.Presentation.Controllers;
using CMSApi.Presentation.Controllers.Auth;
using Microsoft.AspNetCore.Identity;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwagger()
                .InjectDbContext(builder.Configuration)
                .AddAppConfig(builder.Configuration)
                .AddIdentityHandlers()
                .ConfigureIdentityOptions()
                .AddIdentityAuth(builder.Configuration);

builder.Services.AddTransient<IEmailService, EmailService>();
//builder.Services.AddTransient<WelcomeEmailJob>();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // log everything from Debug and above
    .WriteTo.Console()    // log to console
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // log to daily file
    .Enrich.FromLogContext() // add useful context like RequestId
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddScoped<IDeptService, DeptService>()
                .AddScoped<IEmployeeService, EmployeeService>()
                .AddScoped<IPasswordHasher<Employee>, PasswordHasher<Employee>>()
                //.AddScoped<IAuthService, AuthService>()
                .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddAutoMapper(typeof(Program));

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.ConfigureSwagger()
   .ConfigureCORS(builder.Configuration)
   .AddIdentityAuthMiddleware();

app.MapControllers();
app.MapGroup("/api");
//.MapIdentityApi<AppUser>();
//.WithTags("Auth");

app.MapGroup("/api")
   .MapIdentityUserEndpoints()
   .MapAuthorizationDemoEndpoints()
   .MapAccountEndpoints();

app.Run();
