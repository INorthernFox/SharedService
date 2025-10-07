namespace SharedKernel.Sorting;

public record SortOption
{
    public SortDirection Direction { get; init; }
    public string Field { get; init; }
}