using ArtGallery.Repositories;
using ArtGallery.ServiceInterfaces;
using ArtGallery.Services;

namespace ArtGallery.Configuration
{
    public static class DependencyConfiguration
    {
        public static void AddDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Program));
            services.AddHttpContextAccessor();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddHttpContextAccessor();
        }
    }
}
