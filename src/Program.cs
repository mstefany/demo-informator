// Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var version = "0.0.1";
var builder = WebApplication.CreateBuilder(args);

// Get port from environment variable or use default
var port = Environment.GetEnvironmentVariable("LISTEN_PORT");
if (!int.TryParse(port, out int parsedPort))
{
    parsedPort = 8080; // Default to 8080 if not set or invalid
}

if (parsedPort < 1024)
{
    Console.WriteLine("Error: Port number must be 1024 or higher.");
    return;
}
// Add services to the container
builder.Services.AddControllers();

var app = builder.Build();

// Info endpoint
app.MapGet("/info", () => new
{
    CurrentTime = DateTime.UtcNow.ToString("o"),
    Hostname = Environment.MachineName,
    MyNodeName = Environment.GetEnvironmentVariable("MY_NODE_NAME"),
    MyPodName = Environment.GetEnvironmentVariable("MY_POD_NAME"),
    MyPodNamespace = Environment.GetEnvironmentVariable("MY_POD_NAMESPACE"),
    MyPodIp = Environment.GetEnvironmentVariable("MY_POD_IP"),
    MyPodServiceAccount = Environment.GetEnvironmentVariable("MY_POD_SERVICE_ACCOUNT")
});

// Version endpoint
app.MapGet("/version", () => new
{
    AppVersion = version
});

app.Run($"http://*:{parsedPort}");
