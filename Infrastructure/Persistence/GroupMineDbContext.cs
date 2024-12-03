using Application.Common.Interfaces;
using Domain.Enitity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Persistence
{
    public class GroupMineDbContext : DbContext, IGroupMineDbContext
    {
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
