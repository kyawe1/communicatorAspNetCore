using communicator.AuthorizePolicys;
using communicator.Context;
using communicator.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using communicator.ActionFiltersFolder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddUserManager<UserManager<ApplicationUser>>().AddSignInManager<SignInManager<ApplicationUser>>().AddEntityFrameworkStores<ApplicationContext>();

builder.Services.Configure<IdentityOptions>(options=>{
    options.User.RequireUniqueEmail=true;
});

builder.Services.ConfigureApplicationCookie(options=>{
    options.LoginPath="/identity/login";
    options.LogoutPath="/identity/logout";
    options.Cookie.HttpOnly=true;
    options.ExpireTimeSpan=TimeSpan.FromMinutes(100);
    options.SlidingExpiration = true;
});



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SameUser", policy =>
    {
        policy.Requirements.Add(new SameUserRequirement());
    });
});
builder.Services.AddSingleton<IAuthorizationHandler, SameUserHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllerRoute(name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoint.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});



app.Run();
