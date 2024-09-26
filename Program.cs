using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using QuoteAPI;
using QuoteAPI.Data;
using System.Threading.RateLimiting;


var builder = WebApplication.CreateBuilder(args);
var myOptions = new RateLimitSettings();

// Add services to the container.
builder.Services.AddDbContext<QuoteContext>();
builder.Configuration.GetSection(RateLimitSettings.MyRateLimit).Bind(myOptions);

builder.Services.AddRateLimiter(_ => _.AddFixedWindowLimiter(policyName: "fixed", options =>{
    options.PermitLimit = 4; 
    options.Window = TimeSpan.FromSeconds(12); 
    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; 
    options.QueueLimit = 2; 
}));

builder.Services.AddRateLimiter(_ => _.AddSlidingWindowLimiter(policyName: "sliding", options => { 
    options.PermitLimit = myOptions.PermitLimit; 
    options.Window = TimeSpan.FromSeconds(myOptions.Window); 
    options.SegmentsPerWindow = myOptions.SegmentsPerWindow; 
    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; 
    options.QueueLimit = myOptions.QueueLimit; }));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name:"AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers().RequireRateLimiting("fixed");

// Use CORS
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
