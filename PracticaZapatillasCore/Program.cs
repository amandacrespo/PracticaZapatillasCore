using Microsoft.EntityFrameworkCore;
using PracticaZapatillasCore.Data;
using PracticaZapatillasCore.Repositories;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("SqlClase");
builder.Services.AddDbContext<ZapatillasContext>(options => options.UseSqlServer(connection));

builder.Services.AddTransient<RepositoryZapatillas>();

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
app.UseRouting();

app.UseStaticFiles();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Zapatillas}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
