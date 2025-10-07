using CSharpFunctionalExtensions;
using SharedKernel;

namespace SharedService.Core.Abstractions.Queries;

public interface IQueryHandler<TResponse, in TQuery>
    where TQuery : IQuery
{
    Task<Result<TResponse, Errors>> Handle(TQuery query, CancellationToken cancellationToken);
}