# INorthernFox.SharedService.Core

Библиотека **базовых интерфейсов и абстракций уровня Application** для построения микросервисов .NET.
Содержит контракты команд/запросов, транзакционные менеджеры, механизмы кэширования и пользовательские исключения, используемые во всех сервисах экосистемы *SharedService*.

> **Целевая платформа:** .NET 9.0

---

## 📦 Метаданные пакета

| Свойство                       | Значение                                                                              |
| ------------------------------ | ------------------------------------------------------------------------------------- |
| **PackageId**                  | `INorthernFox.SharedService.Core`                                                     |
| **TFM**                        | `net9.0`                                                                              |
| **License**                    | MIT (`PackageLicenseExpression`)                                                      |
| **RepositoryUrl / ProjectUrl** | [GitHub → INNorthernFox/SharedService](https://github.com/INNorthernFox/SharedService) |
| **SourceLink**                 | `Microsoft.SourceLink.GitHub` включён                                                 |
| **XML-документация**           | генерируется (`GenerateDocumentationFile=true`)                                       |
| **Автосборка пакета**          | включена (`GeneratePackageOnBuild=true`)                                              |

### Зависимости

* **FluentValidation.DependencyInjectionExtensions 12.0.0**
* **Microsoft.Extensions.Caching.Abstractions 9.0.9** 
* **Microsoft.SourceLink.GitHub** — отладка по исходникам.

---

## ⚙️ Возможности

* **Application-level контракты:**
  Интерфейсы `ICommand<T>`, `ICommandHandler<TRequest, TResponse>`, `IQuery<T>`, `IQueryHandler<TRequest, TResponse>` определяют соглашения между слоями Application и Domain.

* **Валидация:**
  Интерфейсы `ICommandValidator<T>` и `IQueryValidator<T>` для FluentValidation или кастомных реализаций.

* **Транзакции:**
  `ITransactionManager` и `ITransactionScope` предоставляют единый механизм для атомарных операций и управления `DbContext`/UoW.

* **Кэширование:**
  Контракты `ICacheService` и реализация `CacheService` для интеграции с `IMemoryCache` или `IDistributedCache`.

* **Исключения:**
  `BadRequestException`, `NotFoundException` — базовые ошибки уровня Application, совместимые с ASP.NET middleware для глобальной обработки.

---

## 📁 Структура проекта

```
src/SharedService.Core/
│
├── Abstractions/
│   ├── Commands/
│   │   ├── ICommand.cs
│   │   ├── ICommandHandler.cs
│   │   └── ICommandValidator.cs
│   │
│   ├── Queries/
│   │   ├── IQuery.cs
│   │   ├── IQueryHandler.cs
│   │   └── IQueryValidator.cs
│   │
│   └── TransactionManagers/
│       ├── ITransactionManager.cs
│       └── ITransactionScope.cs
│
├── Caching/
│   ├── ICacheService.cs
│   └── CacheService.cs
│
└── Exceptions/
    ├── BadRequestException.cs
    └── NotFoundException.cs
```

