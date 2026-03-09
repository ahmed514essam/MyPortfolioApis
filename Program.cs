using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPortfolioApis;
using MyPortfolioApis.Data;
using MyPortfolioApis.Data;
using MyPortfolioApis.Extentions;
using MyPortfolioApis.Models;
using MyPortfolioApis.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.CustomJwtAuth(builder.Configuration);
builder.Services.SwaggerGenJwtAuth();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("myConnection")));

builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddScoped<AboutServices>();
builder.Services.AddScoped<HomeServices>();
builder.Services.AddScoped<SkillsServices>();
builder.Services.AddScoped<ProjectsServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
