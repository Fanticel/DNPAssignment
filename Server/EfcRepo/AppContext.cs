using Main;
using Microsoft.EntityFrameworkCore;

namespace EfcRepo;

public class AppContext : DbContext {
    public DbSet<Post> postSet => Set<Post>();
    public DbSet<User> userSet => Set<User>();
    public DbSet<Comment> commentSet => Set<Comment>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=1234");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Post>().HasKey(p => p.PostId);
        modelBuilder.Entity<Comment>().HasKey(c => c.Id);
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Comment>().Property(c => c.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Post>()
            .HasMany<Comment>()
            .WithOne()
            .HasForeignKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.HasDefaultSchema("reddit");
    }
}