using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SampleMicroservices.Services.Basket.Services;
using SampleMicroservices.Services.Basket.Settings;
using SampleMicroservices.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var requiredAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); // need to be Authorization   Authorization olmus biri olmasi lazim

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["identityServerUrl"];
    options.Audience = "resource_catalog";
    options.RequireHttpsMetadata = false;

});


builder.Services.AddControllers(opt => {
    opt.Filters.Add(new  AuthorizeFilter(requiredAuthorizePolicy));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddHttpContextAccessor();
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings")); // options pattern
builder.Services.AddScoped<ISharedIdentityService,SharedIdentityService>();
builder.Services.AddScoped<IBasketService, BasketService>();


builder.Services.AddSingleton<RedisService>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
    var redis = new RedisService(redisSettings.Host, redisSettings.Port);
    redis.Connect();

    return redis;
});//Connection redis 


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
