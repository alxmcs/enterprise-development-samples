# Enterprise Development Samples
Код с лекций по предмету "Разработка корпоративных приложений" для 4 курса ИБАС

## Пример запуска Blazor в Combined Mode
[Sample.Server](https://github.com/alxmcs/enterprise-development-samples/tree/main/Sample.Server) - проект Blazor Server с обоими видами интерактивности   
[Sample.Wasm](https://github.com/alxmcs/enterprise-development-samples/tree/main/Sample.Wasm) - проект Blazor Wasm, сконфигурированный как клиент Sample.Server  

## Простейший пример serverless Blazor Wasm приложения
[Winx.Wasm](https://github.com/alxmcs/enterprise-development-samples/tree/main/Winx.Wasm) - standalone Blazor Wasm приложение, на примере которого демонстрировалась публикация в S3

## Пример выполненных лабораторных [работ по курсу](https://github.com/itsecd/enterprise-development)

|Onion Layer|Проект|Тип|Описание|
|:-----------:|:-----------:|:-----------:|:------------|
|Domain|[BookStore.Domain](https://github.com/alxmcs/enterprise-development-samples/tree/main/BookStore.Domain)|Class library|Содержит доменные классы|
|Application|[BookStore.Contracts](https://github.com/alxmcs/enterprise-development-samples/tree/main/BookStore.Contracts)|Class library|Содержит dto и интерфейсы служб|
|Application|[BookStore.Application](https://github.com/alxmcs/enterprise-development-samples/tree/main/BookStore.Application)|Class library|Содержит имплементацию аппликейшен служб и профайл автомаппера|
|Persistence|[BookStore.EfCore](https://github.com/alxmcs/enterprise-development-samples/tree/main/BookStore.EfCore)|Class library|Содержит настройки EF Core контекста|
|Presentation|[BookStore.WebApi.Host](https://github.com/alxmcs/enterprise-development-samples/tree/main/BookStore.WebApi.Host)|Class library|Содержит контроллеры по Open API стандарту|
|UI|[BookStore.WebApi.Client](https://github.com/alxmcs/enterprise-development-samples/tree/main/BookStore.WebApi.Client)|Class library|Blazor WASM приложение с Open API клиентом|
