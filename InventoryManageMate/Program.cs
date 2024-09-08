using AutoMapper;
using InventoryManageMate.AggregateRoot;
using InventoryManageMate.DTO.MappingProfile;
using InventoryManageMate.Handler.Services;
using InventoryManageMate.Repository.Data;
using InventoryManageMate.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryManageMateSystem")));

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

// Register handlers
builder.Services.AddScoped<IOrderHandler, OrderHandler>();
builder.Services.AddScoped<OrderHandler>();
builder.Services.AddScoped<IOrderDetailHandler, OrderDetailHandler>();
builder.Services.AddScoped<OrderDetailHandler>();

// Register ExportService
builder.Services.AddScoped<ExportService>();

// Register the generic repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
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
