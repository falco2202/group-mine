namespace Domain.Common
{
    public interface IUnitOfWork
    {
        // Add specific repositories

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
