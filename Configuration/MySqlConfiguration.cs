using Microsoft.EntityFrameworkCore;

namespace GIFTCity.API.Configuration;


public static class MySqlConfiguration
{
    public static void ConfigureMySql(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ArtGalleryContext>(opt =>
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