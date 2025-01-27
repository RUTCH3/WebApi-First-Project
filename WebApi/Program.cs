using WebApi.Managers;
using WebApi.Interfaces;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// הגדרת HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000); // HTTP
    options.ListenLocalhost(5001, listenOptions => {listenOptions.UseHttps();});
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services of Jewelry
builder.Services.AddSingleton<IJewelryService, JewelryService>();

var app = builder.Build();

app.UseErrorMiddleware();
app.UseLogMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
