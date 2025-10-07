# INNorthernFox.SharedService.SharedKernel

Набор **базовых строительных блоков** для .NET-проектов: типы ошибок и конвейер обработки результатов (Result-pattern), универсальный конверт ответов `Envelope`, единые опции сортировки, интервалы, простые расширения и абстракции кэширования.
Пакет предназначен для **переиспользования во всех микросервисах экосистемы *SharedService*** и формирования единого подхода к обработке ошибок, форматам ответов и архитектурным паттернам.

> **Целевая платформа:** .NET 9.0
> Совместим с более ранними TFM при необходимости.

---

## 📦 Метаданные пакета

| Свойство                       | Значение                                                                                                          |
| ------------------------------ |-------------------------------------------------------------------------------------------------------------------|
| **PackageId**                  | `INNorthernFox.SharedService.SharedKernel`                                                                        |
| **TFM**                        | `net9.0`                                                                                                          |
| **License**                    | MIT (`PackageLicenseExpression`)                                                                                  |
| **RepositoryUrl / ProjectUrl** | [GitHub → INorthernFox/SharedService](https://github.com/INNorthernFox/SharedService)                             |
| **SourceLink**                 | `Microsoft.SourceLink.GitHub` включён; `PublishRepositoryUrl`, `EmbedUntrackedSources`, `Deterministic` — активны |
| **Symbols**                    | публикуются в `.snupkg` (`IncludeSymbols=true`)                                                                   |
| **XML-документация**           | генерируется (`GenerateDocumentationFile=true`)                                                                   |
| **Автосборка пакета**          | включена (`GeneratePackageOnBuild=true`)                                                                          |

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
* **Кэширование:**
  Контракт `ICacheService` и реализация `CacheService` поверх `Microsoft.Extensions.Caching.*`.
* **Кодстайл:**
  Подключён `StyleCop.Analyzers` с едиными правилами оформления; `SourceLink` активирован для отладки исходников из GitHub.

---

## 📁 Структура проекта

```
src/SharedService.SharedKernel/
│
├── Caching/
│   ├── ICacheService.cs
│   └── CacheService.cs
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
