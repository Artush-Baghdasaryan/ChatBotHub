using System.Text;
using ChatBotHub.Application.Accounts;
using ChatBotHub.Application.AiKnowledge;
using ChatBotHub.Application.AiKnowledge.Options;
using ChatBotHub.Application.Attachments;
using ChatBotHub.Application.ChatBots;
using ChatBotHub.Application.Common.Options;
using ChatBotHub.Application.Files;
using ChatBotHub.Application.Sessions;
using ChatBotHub.Domain.Accounts;
using ChatBotHub.Domain.Attachments;
using ChatBotHub.Domain.ChatBots;
using ChatBotHub.Domain.Files;
using ChatBotHub.Domain.Session;
using ChatBotHub.Infrastructure.Common;
using ChatBotHub.Infrastructure.MongoDb;
using ChatBotHub.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo {
        Title = "ChatBotHub API",
        Version = "v1"
    });
    
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

#region Infrastructure
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("DatabaseOptions"));

MongoDbDataContext.ConfigureData();
builder.Services.AddSingleton<MongoDbDataContext>();
builder.Services
    .AddScoped<IChatBotRepository, ChatBotRepository>()
    .AddScoped<IAttachmentRepository, AttachmentRepository>()
    .AddScoped<IAccountRepository, AccountRepository>()
    .AddScoped<IFileRepository, FileRepository>()
    .AddScoped<ISessionRepository, SessionRepository>();
#endregion

#region Application
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.Configure<AiKnowledgeOptions>(builder.Configuration.GetSection("AiKnowledgeOptions"));

builder
    .Services
    .AddScoped<IAccountQueryService, AccountQueryService>()
    .AddScoped<IAccountCommandService, AccountCommandService>()
    .AddScoped<IAccountAuthenticationService, AccountAuthenticationService>()
    .AddScoped<IChatBotQueryService, ChatBotQueryService>()
    .AddScoped<IChatBotCommandService, ChatBotCommandService>()
    .AddScoped<IAttachmentQueryService, AttachmentQueryService>()
    .AddScoped<IAttachmentCommandService, AttachmentCommandService>()
    .AddScoped<IFileQueryService, FileQueryService>()
    .AddScoped<IFileCommandService, FileCommandService>()
    .AddScoped<ISessionQueryService, SessionQueryService>()
    .AddScoped<ISessionCommandService, SessionCommandService>()
    .AddScoped<IAiKnowledgeClient, AiKnowledgeClient>()
    .AddScoped<IKnowledgeService, KnowledgeService>();
#endregion

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
        ValidAudience = builder.Configuration["JwtOptions:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Key"]!))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyBuilder => {
    policyBuilder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
