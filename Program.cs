using DemoApi.Data;
using DemoApi.Middlewares;
using DemoApi.Repositories;
using DemoApi.Services;
using DemoApi.Validators;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

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
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.Run();
