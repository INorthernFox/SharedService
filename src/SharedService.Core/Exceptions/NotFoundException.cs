using System.Text.Json;
using SharedKernel;

namespace SharedService.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(Error error)
        : this([error])
    {
    }

    public NotFoundException(Error[] errors)
        : base(JsonSerializer.Serialize(errors))
    {
    }
}