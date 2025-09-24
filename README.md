# enterprise-development-samples
Код с лекций по предмету "Разработка корпоративных приложений" для 4 курса ИБАС

|Приложение| Название проекта | Тип проекта | Назначение проекта | Лабораторная | Описание |
|:-----------:|:-----------:|:-----------:|:-----------:|:-----------:|:------------|
|Оркестратор|BookStore.AppHost|.Net Aspire Starter App|Orchestration|Лабораторная 3|Aspire-проект для определения топологии и оркестрации сервисов|
|Оркестратор|BookStore.ServiceDefaults|Class Library|Orchestration|Лабораторная 3|Aspire-проект для общих настроек запускаемых сервисов|
|Сервер|BookStore.Api.Host|ASP.NET Core Web API|Presentation Layer|Лабораторная 2|Веб приложение с API-эндпоинтами сервера |
|Сервер, Клиент, Генератор|BookStore.Application.Contracts|Class Library|Application Layer|Лабораторная 2|Библиотека с контрактами|
|Сервер|BookStore.Application|Class Library|Application Layer|Лабораторная 2|Бибилотека со службами, запускающими use cases в доменном слое|
|Сервер|BookStore.Domain|Class Library|Domain layer|Лабораторная 1|Библиотека с описанием доменной области сервера|
|Сервер|BookStore.Infrastructure.EfCore|Class Library|Infrastructure layer|Лабораторная 3|Библиотека с имплементацией инфраструктурных служб для Entity Framework Core|
|Сервер|BookStore.Infrastructure.InMemory|Class Library|Infrastructure layer|Лабораторная 1|Библиотека с имплементацией инфраструктурных служб с использованием инмемори коллекций|
|Сервер|BookStore.Domain.Tests|xUnit Test Project|Unit Tests|Лабораторная 1|Юнит-тесты доменной логики|
|Сервер|BookStore.Infrastructure.Kafka|Class Library|Infrastructure layer|Лабораторная 4|Библиотека с имплементацией инфраструктурных служб для Apache Kafka|
|Сервер|BookStore.Infrastructure.Nats|Class Library|Infrastructure layer|Лабораторная 4|Библиотека с имплементацией инфраструктурных служб для NATS|
|Сервер|BookStore.Infrastructure.RabbitMq|Class Library|Infrastructure layer|Лабораторная 4|Библиотека с имплементацией инфраструктурных служб для RabbitMQ|
|Генератор|BookStore.Generator|Class Library|Domain layer|Лабораторная 4|Библиотека с описанием доменной области генератора контрактов|
|Генератор|BookStore.Generator.Grpc.Host|ASP.NET Core Empty|Presentation Layer|Лабораторная 4|Веб приложение генератора с передачей контрактов с помощью gRPC |
|Генератор|BookStore.Generator.Kafka.Host|ASP.NET Core Empty|Presentation Layer|Лабораторная 4|Веб приложение генератора с передачей контрактов с помощью Apache Kafka |
|Генератор|BookStore.Generator.Nats.Host|ASP.NET Core Empty|Presentation Layer|Лабораторная 4|Веб приложение генератора с передачей контрактов с помощью NATS |
|Генератор|BookStore.Generator.RabbitMq.Host|ASP.NET Core Empty|Presentation Layer|Лабораторная 4|Веб приложение генератора с передачей контрактов с помощью RabbitMQ |
|Клиент|BookStore.Wasm|Blazor Web Assembly Standalone App|Client|Лабораторная 5|Клиентское приложение|
