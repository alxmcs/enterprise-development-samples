using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    patronymic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    biography = table.Column<string>(type: "text", maxLength: 2147483647, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    annotation = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: true),
                    page_count = table.Column<int>(type: "integer", nullable: true),
                    year = table.Column<int>(type: "integer", nullable: true),
                    publisher = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    isbn = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "book_authors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    book_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_authors", x => x.id);
                    table.ForeignKey(
                        name: "FK_book_authors_authors_author_id",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_book_authors_books_book_id",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "authors",
                columns: new[] { "id", "biography", "first_name", "last_name", "patronymic" },
                values: new object[,]
                {
                    { 1, null, "Гаурав", "Арораа", null },
                    { 2, null, "Джеффри", "Чилберто", null },
                    { 3, null, "Стивен", "Клири", null },
                    { 4, "Анатолий Дроздов – русскоязычный белорусский писатель, автор многочисленных книг в жанре альтернативной истории. Награжден медалью «За отличие в воинской службе» (1986), Почетной грамотой Национального собрания (Парламента) Республики Беларусь (2010).\r\n\r\nАнатолий Федорович Дроздов родился 4 мая 1955 года в городе Чаусы Могилевской области. Окончив среднюю школу, поступил на вечерний факультет Минского политехнического института. Работал слесарем-инструментальщиком на Минском авторемонтном заводе. В 1973 году призван на срочную службу в пограничные войска. Отслужив, поступил на заочное отделение Литературного института имени А. М. Горького, которое окончил в 1982 году. В 1986 году был призван из запаса и в течение шести месяцев участвовал в ликвидации последствий аварии на Чернобыльской АЭС", "Анатолий", "Дроздов", "Федорович" }
                });

            migrationBuilder.InsertData(
                table: "books",
                columns: new[] { "id", "annotation", "isbn", "page_count", "publisher", "title", "year" },
                values: new object[,]
                {
                    { 1, "Паттерны проектирования — удобный прием программирования для решения рутинных задач разработки ПО.Грамотное использование паттернов позволяет добиться соответствия любым требованиям и снизить расходы. В этой книге описаны эффективные способы применения паттернов проектирования с учётом специфики языка C# и платформы .NET Core. Кроме знакомых паттернов проектирования из книги «Банды четырех» вы изучите основы объектно-ориентированного программирования и принципов SOLID. Затем узнаете о функциональных, реактивных и конкурентных паттернах, с помощью которых будете работать с потоками и корутинами. Заключительная часть содержит паттерны для работы с микросервисными, бессерверными и облачно-ориентированными приложениями. Вы также узнаете, как сделать выбор архитектуры, например микросервисной или MVC. Вы научитесь - Повышать гибкость кода, используя принципы SOLID. - Применять разработку через тестирование (TDD) в ваших проектах на .NET Core. - Выполнять эффективную миграцию баз данных, обеспечивать долговременное хранение данных и их тестирование. - Преобразовывать консольное приложение в веб-приложение с помощью подходящего MVP. - Писать асинхронный, многопоточный и параллельный код. - Использовать парадигму MVVM и работать с RxJS и AngularJS для управления изменениями в базах данных. - Откроете для себя возможности микросервисов, бессерверного программирования и облачных вычислений.", "978-5-4461-1523-5", 352, "Питер", "Паттерны проектирования для C# и платформы .NET Core", 2021 },
                    { 2, "Большинство разработчиков настороженно относятся к конкурентному и многопоточному программированию, опасаясь проблем связанных с взаимной блокировкой, голоданием и др. Стивен Клири поможет разобраться с трудностями и избежать подводных камней, возникающих при решении реальных задач. В вашем распоряжении 85 рецептов работы с .NET и C# 8.0, необходимых для параллельной обработки и асинхронного программирования.\r\nКонкурентность уже стала общепринятым методом разработки хорошо масштабируемых приложений, но параллельное программирование остается непростой задачей. Подробные примеры и комментарии к коду позволят разобраться в том, как современные инструменты повышают уровень абстракции и упрощают конкурентное программирование.", "978-5-4461-1572-3", 304, "Питер", "Конкурентность в C#. Асинхронное, параллельное и многопоточное программирование", 2020 },
                    { 3, "После смерти пенсионер оказывается в теле молодого человека, который обладает необычным даром. Как все помнят, в девяностые годы был расцвет моды на экстрасенсов и нетрадиционные методы лечения. Разница в том, что герой не шарлатан и может исцелять реально, причём даже безнадёжно больных. Наличие таких способностей – золотая жила. Вот только герой не намерен монетизировать свой дар.\r\n\r\nБеда в том, что некоторые по-хорошему не понимают… Ну что же, кто хотел войны, тот её получит. Экстрасенс разбушевался.", "978-5-04-421261-9", 300, "1С-Паблишинг", "Экстрасенс разбушевался", 2022 },
                    { 4, "Двое дезертиров сбежали из воинской части в Кишиневе. Они убили часового, захватили оружие и угнали автомобиль. По тревоге поднимают пограничников, чтобы найти и схватить дезертиров. В один из отрядов попадает Антон Ильин. И ему все-таки удается найти беглецов. Началась перестрелка, а один из дезертиров подрывает Ильина гранатой вместе с автомобилем, набитым оружием. В таких передрягах, увы, не выживают…\r\n\r\nУдивительно, но Антон Ильин не умер. Точнее, умер, но не исчез бесследно. Он оказался на незнакомой планете Нимей, в государстве Рум. Это совершенно невероятное место, населенное орками и прочими нелюдями. Жить здесь – не лучшая перспектива для пограничника с Земли, но всяко лучше, чем сгинуть навсегда. Придется приспосабливаться. А там, глядишь, найдется способ вернуться домой к отцу и сестре. По крайней мере, он сделает для этого все возможное.", "978-5-04-421261-9", 310, "1С-Паблишинг", "Не плачь, орчанка!", 2020 }
                });

            migrationBuilder.InsertData(
                table: "book_authors",
                columns: new[] { "id", "author_id", "book_id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 2 },
                    { 4, 4, 3 },
                    { 5, 4, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_book_authors_author_id",
                table: "book_authors",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_book_authors_book_id",
                table: "book_authors",
                column: "book_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book_authors");

            migrationBuilder.DropTable(
                name: "authors");

            migrationBuilder.DropTable(
                name: "books");
        }
    }
}
