using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Impl;
using Patterns_KNTu_22_1_Surhai_Andrii.DAL.DAO.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddTransient<IBrandDAO, BrandDAO>();
builder.Services.AddTransient<ICategoryDAO, CategoryDAO>();
builder.Services.AddTransient<ICountryDAO, CountryDAO>();
builder.Services.AddTransient<IInstrumentDAO, InstrumentDAO>();
builder.Services.AddTransient<IOrderDAO, OrderDAO>();
builder.Services.AddTransient<IOrderDetailDAO, OrderDetailDAO>();
builder.Services.AddTransient<IOrderStatusDAO, OrderStatusDAO>();
builder.Services.AddTransient<IUserDAO, UserDAO>();


//builder.Services.AddScoped<IInstrumentDAO, InstrumentDAO>();
//builder.Services.AddScoped<ICategoryDAO, CategoryDAO>();
//builder.Services.AddScoped<IBrandDAO, BrandDAO>();
//builder.Services.AddScoped<ICountryDAO, CountryDAO>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
