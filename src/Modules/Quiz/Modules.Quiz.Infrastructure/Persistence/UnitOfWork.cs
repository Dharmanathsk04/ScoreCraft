using Modules.Quiz.Infrastructure.Data;
using Shared.Core;

namespace Modules.Quiz.Infrastructure.Persistence;
internal class UnitOfWork(QuestionModuleDbContext dbContext) : IUnitOfWork
{
    private bool _disposed;
    
    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                dbContext.Dispose();
        _disposed = true;
    }
}
