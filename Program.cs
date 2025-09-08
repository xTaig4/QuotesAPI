using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using QuoteAPI;
using QuoteAPI.Data;
using QuoteAPI.Services;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;


var builder = WebApplication.CreateBuilder(args);
var myOptions = new RateLimitSettings();

// Add services to the container.
builder.Services.AddDbContext<QuoteContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AppSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            builder.Configuration.GetValue<string>("AppSettings:Token")
        )),
        ValidateIssuerSigningKey = true,
    });

builder.Services.AddScoped<IAuthService, AuthService>();

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
builder.Services.AddAuthorization();
builder.Services.AddOpenApi();


var app = builder.Build();

app.MapControllers().RequireRateLimiting("fixed");
app.UseCors("AllowAll");

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<QuoteContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
