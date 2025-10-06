using System.Text.Json.Serialization;

namespace SharedKernel.Sorting;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortDirection
{
    Asc = 0,
    Desc = 1,
}