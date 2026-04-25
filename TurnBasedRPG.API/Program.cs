using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using TurnBasedRPG.API.Services.CombatService;
using TurnBasedRPG.API.Services.EffectService;
using TurnBasedRPG.API.Services.GameService;
using TurnBasedRPG.API.Services.StatService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Services
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IStatService, StatService>();
builder.Services.AddScoped<IEffectService, EffectService>();
builder.Services.AddScoped<ICombatService, CombatService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization(); // If needed later
app.MapControllers();
app.Run();
