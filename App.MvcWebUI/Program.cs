using App.Business.Abstract;
using App.Business.Concrete;
using App.DataAccess.Abstract;
using App.DataAccess.Concrete.EfEntityFramework;
using App.MvcWebUI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddSingleton<ICartSessionService, CartSessionService>();
builder.Services.AddSingleton<ICartService, CartService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();

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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
