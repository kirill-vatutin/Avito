var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Получаем токен из конфигурации
var botToken = builder.Configuration["TelegramBot:Token"];
builder.Services.AddSingleton(sp => new TelegramBotService(botToken));

builder.Services.AddHostedService<RabbitMqBackgroundService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
