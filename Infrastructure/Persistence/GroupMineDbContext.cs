using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Domain.Enitity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class GroupMineDbContext : DbContext, IGroupMineDbContext
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public GroupMineDbContext(
                    DbContextOptions<GroupMineDbContext> options,
                    IDateTimeProvider dateTimeProvider) : base(options)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTimeProvider.UtcNow;
                        entry.Entity.LastModified = _dateTimeProvider.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTimeProvider.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
