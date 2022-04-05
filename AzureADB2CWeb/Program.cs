using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.Authority = "https://login.microsoftonline.com/332b511a-c5e3-41d8-8dac-a1267a235b41/v2.0";
        options.ClientId = "84dcae56-f3b9-417b-b83c-cbaa488c97ab";

        #region ClientSecret
        //options.ResponseType = "id_token";
        options.ResponseType = "code";  // to be updated as code if ClientSecret is added
        #endregion region

        options.SaveTokens = true;
        options.Scope.Add("api://5fb4364d-f80f-4c42-afce-7cf963441985/AdminAccess");
        options.ClientSecret = "sEV7Q~fDB1d9DwbTK-gWYL_-RwFhGzIEehaTs";
        //Display email address
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name"
        };
    })
    ;

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
