using BetterHere.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BetterHere.Web.Data
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<EstablishmentEntity> Establishments { get; set; }

        public DbSet<FoodEntity> Foods { get; set; }

        public DbSet<EstablishmentLocationEntity> EstablishmentLocations { get; set; }

        public DbSet<TypeEstablishmentEntity> TypeEstablishments { get; set; }

        public DbSet<TypeFoodEntity> TypeFoods { get; set; }
        
        public DbSet<CityEntity> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EstablishmentEntity>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<FoodEntity>()
                .HasIndex(t => new { t.FoodName, t.EstablishmentId })
                .IsUnique();
        }
    }
}
