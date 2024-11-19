﻿// <auto-generated />
using System;
using BookStore.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookStore.EfCore.Migrations
{
    [DbContext(typeof(BookStoreDbContext))]
    [Migration("20241116094842_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("BookStore.Domain.Model.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Biography")
                        .HasMaxLength(2147483647)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Гаурав",
                            LastName = "Арораа"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Джеффри",
                            LastName = "Чилберто"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Стивен",
                            LastName = "Клири"
                        },
                        new
                        {
                            Id = 4,
                            Biography = "Анатолий Дроздов – русскоязычный белорусский писатель, автор многочисленных книг в жанре альтернативной истории. Награжден медалью «За отличие в воинской службе» (1986), Почетной грамотой Национального собрания (Парламента) Республики Беларусь (2010).\r\n\r\nАнатолий Федорович Дроздов родился 4 мая 1955 года в городе Чаусы Могилевской области. Окончив среднюю школу, поступил на вечерний факультет Минского политехнического института. Работал слесарем-инструментальщиком на Минском авторемонтном заводе. В 1973 году призван на срочную службу в пограничные войска. Отслужив, поступил на заочное отделение Литературного института имени А. М. Горького, которое окончил в 1982 году. В 1986 году был призван из запаса и в течение шести месяцев участвовал в ликвидации последствий аварии на Чернобыльской АЭС",
                            FirstName = "Анатолий",
                            LastName = "Дроздов",
                            Patronymic = "Федорович"
                        });
                });

            modelBuilder.Entity("BookStore.Domain.Model.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Annotation")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Isbn")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PageCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Publisher")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<int?>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Annotation = "Паттерны проектирования — удобный прием программирования для решения рутинных задач разработки ПО.Грамотное использование паттернов позволяет добиться соответствия любым требованиям и снизить расходы. В этой книге описаны эффективные способы применения паттернов проектирования с учётом специфики языка C# и платформы .NET Core. Кроме знакомых паттернов проектирования из книги «Банды четырех» вы изучите основы объектно-ориентированного программирования и принципов SOLID. Затем узнаете о функциональных, реактивных и конкурентных паттернах, с помощью которых будете работать с потоками и корутинами. Заключительная часть содержит паттерны для работы с микросервисными, бессерверными и облачно-ориентированными приложениями. Вы также узнаете, как сделать выбор архитектуры, например микросервисной или MVC. Вы научитесь - Повышать гибкость кода, используя принципы SOLID. - Применять разработку через тестирование (TDD) в ваших проектах на .NET Core. - Выполнять эффективную миграцию баз данных, обеспечивать долговременное хранение данных и их тестирование. - Преобразовывать консольное приложение в веб-приложение с помощью подходящего MVP. - Писать асинхронный, многопоточный и параллельный код. - Использовать парадигму MVVM и работать с RxJS и AngularJS для управления изменениями в базах данных. - Откроете для себя возможности микросервисов, бессерверного программирования и облачных вычислений.",
                            Isbn = "978-5-4461-1523-5",
                            PageCount = 352,
                            Publisher = "Питер",
                            Title = "Паттерны проектирования для C# и платформы .NET Core",
                            Year = 2021
                        },
                        new
                        {
                            Id = 2,
                            Annotation = "Большинство разработчиков настороженно относятся к конкурентному и многопоточному программированию, опасаясь проблем связанных с взаимной блокировкой, голоданием и др. Стивен Клири поможет разобраться с трудностями и избежать подводных камней, возникающих при решении реальных задач. В вашем распоряжении 85 рецептов работы с .NET и C# 8.0, необходимых для параллельной обработки и асинхронного программирования.\r\nКонкурентность уже стала общепринятым методом разработки хорошо масштабируемых приложений, но параллельное программирование остается непростой задачей. Подробные примеры и комментарии к коду позволят разобраться в том, как современные инструменты повышают уровень абстракции и упрощают конкурентное программирование.",
                            Isbn = "978-5-4461-1572-3",
                            PageCount = 304,
                            Publisher = "Питер",
                            Title = "Конкурентность в C#. Асинхронное, параллельное и многопоточное программирование",
                            Year = 2020
                        },
                        new
                        {
                            Id = 3,
                            Annotation = "После смерти пенсионер оказывается в теле молодого человека, который обладает необычным даром. Как все помнят, в девяностые годы был расцвет моды на экстрасенсов и нетрадиционные методы лечения. Разница в том, что герой не шарлатан и может исцелять реально, причём даже безнадёжно больных. Наличие таких способностей – золотая жила. Вот только герой не намерен монетизировать свой дар.\r\n\r\nБеда в том, что некоторые по-хорошему не понимают… Ну что же, кто хотел войны, тот её получит. Экстрасенс разбушевался.",
                            Isbn = "978-5-04-421261-9",
                            PageCount = 300,
                            Publisher = "1С-Паблишинг",
                            Title = "Экстрасенс разбушевался",
                            Year = 2022
                        },
                        new
                        {
                            Id = 4,
                            Annotation = "Двое дезертиров сбежали из воинской части в Кишиневе. Они убили часового, захватили оружие и угнали автомобиль. По тревоге поднимают пограничников, чтобы найти и схватить дезертиров. В один из отрядов попадает Антон Ильин. И ему все-таки удается найти беглецов. Началась перестрелка, а один из дезертиров подрывает Ильина гранатой вместе с автомобилем, набитым оружием. В таких передрягах, увы, не выживают…\r\n\r\nУдивительно, но Антон Ильин не умер. Точнее, умер, но не исчез бесследно. Он оказался на незнакомой планете Нимей, в государстве Рум. Это совершенно невероятное место, населенное орками и прочими нелюдями. Жить здесь – не лучшая перспектива для пограничника с Земли, но всяко лучше, чем сгинуть навсегда. Придется приспосабливаться. А там, глядишь, найдется способ вернуться домой к отцу и сестре. По крайней мере, он сделает для этого все возможное.",
                            Isbn = "978-5-04-421261-9",
                            PageCount = 310,
                            Publisher = "1С-Паблишинг",
                            Title = "Не плачь, орчанка!",
                            Year = 2020
                        });
                });

            modelBuilder.Entity("BookStore.Domain.Model.BookAuthor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookId");

                    b.ToTable("BookAuthors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            BookId = 1
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 2,
                            BookId = 1
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 3,
                            BookId = 2
                        },
                        new
                        {
                            Id = 4,
                            AuthorId = 4,
                            BookId = 3
                        },
                        new
                        {
                            Id = 5,
                            AuthorId = 4,
                            BookId = 4
                        });
                });

            modelBuilder.Entity("BookStore.Domain.Model.BookAuthor", b =>
                {
                    b.HasOne("BookStore.Domain.Model.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookStore.Domain.Model.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("BookStore.Domain.Model.Author", b =>
                {
                    b.Navigation("BookAuthors");
                });

            modelBuilder.Entity("BookStore.Domain.Model.Book", b =>
                {
                    b.Navigation("BookAuthors");
                });
#pragma warning restore 612, 618
        }
    }
}