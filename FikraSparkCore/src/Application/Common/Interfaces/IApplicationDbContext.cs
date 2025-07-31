using FikraSparkCore.Domain.Entities;

namespace FikraSparkCore.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<Idea> Ideas { get; }
}

