using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using VirtualReception.Client;
using VirtualReception.Client.Infrastructure;
using VitualReception.Domain.Model;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000/") });
builder.Services.AddSingleton<IChatRepository, HttpChatRepository>();

await builder.Build().RunAsync();
