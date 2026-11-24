# TattooParlor
Пример выполнения лабораторных работ

<details>
<summary>Описание доменной области </summary>
  
В базе данных тату-салона содержится информация о клиентах, тату-мастерах и тату-сессиях.

Человек является абстрактным базовым классом иерархии наследования и содержит сведения об идентификаторе, имени, фамилии, дате рождения, номере телефона, электронной почте.

Клиент (наследник Человека) характеризуется датой первого визита, числом визитов, наличием аллергий.

Тату-мастер (наследник Человека) характеризуется датой начала работы, рейтингом, предпочитаемым стилем.

Стиль татуировки является перечислением.

Участок тела является перечислением.

Прайс по стилю является справочником и содержит сведения о стиле, цене за квадратный сантиметр, коэффициенте за многоцветность.

Прайс по участку тела является справочником и содержит сведения об участке тела и коэффициенте его стоимости.

Тату-сессия содержит информацию о клиенте, мастере, участке тела, стиле, а также о дате сессии, высоте и ширине татуировки в сантиметрах, индикаторе многоцветности, дате отмены. Стоимость сессии вычисляется на основе площади, цены стиля, коэффициента участка тела, наценки за многоцветность и скидки, если стиль совпадает с предпочитаемым стилем мастера. Аванс составляет 30% от стоимости. Невозвратность аванса определяется на основе даты отмены и даты сессии.

*Используется в качестве контракта.*

Юнит-тесты:
* Вывести все сессии, аванс по которым не подлежит возврату.
* Вывести минимальную, максимальную и среднюю стоимость татуировок по заданному стилю.
* Вывести клиентов, у которых были сессии в заданном стиле.
* Вывести топ 5 клиентов по количеству визитов за указанный период.
* Вывести мастеров с заданным предпочитаемым стилем.
* Вывести топ 5 мастеров по выручке за указанный период.
  
</details>

|Приложение| Название проекта | Шаблон проекта | Назначение проекта | Лабораторная | Описание |
|:-----------:|:-----------:|:-----------:|:-----------:|:-----------:|:------------|
|Оркестратор|[TattooParlor.AppHost](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.AppHost)|.Net Aspire Starter App|Orchestration|Лабораторная 3|Aspire-проект для определения топологии и оркестрации сервисов|
|Оркестратор|[TattooParlor.ServiceDefaults](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.ServiceDefaults)|Class Library|Orchestration|Лабораторная 3|Aspire-проект для общих настроек запускаемых сервисов|
|Сервер|[TattooParlor.Api](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.Api)|ASP.NET Core Web API|Presentation layer|Лабораторная 2|Презентационный слой с RESTful API|
|Сервер|[TattooParlor.Application](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.Application)|Class Library|Application layer|Лабораторная 2|Библиотека с аппликейшен слоем сервера|
|Сервер|[TattooParlor.Application.Contracts](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.Application.Contracts)|Class Library|Application layer|Лабораторная 2|Библиотека с контрактами|
|Сервер|[TattooParlor.Domain](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.Domain)|Class Library|Domain layer|Лабораторная 1|Библиотека с описанием доменной области сервера|
|Сервер|[TattooParlor.Domain.Shared](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.Domain.Shared)|Class Library|Domain layer|Лабораторная 1|Библиотека с общими для остальных слоев архитектуры классами|
|Генератор|[TattooParlor.Generator](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.Generator)|Class Library|Domain layer|Лабораторная 4|Библиотека с описанием доменной области генератора контрактов|
|Генератор|[TattooParlor.Generator.Grpc](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.Generator.Grpc)|ASP.NET Core Empty|Presentation Layer|Лабораторная 4|Веб приложение генератора с передачей контрактов с помощью gRPC |
|Генератор|[TattooParlor.Generator.Kafka](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.Generator.Kafka)|ASP.NET Core Empty|Presentation Layer|Лабораторная 4|Веб приложение генератора с передачей контрактов с помощью Apache Kafka |
|Генератор|[TattooParlor.Generator.Nats](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.Generator.Nats)|ASP.NET Core Empty|Presentation Layer|Лабораторная 4|Веб приложение генератора с передачей контрактов с помощью NATS |
|Генератор|[TattooParlor.Generator.RabbitMq](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.Generator.RabbitMq)|ASP.NET Core Empty|Presentation Layer|Лабораторная 4|Веб приложение генератора с передачей контрактов с помощью RabbitMQ |
|Сервер|[TattooParlor.Infrastructure.InMemory](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.Infrastructure.InMemory)|Class Library|Infrastructure layer|Лабораторная 1|Библиотека с имплементацией инфраструктурного слоя с использованием инмемори коллекций|
|Сервер|[TattooParlor.Infrastructure.EfCore](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.Infrastructure.EfCore)|Class Library|Infrastructure layer|Лабораторная 3|Библиотека с имплементацией инфраструктурного слоя с использованием Entity Framework Core|
|Сервер|[TattooParlor.Domain.Tests](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.Domain.Tests)|xUnit Test Project|Unit Tests|Лабораторная 1|Юнит-тесты доменной логики|
|Сервер|[TattooParlor.Infrastructure.Kafka](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.Infrastructure.Kafka)|Class Library|Infrastructure layer|Лабораторная 4|Библиотека с имплементацией инфраструктурных служб для Apache Kafka|
|Сервер|[TattooParlor.Infrastructure.Nats](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.Infrastructure.Nats)|Class Library|Infrastructure layer|Лабораторная 4|Библиотека с имплементацией инфраструктурных служб для NATS|
|Сервер|[TattooParlor.Infrastructure.RabbitMq](https://github.com/alxmcs/enterprise-development-samples/tree/main/TattooParlor/TattooParlor.Infrastructure.RabbitMq)|Class Library|Infrastructure layer|Лабораторная 4|Библиотека с имплементацией инфраструктурных служб для RabbitMQ|