// Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var version = "0.0.3";
var builder = WebApplication.CreateBuilder(args);

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

app.Run($"http://*:8080");
