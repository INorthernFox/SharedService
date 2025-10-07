namespace SharedKernel;

public enum ErrorType
{
    /// <summary>
    /// Неопределенный тип ошибки (по умолчанию)
    /// </summary>
    UNDEFINED,

    /// <summary>
    /// Ошибка валидации данных (некорректные входные данные)
    /// </summary>
    VALIDATION,

    /// <summary>
    /// Запрашиваемый ресурс не найден
    /// </summary>
    NOT_FOUND,

    /// <summary>
    /// Общая ошибка выполнения операции
    /// </summary>
    FAILURE,

    /// <summary>
    /// Конфликт данных (например, дублирование уникальных значений)
    /// </summary>
    CONFLICT,
}