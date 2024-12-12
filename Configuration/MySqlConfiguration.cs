using ArtGallery.Domains;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Configuration;


public static class MySqlConfiguration
{
    public static void ConfigureMySql(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ArtGallleryContext>(opt =>
            opt.UseMySql(
                configuration.GetConnectionString("Default"), 
                ServerVersion.AutoDetect(configuration.GetConnectionString("Default")),
                opt => {
                    opt.EnableRetryOnFailure();
                }
            )
        );

    }
}