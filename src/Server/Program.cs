using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; // Hinzugefügt
using VirtualReception.Server.Hubs;
using VirtualReception.Server.Infrastructure;
using VitualReception.Domain.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IChatRepository, FakeChatRepository>();

builder.Services.AddCors(opt => opt.AddDefaultPolicy(builder =>
{
    builder.SetIsOriginAllowed(_ => true);
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowCredentials();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // Korrektur
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("hubs/chat");
});

app.Run();
