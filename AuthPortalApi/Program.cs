using AuthLibrary;
using CacheLibrary;
using JwtLibrary;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT_SECRET"])),
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ValidateAudience=false,
        ValidateIssuer=false,   
    };
});


builder.Services.AddSingleton<ICache<string>, MemoryCacheService<string>>();
builder.Services.AddSingleton<IJwt>(new JwtService(new TimeSpan(0, 1, 0), builder.Configuration["JWT_SECRET"]));
builder.Services.AddSingleton<IAuth>
    (
        new CognitoUserService(new CognitoUserServiceConfig
        {
            SecretAccessKey = builder.Configuration["AWS_SECRET_ACCESS_KEY"],
            AccessKeyId = builder.Configuration["AWS_ACCESS_KEY_ID"],
            ClientID = builder.Configuration["CLIENT_ID"],
            ClientSecret = builder.Configuration["CLIENT_SECRET"],
            UserpoolId = builder.Configuration["USERPOOL_ID"],
        })
    );


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(
    options => {
        options.AddPolicy("all-origins", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
     }
    );


var app = builder.Build();

app.UseCors("all-origins");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
