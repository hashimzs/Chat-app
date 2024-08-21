using Library;
using Library.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000") });
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<StompService>();
builder.Services.AddScoped<ChatStompService>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
