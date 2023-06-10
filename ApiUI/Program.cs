using Bussiness.Abstract;
using Bussiness.Consrete;
using Core.Validation;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entity.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();
RegistrateTypes();
RegistrateJwt();
RegistrateDbContext();
var assembly = Assembly.GetExecutingAssembly();
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
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

void RegistrateTypes()
{
    builder.Services.AddScoped<IValidator<EmployeeDTO>, EmployeeValidator>();
    builder.Services.AddScoped<IValidator<DepartmentDTO>, DepartmentValidator>();
    builder.Services.AddScoped<IEmployeeService, EmployeeManager>();
    builder.Services.AddScoped<IDepartmentService, DepartmentManager>();
    builder.Services.AddScoped<IEmployeeDAL, EmployeeDAL>();
    builder.Services.AddScoped<IDepartmentDAL, DepartmentDAL>();
}

void RegistrateDbContext()
{
    builder.Services.AddDbContext<MSSQLDBContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLDBContextConnectionString"));
    });
}

void RegistrateJwt()
{
    var key = builder.Configuration.GetValue<string>("JWT:Key");
    builder.Services.AddAuthentication(w =>
    {
        w.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        w.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(w =>
    {
        w.RequireHttpsMetadata = false;
        w.SaveToken = true;
        w.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
    builder.Services.AddSingleton<JWTAuthenticationManager>(new JWTAuthenticationManager(key));
}

