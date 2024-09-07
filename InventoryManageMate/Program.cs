using AutoMapper;
using InventoryManageMate.AggregateRoot;
using InventoryManageMate.DTO.MappingProfile;
using InventoryManageMate.Handler.Services;
using InventoryManageMate.Repository.Data;
using InventoryManageMate.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryManageMateSystem")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddScoped<IOrderHandler, OrderHandler>();
builder.Services.AddScoped<OrderHandler>();
builder.Services.AddScoped<IOrderDetailHandler, OrderDetailHandler>();
builder.Services.AddScoped<OrderDetailHandler>();

builder.Services.AddScoped<ExportService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
