using CSharpFunctionalExtensions;
using SharedKernel;

namespace SharedService.Core.Abstractions.TransactionManagers;

public interface ITransactionManager
{
    Task<Result<ITransactionScope, Error>> BeginTransactionAsync(CancellationToken cancellationToken);

    Task<UnitResult<Error>> SaveChangesAsync(CancellationToken cancellationToken);
}