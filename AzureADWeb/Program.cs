using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

//Aplication/Client ID - 84dcae56-f3b9-417b-b83c-cbaa488c97ab
//Authorize application OAuth2 - https://login.microsoftonline.com/332b511a-c5e3-41d8-8dac-a1267a235b41/oauth2/v2.0/authorize
// https://login.microsoftonline.com/332b511a-c5e3-41d8-8dac-a1267a235b41/v2.0

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
        //options.ResponseType = "id_token";
        options.ResponseType = "code";  // to be updated as code if ClientSecret is added
        options.SaveTokens = true;
        options.ClientSecret = "sEV7Q~fDB1d9DwbTK-gWYL_-RwFhGzIEehaTs";
    })
    ;


//services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
//}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
//            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options => {
//                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//                options.Authority = "https://login.microsoftonline.com/fbffd135-c37d-4b61-8c05-a641ba181d5c/v2.0";
//                options.ClientId = "398d155d-1078-4b3b-acb1-686cc6414698";
//                options.ResponseType = "code";
//                options.SaveTokens = true;
//                options.Scope.Add("api://de626dc0-5cbe-4f4b-9f8e-3148b9288f7b/AdminAccess");
//                options.ClientSecret = "Z~_0vsP8PUJu05l_Eq8WTf5g~po7cbu5rR";
//                options.TokenValidationParameters = new TokenValidationParameters()
//                {
//                    NameClaimType = "name"
//                };
//            });

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
