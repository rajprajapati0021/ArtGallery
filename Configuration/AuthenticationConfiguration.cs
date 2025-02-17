﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace ArtGallery.Configuration;

public static class AuthenticationConfiguration
{
    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var securityKey = configuration["JWT:SecurityKey"];

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration.GetSection("JWT:Issuer").Value,
                    ValidAudience = configuration.GetSection("JWT:Audience").Value,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:SecurityKey").Value!)),
                    RoleClaimType = ClaimTypes.Role
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.HttpContext.Request.Path.StartsWithSegments("/api/chatHub") &&
                             context.HttpContext.Request.Query.TryGetValue("access_token", out var token))
                        {
                            context.Token = token;
                            Console.WriteLine($"Extracted Token: {context.Token}");
                        }

                        return Task.CompletedTask;
                    }
                };
            });

    }
}
    