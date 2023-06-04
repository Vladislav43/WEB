using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Fluentify.Web.Data;
using Fluentify.Web.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
  googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
   googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});
var connectionString = builder.Configuration.GetConnectionString("StoreDatabase") ?? throw new InvalidOperationException("Connection string 'StoreDatabase' not found.");

builder.Services.AddDbContext<FluentifyDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<FluentifyDbContext>();

// Add services to the container.



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["ApplicationInsights"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();


app.Run();