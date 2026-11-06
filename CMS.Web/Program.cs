using CMS.Data;
using CMS.Data.Models;
using CMS.Infrastructure.AutoMapper;
using CMS.Infrastructure.Middlewares;
using CMS.Infrastructure.Services;
using CMS.Infrastructure.Services.Advertisements;
using CMS.Infrastructure.Services.Categories;
using CMS.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<CMSDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddIdentity<User, IdentityRole>(config =>
{
    config.User.RequireUniqueEmail = true;
    config.Password.RequireDigit = false;
    config.Password.RequiredLength = 6;
    config.Password.RequireLowercase = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
    config.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<CMSDbContext>()
    .AddDefaultTokenProviders().AddDefaultUI();

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

//builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddAutoMapper(x => { x.AddProfile<MapperProfile>();  }, typeof(Program));
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IAdvertisementService, AdvertisementService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.UseExceptionHandler(opts => opts.UseMiddleware<ExceptionHandler>());

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
