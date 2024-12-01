using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApi.DBOperations;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

 

builder.Services.AddCors();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token: Issuer"],
        ValidAudience = builder.Configuration["Token: Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero

    };
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
});

builder.Services.AddDbContext<BookStoreDBContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi"));
}

app.UseAuthentication();
//app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseCustomExceptionMiddle();

using (var scope = app.Services.CreateScope())
{
    var sevices = scope.ServiceProvider;
    DataGenerator.Initialize(sevices);
}

    app.MapControllers();

app.Run();

