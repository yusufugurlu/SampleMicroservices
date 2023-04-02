using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using SampleMicroservices.Services.Discount.Services;
using SampleMicroservices.Shared.Services;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var requiredAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); // need to be Authorization   Authorization olmus biri olmasi lazim

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub"); // we don't need convert sub to nameidentifier. If you can't do this. Later It might be harder.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["identityServerUrl"];
    options.Audience = "resource_discount";
    options.RequireHttpsMetadata = false;

});


builder.Services.AddControllers(opt => {
    opt.Filters.Add(new AuthorizeFilter(requiredAuthorizePolicy));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
