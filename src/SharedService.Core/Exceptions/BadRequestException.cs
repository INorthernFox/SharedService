using System.Text.Json;
using SharedKernel;

namespace SharedService.Core.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(Error error)
        : this([error])
    {
    }

    public BadRequestException(Error[] errors)
        : base(JsonSerializer.Serialize(errors))
    {
    }
}