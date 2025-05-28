using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
   rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
   {
       options.PermitLimit = 5; // Allow 5 requests
       options.Window = TimeSpan.FromSeconds(30); // Per 30 seconds
       //options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
       //options.QueueLimit = 2; // Allow up to 2 requests in the queue
   });
});

var app = builder.Build();

app.UseRateLimiter(); // Apply rate limiting middleware

// Configure the HTTP request pipeline.
app.MapReverseProxy();

app.Run();
