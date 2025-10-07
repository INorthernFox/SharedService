using CSharpFunctionalExtensions;
using SharedKernel;

namespace SharedService.Core.Abstractions.Queries;

public interface IQueryValidator<in TTarget>
    where TTarget : IQuery
{
    public UnitResult<Errors> ValidateQuery(TTarget target);
}