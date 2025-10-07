using CSharpFunctionalExtensions;
using SharedKernel;

namespace SharedService.Core.Abstractions.TransactionManagers;

public interface ITransactionScope : IDisposable
{
    UnitResult<Errors> Commit();

    UnitResult<Errors> Rollback();
}