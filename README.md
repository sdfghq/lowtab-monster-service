# Lowtab.Monster.Service

Проект создан на основе шаблона **Service.Template** - корпоративного решения для .NET микросервисов.

## Описание

Микросервис построен по принципам Clean Architecture с четким разделением ответственности между слоями. Решение включает
все необходимые компоненты для production-ready сервиса: API, бизнес-логику, инфраструктуру, тесты и утилиты.

### Структура решения

```
├── src/              # Исходный код сервиса
├── tests/            # Модульные и интеграционные тесты
├── tools/            # CLI утилиты (миграции БД)
├── specs/            # OpenAPI спецификации
├── enviroment/       # Docker Compose для локальной разработки
└── .github/          # GitHub Actions workflows
```

### Проекты

#### Основные слои (src/)

**Lowtab.Monster.Service.Api** - Web API слой

- ASP.NET Core приложение с Minimal API endpoints
- Маршрутизация, middleware, dependency injection
- Интеграция с observability (логирование, метрики, health checks)

**Lowtab.Monster.Service.Application** - Бизнес-логика

- Use cases через Mediator pattern (Commands/Queries)
- FluentValidation для валидации запросов
- Организация по фичам (Feature Folders)

**Lowtab.Monster.Service.Domain** - Доменная модель

- Сущности, value objects, enums
- Не зависит от внешних библиотек
- Генерируется только при `WithDatabase=true`

**Lowtab.Monster.Service.Infrastructure** - Инфраструктурный слой

- EF Core репозитории и DbContext
- Интеграции: PostgreSQL, Redis, Kafka, RabbitMQ
- Внешние HTTP клиенты

**Lowtab.Monster.Service.Contracts** - Публичные контракты

- DTO для API endpoints
- Контракты сообщений для очередей
- Публикуется как NuGet пакет

**Lowtab.Monster.Service.Api.Client** - HTTP клиент

- NSwag генерируемый клиент для внешних потребителей
- Автоматическая генерация из OpenAPI спецификации
- Публикуется как NuGet пакет

#### Утилиты (tools/)

**Lowtab.Monster.Service.Cli** - Консольная утилита

- Применение EF Core миграций
- Подготовка окружения перед запуском

#### Тесты (tests/)

**Lowtab.Monster.Service.Api.UnitTests** - Тесты API

- Интеграционные тесты через WebApplicationFactory
- Тестирование endpoints и middleware

**Lowtab.Monster.Service.Application.UnitTests** - Тесты бизнес-логики

- Unit тесты handlers и validators
- InMemory БД для изоляции

**Tests.Common** - Общая инфраструктура

- Фикстуры, моки, тестовые данные

### Конфигурационные файлы

