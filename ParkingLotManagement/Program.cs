using Microsoft.Extensions.DependencyInjection;
using ParkingLotManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

string connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton<IParkingLotRepository>(new ParkingLotRepository(connectionString));
builder.Services.AddSingleton<IParkingLotSpaceRepository>(new ParkingLotSpaceRepository(connectionString));
builder.Services.AddSingleton<IParkingLotTransactionRepository>(new ParkingLotTransactionRepository(connectionString));


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
