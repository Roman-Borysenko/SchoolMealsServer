using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SchoolMeals.Models
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            :base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(System.Console.WriteLine, LogLevel.Information);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>()
                .HasOne(p => p.Language)
                .WithMany(t => t.Dishes)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<DishIngredient>()
                .HasOne(di => di.Ingredient)
                .WithMany(i => i.DishIngredients)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<DishTag>()
                .HasOne(dt => dt.Tag)
                .WithMany(t => t.DishTags)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
