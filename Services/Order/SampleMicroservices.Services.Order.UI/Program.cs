using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using SampleMicroservices.Services.Order.Instracture;
using SampleMicroservices.Shared.Services;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var requiredAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); // need to be Authorization   Authorization olmus biri olmasi lazim

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub"); // we don't need convert sub to nameidentifier. If you can't do this. Later It might be harder.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["identityServerUrl"];
    options.Audience = "resource_order";
    options.RequireHttpsMetadata = false;

});

builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure => {

        configure.MigrationsAssembly("SampleMicroservices.Services.Order.Instracture");
    });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddMediatR(typeof(SampleMicroservices.Services.Order.Application.Handler.CreateOrderCommandHandler).Assembly);


builder.Services.AddControllers(opt => {
    opt.Filters.Add(new AuthorizeFilter(requiredAuthorizePolicy));
});
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
