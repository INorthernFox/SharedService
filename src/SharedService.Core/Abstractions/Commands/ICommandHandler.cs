using CSharpFunctionalExtensions;
using SharedKernel;

namespace SharedService.Core.Abstractions.Commands;

public interface ICommandHandler<TResponse, in TCommand>
    where TCommand : ICommand
{
    Task<Result<TResponse, Errors>> Handle(TCommand command, CancellationToken cancellationToken);
}