namespace SharedKernel;

public class GeneralErrors
{
    public static class Locations
    {
        public static Error EmptyLocationName() =>
            Error.Failure("Имя локации не может быть пустым");
    }
}