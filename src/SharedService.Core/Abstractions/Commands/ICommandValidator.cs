using CSharpFunctionalExtensions;
using SharedKernel;

namespace SharedService.Core.Abstractions.Commands;

public interface ICommandValidator<in TTarget>
    where TTarget : ICommand
{
    public UnitResult<Errors> ValidateCommand(TTarget target);
}