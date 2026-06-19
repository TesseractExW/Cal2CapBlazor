using Cal2CapBlazor.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Cal2CapBlazor.Infrastructure.Context;
/// <summary>Represents the primary database context for the application.</summary>
public class AppMainDbContext : DbContext {
    #region Database sets
    /// <summary>The database set for managing <see cref="Account"/> entities.</summary>
    public DbSet<Account> Accounts { get; set; }
    /// <summary>The database set for managing <see cref="Meal"/> entities.</summary>
    public DbSet<Meal> Meals { get; set; }
    #endregion

    #region Class constructure
    /// <summary> Initializes the context with dependency injection options.</summary>
    /// <param name="options">Configuration options for this context.</param>
    public AppMainDbContext(DbContextOptions<AppMainDbContext> options) : base(options) {}
    #endregion

    #region Class methods
    /// <summary> Configures entity relationships and database schema mapping.</summary>
    /// <param name="modelBuilder">The builder used to construct the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Account>()
            .HasMany(e => e.Meals)
            .WithOne(e => e.Account)
            .HasForeignKey(e => e.AccountId);
        base.OnModelCreating(modelBuilder);
    }
    #endregion
}