| Файл                       | Назначение                                            |
|----------------------------|-------------------------------------------------------|
| `.editorconfig`            | Правила форматирования и стиля кода (C#, JSON, XML)   |
| `Directory.Build.props`    | Общие MSBuild свойства для всех проектов              |
| `Directory.Packages.props` | Централизованное управление версиями NuGet пакетов    |
| `global.json`              | Фиксация версии .NET SDK (8.0.400+)                   |
| `nuget.config`             | Источники NuGet пакетов (nuget.org + GitHub Packages) |
| `docker-compose.yml`       | Инфраструктура для локальной разработки               |

### Конфигурация приложения (appsettings)

| Файл                          | Окружение            |
|-------------------------------|----------------------|
| `appsettings.json`            | Базовая конфигурация |
| `appsettings.local.json`      | Локальная разработка |
| `appsettings.develop.json`    | Dev/Dynamic среды    |
| `appsettings.staging.json`    | Staging              |
| `appsettings.production.json` | Production           |

## Начало работы

### Предварительные требования

1. **.NET 8.0 SDK** (8.0.400 или выше)
2. **Docker Desktop** (для PostgreSQL и Redis)
3. **Доступ к GitHub Packages** для пакетов `Sdf.*`

### Настройка окружения

Запустите инфраструктуру через Docker Compose:

```bash
docker-compose -f enviroment/docker-compose.yml up -d
```

Это запустит:

- PostgreSQL (порт 5432)
- KeyDB/Redis (порт 6379)
- RedisInsight (порт 8009)

### Применение миграций

Если проект создан с `WithDatabase=true`:

```bash
cd tools/Lowtab.Monster.Service.Cli
dotnet run
```

### Запуск приложения

```bash
cd src/Lowtab.Monster.Service.Api
dotnet run
```

Или с hot reload:

```bash
dotnet watch run
```

Приложение доступно по адресу:

- HTTP: http://localhost:5844
- HTTPS: https://localhost:6434
- Swagger UI: https://localhost:6434/swagger

### Запуск тестов

Все тесты:

```bash
dotnet test
```

С покрытием кода:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

Только unit тесты:

```bash
dotnet test --filter Category=Unit
```

### Сборка проекта

```bash
dotnet build
```

Release сборка:

```bash
dotnet build -c Release
```

## Архитектура

Проект следует принципам **Clean Architecture**:

```
┌─────────────────────────────────────┐
│           Lowtab.Monster.Service.Api           │  ← Presentation Layer
│         (Controllers, Endpoints)     │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│      Lowtab.Monster.Service.Application        │  ← Business Logic Layer
│    (Handlers, Validators, DTOs)     │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│        Lowtab.Monster.Service.Domain           │  ← Domain Layer
│      (Entities, Value Objects)      │
└─────────────────────────────────────┘
               ▲
               │
┌──────────────┴──────────────────────┐
│     Lowtab.Monster.Service.Infrastructure      │  ← Infrastructure Layer
│   (DbContext, Repositories, APIs)   │
└─────────────────────────────────────┘
```

### Зависимости между слоями

- **Api** → Application, Infrastructure
- **Application** → Domain, Contracts
- **Infrastructure** → Application, Domain
- **Domain** → Contracts (только)
- **Contracts** → (независим)

## Технологический стек

### Платформа Sdf

- `Sdf.Platform.Build.Sdk` - MSBuild SDK с анализаторами
- `Sdf.Platform.Web.Sdk` - Web инфраструктура
- `Sdf.Platform.Mediator` - Mediator pattern
- `Sdf.Platform.EntityFrameworkCore.Postgresql` - EF Core для PostgreSQL
- `Sdf.Platform.Redis` - Распределенный кеш
- `Sdf.Platform.Observability.Common` - Логирование, метрики, трассировка

### Внешние библиотеки

- **Mediator.SourceGenerator** - Source generator для Mediator
- **Riok.Mapperly** - Маппинг через source generation
- **FluentValidation** - Валидация
- **Entity Framework Core 9.0** - ORM
- **xUnit** - Тестирование
- **Moq** - Моки
- **FluentAssertions** - Assertion библиотека

## Добавление новых фич

### Использование CRUD генератора

Для добавления новой сущности с CRUD операциями:

```bash
dotnet new sdf-service-crud -n YourProject.Name -E Product
```

Где `Product` - имя новой сущности.

### Ручное добавление фичи

1. Создайте папку в `Application/` для новой фичи
2. Добавьте Commands/Queries
3. Добавьте Handlers
4. Добавьте Validators
5. Добавьте Contracts в `Contracts/`
6. Зарегистрируйте endpoints в `Api/Endpoints/`
7. Добавьте миграцию (если используется БД)
8. Напишите тесты

## Публикация

### Публикация контрактов

```bash
cd src/Lowtab.Monster.Service.Contracts
dotnet pack -c Release
dotnet nuget push bin/Release/*.nupkg
```

### Публикация клиента

```bash
cd src/Lowtab.Monster.Service.Api.Client
dotnet pack -c Release
dotnet nuget push bin/Release/*.nupkg
```

## Troubleshooting

### Ошибка доступа к GitHub Packages

Убедитесь, что в `nuget.config` настроен доступ к GitHub Packages и токен актуален.

### Ошибка подключения к БД

Проверьте, что PostgreSQL запущен:

```bash
docker ps | grep postgres
```

### Порт уже занят

Измените порты в `appsettings.local.json` или `docker-compose.yml`.

