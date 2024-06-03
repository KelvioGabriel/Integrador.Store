using Microsoft.OpenApi.Models;

namespace Senac.eShop.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "eShop API",
                    Description = "Esta API é feita como parte do curso" +
                    " técnico em TI - Senac",
                    Contact = new OpenApiContact()
                    {
                        Name = "Senac",
                        Email = "contato@go.senac.br"
                    }
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Autenticação por cabeçalho de JWT, usando o esquema Bearer.
                                    Exemplo: Bearer 1235345sfasd",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        }, new List<string>() 
                    }
                });
            });


        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
            });

        }
    }
}
