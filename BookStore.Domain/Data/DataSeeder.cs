﻿using BookStore.Domain.Model;
using System.Collections.Generic;

namespace BookStore.Domain.Data;

/// <summary>
/// Приведенные в этом классе данные никаким образом не являются моей рекомендацией к прочтению
/// Это просто рандомные книги из интернета
/// </summary>
public static class DataSeeder 
{
    public static readonly List<Book> Books = [
            new Book
            {
                Id =1,
                Title = "Паттерны проектирования для C# и платформы .NET Core",
                Year = 2021,
                PageCount = 352,
                Isbn = "978-5-4461-1523-5",
                Publisher = "Питер",
                Annotation = "Паттерны проектирования — удобный прием программирования для решения рутинных задач разработки ПО." +
                    "Грамотное использование паттернов позволяет добиться соответствия любым требованиям и снизить расходы. В эт" +
                    "ой книге описаны эффективные способы применения паттернов проектирования с учётом специфики языка C# и плат" +
                    "формы .NET Core. Кроме знакомых паттернов проектирования из книги «Банды четырех» вы изучите основы объектн" +
                    "о-ориентированного программирования и принципов SOLID. Затем узнаете о функциональных, реактивных и конкуре" +
                    "нтных паттернах, с помощью которых будете работать с потоками и корутинами. Заключительная часть содержит п" +
                    "аттерны для работы с микросервисными, бессерверными и облачно-ориентированными приложениями. Вы также узнае" +
                    "те, как сделать выбор архитектуры, например микросервисной или MVC. Вы научитесь - Повышать гибкость кода, " +
                    "используя принципы SOLID. - Применять разработку через тестирование (TDD) в ваших проектах на .NET Core. - " +
                    "Выполнять эффективную миграцию баз данных, обеспечивать долговременное хранение данных и их тестирование. -" +
                    " Преобразовывать консольное приложение в веб-приложение с помощью подходящего MVP. - Писать асинхронный, мн" +
                    "огопоточный и параллельный код. - Использовать парадигму MVVM и работать с RxJS и AngularJS для управления " +
                    "изменениями в базах данных. - Откроете для себя возможности микросервисов, бессерверного программирования и" +
                    " облачных вычислений."
            },
            new Book
            {
                Id = 2,
                Title = "Конкурентность в C#. Асинхронное, параллельное и многопоточное программирование",
                Year = 2020,
                PageCount = 304,
                Publisher = "Питер",
                Isbn = "978-5-4461-1572-3",
                Annotation = "Большинство разработчиков настороженно относятся к конкурентному и многопоточному программированию," +
                    " опасаясь проблем связанных с взаимной блокировкой, голоданием и др. Стивен Клири поможет разобраться с труд" +
                    "ностями и избежать подводных камней, возникающих при решении реальных задач. В вашем распоряжении 85 рецепто" +
                    "в работы с .NET и C# 8.0, необходимых для параллельной обработки и асинхронного программирования.\r\nКонкуре" +
                    "нтность уже стала общепринятым методом разработки хорошо масштабируемых приложений, но параллельное программ" +
                    "ирование остается непростой задачей. Подробные примеры и комментарии к коду позволят разобраться в том, как " +
                    "современные инструменты повышают уровень абстракции и упрощают конкурентное программирование."
            },
            new Book
            {
                Id = 3,
                Title = "Экстрасенс разбушевался",
                Year = 2022,
                PageCount = 300,
                Isbn = "978-5-04-421261-9",
                Publisher = "1С-Паблишинг",
                Annotation = "После смерти пенсионер оказывается в теле молодого человека, который обладает необычным даром. Как " +
                    "все помнят, в девяностые годы был расцвет моды на экстрасенсов и нетрадиционные методы лечения. Разница в то" +
                    "м, что герой не шарлатан и может исцелять реально, причём даже безнадёжно больных. Наличие таких способносте" +
                    "й – золотая жила. Вот только герой не намерен монетизировать свой дар.\r\n\r\nБеда в том, что некоторые по-х" +
                    "орошему не понимают… Ну что же, кто хотел войны, тот её получит. Экстрасенс разбушевался."
            },
            new Book
            {
                Id = 4,
                Title = "Не плачь, орчанка!",
                Year = 2020,
                PageCount = 310,
                Isbn = "978-5-04-421261-9",
                Publisher = "1С-Паблишинг",
                Annotation = "Двое дезертиров сбежали из воинской части в Кишиневе. Они убили часового, захватили оружие и угнали" +
                " автомобиль. По тревоге поднимают пограничников, чтобы найти и схватить дезертиров. В один из отрядов попадает А" +
                "нтон Ильин. И ему все-таки удается найти беглецов. Началась перестрелка, а один из дезертиров подрывает Ильина г" +
                "ранатой вместе с автомобилем, набитым оружием. В таких передрягах, увы, не выживают…\r\n\r\nУдивительно, но Анто" +
                "н Ильин не умер. Точнее, умер, но не исчез бесследно. Он оказался на незнакомой планете Нимей, в государстве Рум" +
                ". Это совершенно невероятное место, населенное орками и прочими нелюдями. Жить здесь – не лучшая перспектива для" +
                " пограничника с Земли, но всяко лучше, чем сгинуть навсегда. Придется приспосабливаться. А там, глядишь, найдетс" +
                "я способ вернуться домой к отцу и сестре. По крайней мере, он сделает для этого все возможное."
            }];

    public static readonly List<Author> Authors = [
            new Author
            {
                Id = 1,
                FirstName = "Гаурав",
                LastName = "Арораа",
            },
            new Author
            {
                Id = 2,
                FirstName = "Джеффри",
                LastName = "Чилберто",
            },
            new Author
            {
                Id = 3,
                FirstName = "Стивен",
                LastName = "Клири",
            },
            new Author
            {
                Id = 4,
                LastName = "Дроздов",
                FirstName = "Анатолий",
                Patronymic="Федорович",
                Biography = "Анатолий Дроздов – русскоязычный белорусский писатель, автор многочисленных книг в жанре альтернатив" +
                    "ной истории. Награжден медалью «За отличие в воинской службе» (1986), Почетной грамотой Национального собран" +
                    "ия (Парламента) Республики Беларусь (2010).\r\n\r\nАнатолий Федорович Дроздов родился 4 мая 1955 года в горо" +
                    "де Чаусы Могилевской области. Окончив среднюю школу, поступил на вечерний факультет Минского политехническог" +
                    "о института. Работал слесарем-инструментальщиком на Минском авторемонтном заводе. В 1973 году призван на сро" +
                    "чную службу в пограничные войска. Отслужив, поступил на заочное отделение Литературного института имени А. М" +
                    ". Горького, которое окончил в 1982 году. В 1986 году был призван из запаса и в течение шести месяцев участво" +
                    "вал в ликвидации последствий аварии на Чернобыльской АЭС"
            }];

    public static readonly List<BookAuthor> BookAuthors = [
            new BookAuthor
            {
                 Id = 1,
                 AuthorId = 1,
                 BookId = 1,
            },
            new BookAuthor
            {
                 Id = 2,
                 AuthorId = 2,
                 BookId = 1,
            },
            new BookAuthor
            {
                 Id = 3,
                 AuthorId = 3,
                 BookId = 2,
            },
            new BookAuthor
            {
                 Id = 4,
                 AuthorId = 4,
                 BookId = 3,
            },
            new BookAuthor
            {
                 Id = 5,
                 AuthorId = 4,
                 BookId = 4,
            }];
}