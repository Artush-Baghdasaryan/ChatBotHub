using System.Text;
using ChatBotHub.WebApi.Application.Services;
using ChatBotHub.WebApi.Application.Services.Implementations;
using ChatBotHub.WebApi.Infrastructure.Context;
using ChatBotHub.WebApi.Infrastructure.Repositories;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Settings
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<RagServiceSettings>(builder.Configuration.GetSection("RagServiceSettings"));
#endregion

#region Data
DataContext.ConfigureData();
builder.Services
    .AddScoped<DataContext>()
    .AddScoped<ChatBotRepository>()
    .AddScoped<UserRepository>()
    .AddScoped<AttachmentRepository>()
    .AddScoped<FileModelRepository>();
#endregion

#region Services
builder.Services
    .AddScoped<IUserService, UserService>()
    .AddScoped<IFileService, FileService>()
    .AddScoped<IChatBotService, ChatBotService>()
    .AddScoped<IIndexService, IndexService>()
    .AddScoped<IRagService, RagService>()
    .AddScoped<IAuthService, AuthService>();
#endregion

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
