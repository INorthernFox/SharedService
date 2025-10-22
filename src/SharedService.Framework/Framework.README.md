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