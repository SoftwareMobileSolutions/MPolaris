

using mpolaris.Data;
using mpolaris.Interfaces;
using mpolaris.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<SqlCnConfigMain>();
builder.Services.AddScoped<SqlCnConfigMainB>();
builder.Services.AddScoped<Imp_vermapa, mp_vermapaServices>();

builder.Services.AddDistributedMemoryCache();

// Add services to the container.


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
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
