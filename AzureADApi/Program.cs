using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = "https://login.microsoftonline.com/332b511a-c5e3-41d8-8dac-a1267a235b41/v2.0";
        options.Audience = "api://5fb4364d-f80f-4c42-afce-7cf963441985";

        #region Validate Token
        //disable token validation
        options.TokenValidationParameters.ValidateIssuer = false; 

        //get accesstoken value from variable and open link - jwt.io and paste contents and see the issuer value
        // copy and paste here the value
        
        //options.TokenValidationParameters.ValidIssuer = "https://sts.windows.net/332b511a-c5e3-41d8-8dac-a1267a235b41/";
        #endregion

    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
