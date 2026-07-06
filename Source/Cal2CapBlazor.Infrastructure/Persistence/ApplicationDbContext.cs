using System.Text.Json;
using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Domain.Accounts;
using Cal2CapBlazor.Domain.Accounts.ValueObjects;
using Cal2CapBlazor.Domain.Meals;
using Cal2CapBlazor.Domain.Meals.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Cal2CapBlazor.Infrastructure.Persistence;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Meal> Meals => Set<Meal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.EmailAddress)
            .HasConversion(
                email => email.Value,
                value => EmailAddress.Create(value).Value)
            .HasMaxLength(EmailAddress.MaximumLength)
            .IsRequired();

            entity.Property(a => a.DisplayName)
                .HasConversion(
                    displayName => displayName.Value,
                    value => DisplayName.Create(value).Value) 
                .HasMaxLength(DisplayName.MaximumLength)
                .IsRequired();

            entity.Property(a => a.HashedPassword)
                .HasConversion(
                    password => password.Value,
                    value => HashedPassword.Create(value).Value) 
                .HasMaxLength(HashedPassword.MaximumLength)
                .IsRequired();
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.HasKey(m => m.Id);

            entity.HasOne<Account>()
                .WithMany()
                .HasForeignKey(m => m.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.Property(m => m.MealName)
                .HasConversion(
                    name => name.Value,
                    value => MealName.Create(value).Value)
                .HasMaxLength(MealName.MaximumLength)
                .IsRequired();

            entity.Property(m => m.MealDetails)
                .HasConversion(
                    details => details.Value,
                    value => MealDetails.Create(value).Value)
                .HasMaxLength(MealDetails.MaximumLength);

            entity.Property(m => m.NutrientProfile)
                .HasConversion(
                    profile => JsonSerializer.Serialize(profile, (JsonSerializerOptions?)null),
                    json => JsonSerializer.Deserialize<NutrientProfile>(json, (JsonSerializerOptions?)null)!)
                .IsRequired();
        });
    }
}