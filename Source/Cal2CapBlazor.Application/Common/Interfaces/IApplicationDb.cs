using Microsoft.EntityFrameworkCore;
using Cal2CapBlazor.Domain.Accounts;
using Cal2CapBlazor.Domain.Meals;

namespace Cal2CapBlazor.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Account> Accounts { get; }
    DbSet<Meal> Meals { get; }
}