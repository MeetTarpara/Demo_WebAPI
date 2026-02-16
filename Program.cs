using DemoApi.Data;
using DemoApi.Middlewares;
using DemoApi.Repositories;
using DemoApi.Services;
using DemoApi.Validators;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

//JWT Token
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//Fluent Validation
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<StudentValidator>());


//Controllers
builder.Services.AddControllers();

//Dependency Injection
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<EmployeeService>();


// Register services with different lifetimes
builder.Services.AddTransient<TransientGuidService>();
builder.Services.AddScoped<ScopedGuidService>();
builder.Services.AddSingleton<SingletonGuidService>();


//API should allow Angular app that runs on PORT::4200
//for Angular connection
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


// DB
builder.Services.AddDbContext<CompanyDBContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));


//Jwt Token
var key = builder.Configuration["Jwt:Key"]
          ?? throw new Exception("JWT Key not found in appsettings.json");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "MyWebApi",
        ValidAudience = "MyAngularApp",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});



var app = builder.Build();
app.UseCors("AllowAngular");


//Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Middleware
// app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

//JWT - Authentication and authorization 
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
