using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Domains
{
    public class ArtGallleryContext(DbContextOptions<ArtGallleryContext> opt) : DbContext(opt)
    {
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

    }
}
