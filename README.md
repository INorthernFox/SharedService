# SharedService

---


# INorthernFox.SharedService.Framework

Библиотека **инфраструктурных расширений для ASP.NET Core**, обеспечивающая единый подход к регистрации эндпоинтов, глобальной обработке ошибок, настройке Swagger/OpenAPI и интеграции HTTP-клиентов.
Используется как общий слой инфраструктуры для всех микросервисов экосистемы *SharedService*.

> **Целевая платформа:** .NET 9.0

---

## 📦 Метаданные пакета

| Свойство                       | Значение                                                                              |
| ------------------------------ | ------------------------------------------------------------------------------------- |
| **PackageId**                  | `INorthernFox.SharedService.Framework`                                                |
| **TFM**                        | `net9.0`                                                                              |
| **License**                    | MIT (`PackageLicenseExpression`)                                                      |
| **RepositoryUrl / ProjectUrl** | [GitHub → INNorthernFox/SharedService](https://github.com/INNorthernFox/SharedService) |
| **SourceLink**                 | `Microsoft.SourceLink.GitHub` включён                                                 |
| **XML-документация**           | генерируется (`GenerateDocumentationFile=true`)                                       |
| **Автосборка пакета**          | включена (`GeneratePackageOnBuild=true`)                                              |

---

## 📚 Зависимости

* **Microsoft.AspNetCore.OpenApi** — 9.0.1
* **Microsoft.EntityFrameworkCore.Design** — 9.0.8
* **Swashbuckle.AspNetCore.SwaggerUI** — 9.0.3
* **Microsoft.Extensions.Hosting** — 9.0.9
* **StyleCop.Analyzers** — 1.1.118
* **Serilog.AspNetCore** — 9.0.0
* **Microsoft.SourceLink.GitHub** *(PrivateAssets = All)* — отладка по исходникам GitHub.

---

## ⚙️ Возможности

* **Endpoint Results:**
  Унифицированные результаты ответов API (`EndpointResult`, `ErrorsResult`, `SuccessResult`) для контроллеров и Minimal API.

* **Middleware:**
  Общие промежуточные слои для ASP.NET Core — `ExceptionMiddleware` и расширение `ExceptionMiddlewareExtension` для глобальной обработки ошибок и логирования.

* **Dependency Injection:**
  Единая точка регистрации инфраструктурных сервисов через `DependencyInjection`.

* **Расширяемость:**
  Поддержка регистрации контроллеров, Swagger, HTTP-клиентов и других инфраструктурных компонентов.

---

## 📁 Структура проекта

```
src/SharedService.Framework/
│
├── EndpointResults/
│   ├── EndpointResult.cs
│   ├── ErrorsResult.cs
│   └── SuccessResult.cs
│
├── Middlewares/
│   ├── ExceptionMiddleware.cs
│   └── ExceptionMiddlewareExtension.cs
│
└── DependencyInjection.cs
```

---
## AddOpenApiDependencies в DependencyInjection
```
    public static WebApplicationBuilder AddOpenApiDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((doc, ctx, ct) =>
            {
                doc.Servers = [new OpenApiServer { Url = "/" }];
                return Task.CompletedTask;
            });

            options.AddSchemaTransformer((schema, context, _) =>
            {
                if (context.JsonTypeInfo.Type == typeof(Envelope<Errors>))
                {
                    if (schema.Properties.TryGetValue("errors", out var errorsProp))
                    {
                        errorsProp.Items.Reference = new OpenApiReference { Type = ReferenceType.Schema, Id = "Error", };
                    }
                }

                return Task.CompletedTask;
            });
        });

        return builder;
    }
```

---
# INorthernFox.SharedService.SharedKernel

Набор **базовых строительных блоков** для .NET-проектов: типы ошибок и конвейер обработки результатов (Result-pattern), универсальный конверт ответов `Envelope`, единые опции сортировки, интервалы, простые расширения и абстракции кэширования.
Пакет предназначен для **переиспользования во всех микросервисах экосистемы *SharedService*** и формирования единого подхода к обработке ошибок, форматам ответов и архитектурным паттернам.

> **Целевая платформа:** .NET 9.0
> Совместим с более ранними TFM при необходимости.

---

## 📦 Метаданные пакета

| Свойство                       | Значение                                                                                                         |
| ------------------------------ |------------------------------------------------------------------------------------------------------------------|
| **PackageId**                  | `INorthernFox.SharedService.SharedKernel`                                                                        |
| **TFM**                        | `net9.0`                                                                                                         |
| **License**                    | MIT (`PackageLicenseExpression`)                                                                                 |
| **RepositoryUrl / ProjectUrl** | [GitHub → INorthernFox/SharedService](https://github.com/INNorthernFox/SharedService)                            |
| **SourceLink**                 | `Microsoft.SourceLink.GitHub` включён; `PublishRepositoryUrl`, `EmbedUntrackedSources`, `Deterministic` — активны |
| **Symbols**                    | публикуются в `.snupkg` (`IncludeSymbols=true`)                                                                  |
| **XML-документация**           | генерируется (`GenerateDocumentationFile=true`)                                                                  |
| **Автосборка пакета**          | включена (`GeneratePackageOnBuild=true`)                                                                         |

### Зависимости

* **CSharpFunctionalExtensions** 3.6.0
* **Microsoft.Extensions.Caching.Abstractions** 9.0.9
* **Microsoft.SourceLink.GitHub** 8.0.0 (PrivateAssets = `All`)

---

## ⚙️ Возможности

* **Доменные ошибки:**
  `Error`, `Errors`, `ErrorCodes`, `ErrorType`, `GeneralErrors` — типизированные описания и категории ошибок.
* **Result-pattern:**
  Интеграция с *CSharpFunctionalExtensions* для унифицированной обработки успехов и ошибок.
* **Унифицированный ответ API:**
  `Envelope<T>` — единый формат: `{ data, error, traceId, timestamp }`.
* **Сортировка:**
  `SortDirection`, `SortOption`, `SortExtensions` — декларативная сортировка `IQueryable` и коллекций.
* **Интервалы:**
  `IntervalType`, `IntervalTypeExtensions` — получение диапазонов дат (`Week`, `Month`, `Year`, …).
* **Кодстайл:**
  Подключён `StyleCop.Analyzers` с едиными правилами оформления; `SourceLink` активирован для отладки исходников из GitHub.

---

## 📁 Структура проекта

```
src/SharedService.SharedKernel/
│
├── Extensions/
│   └── SortExtensions.cs
│
├── Intervals/
│   ├── IntervalType.cs
│   └── IntervalTypeExtensions.cs
│
├── Sorting/
│   ├── SortDirection.cs
│   └── SortOption.cs
│
├── Envelope.cs
├── Error.cs
├── Errors.cs
├── ErrorCodes.cs
├── ErrorType.cs
└── GeneralErrors.cs
```

---

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

