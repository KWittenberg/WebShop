var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add ApplicationUser OLD
// builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

// Add ApplicationUser NEW
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.ClaimsIdentity.UserIdClaimType = JwtRegisteredClaimNames.Jti;
    }).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();



// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

//builder.Services.AddControllersWithViews();

// Add Startup for UpdateShoppinCartStatus
builder.Services.Configure<AppConfig>(builder.Configuration);



// Add IdentityService
builder.Services.AddSingleton<IIdentityService, IdentityService>();
// Add ApplicationUserService
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
// Add ProductService
builder.Services.AddScoped<IProductService, ProductService>();
// Add CustomerService
builder.Services.AddScoped<ICustomerService, CustomerService>();


// Custom Location for Login Page
builder.Services.ConfigureApplicationCookie(options =>
{
    //Location for your Custom Access Denied Page
    //options.AccessDeniedPath = "Account/AccessDenied";

    //Location for your Custom Login Page
    options.LoginPath = "/ApplicationUser/Login";
});

builder.Services.AddControllersWithViews();

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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Add IdentityService for Create Role and User
app.Services.GetService<IIdentityService>();

//Seed database
AppDbInitializer.Seed(app);

app.Run();