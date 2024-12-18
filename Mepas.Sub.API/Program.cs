using Mepas.Sub.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Sql Baglantısı
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

builder.Services.AddScoped<ISubscriberRepository, SubscriberRepository>();  // Repository
builder.Services.AddScoped<ISubscriberService, SubscriberService>(); // Service

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


// API servislerini ekle
builder.Services.AddControllers();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<Mepas.Sub.API.Middleware.ExceptionMiddleware>();
app.MapControllers();

app.Run();
