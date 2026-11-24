# GrpcExample

Пример сервисов, взаимодействующих через gRPC (`.NET 10`, `.NET Aspire 13`)

|Приложение| Название проекта | Шаблон проекта |  Описание |
|:-----------:|:-----------:|:-----------:|:------------|
|Оркестратор|[GrpcExample.AppHost](GrpcExample.AppHost)|.Net Aspire Starter App|Aspire-проект для определения топологии и оркестрации сервисов|
|Оркестратор|[GrpcExample.ServiceDefaults](GrpcExample.ServiceDefaults)|Class Library|Aspire-проект для общих настроек запускаемых сервисов|
|gRPC Сервер|[GrpcExample.Server](GrpcExample.Server)|ASP.NET Core Empty|Приложение с серверной частью gRPC|
|gRPC Клиент|[GrpcExample.Client](GrpcExample.Client)|ASP.NET Core Empty|Приложение с клиентской частью gRPC|
