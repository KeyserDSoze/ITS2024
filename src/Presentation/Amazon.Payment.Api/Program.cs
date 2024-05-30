using Amazon.Authentication.Sql;
using Amazon.Authentication.Sql.Services;
using Amazon.Payment.Api.Middlewares;
using Amazon.Payment.Domain.Services;
using Amazon.Payment.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<ICartStorageService, CartStorageServiceWithSql>();
//builder.Services.AddSingleton<ICartStorageService, CartStorageService>();
builder.Services
    .AddSqlServer<PaymentDbContext>(builder.Configuration.GetConnectionString("Default"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthenticationStorage(builder.Configuration);
builder.Services.AddTransient<ApiKeyMiddleware>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();
    await dbContext.Database.MigrateAsync();
    var authenticationDbContext = scope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();
    await authenticationDbContext.Database.MigrateAsync();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();
