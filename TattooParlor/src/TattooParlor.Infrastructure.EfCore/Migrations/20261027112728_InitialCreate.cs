using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TattooParlor.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tp");

            migrationBuilder.CreateTable(
                name: "body_zone_pricings",
                schema: "tp",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    body_zone = table.Column<int>(type: "integer", nullable: false),
                    multiplier = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_body_zone_pricings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                schema: "tp",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_visit_date = table.Column<DateOnly>(type: "date", nullable: false),
                    visit_count = table.Column<int>(type: "integer", nullable: false),
                    has_allergies = table.Column<bool>(type: "boolean", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "style_pricings",
                schema: "tp",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    style = table.Column<int>(type: "integer", nullable: false),
                    price_per_sq_cm = table.Column<decimal>(type: "numeric", nullable: false),
                    multicolor_multiplier = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_style_pricings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tattoo_artists",
                schema: "tp",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    work_start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    rating = table.Column<double>(type: "double precision", nullable: false),
                    favorite_style = table.Column<int>(type: "integer", nullable: true),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tattoo_artists", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tattoo_sessions",
                schema: "tp",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    tattoo_artist_id = table.Column<int>(type: "integer", nullable: false),
                    session_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    body_zone_id = table.Column<int>(type: "integer", nullable: false),
                    style_id = table.Column<int>(type: "integer", nullable: false),
                    height_in_cm = table.Column<double>(type: "double precision", nullable: false),
                    width_in_cm = table.Column<double>(type: "double precision", nullable: false),
                    is_multicolor = table.Column<bool>(type: "boolean", nullable: false),
                    cancel_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tattoo_sessions", x => x.id);
                    table.ForeignKey(
                        name: "FK_tattoo_sessions_body_zone_pricings_body_zone_id",
                        column: x => x.body_zone_id,
                        principalSchema: "tp",
                        principalTable: "body_zone_pricings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tattoo_sessions_customers_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "tp",
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tattoo_sessions_style_pricings_style_id",
                        column: x => x.style_id,
                        principalSchema: "tp",
                        principalTable: "style_pricings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tattoo_sessions_tattoo_artists_tattoo_artist_id",
                        column: x => x.tattoo_artist_id,
                        principalSchema: "tp",
                        principalTable: "tattoo_artists",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "tp",
                table: "body_zone_pricings",
                columns: new[] { "id", "body_zone", "multiplier" },
                values: new object[,]
                {
                    { 1, 0, 1.0m },
                    { 2, 1, 1.1m },
                    { 3, 2, 1.0m },
                    { 4, 3, 1.3m },
                    { 5, 4, 1.2m },
                    { 6, 5, 1.2m },
                    { 7, 6, 1.1m },
                    { 8, 7, 1.0m },
                    { 9, 9, 2.0m },
                    { 10, 10, 1.4m },
                    { 11, 8, 1.0m }
                });

            migrationBuilder.InsertData(
                schema: "tp",
                table: "customers",
                columns: new[] { "id", "date_of_birth", "email", "first_name", "first_visit_date", "has_allergies", "last_name", "phone_number", "visit_count" },
                values: new object[,]
                {
                    { 1, new DateOnly(1990, 2, 10), "alexey.petrov@mail.com", "Alexey", new DateOnly(2021, 5, 3), false, "Petrov", "+79161112233", 4 },
                    { 2, new DateOnly(1994, 8, 25), "olga.ivanova@mail.com", "Olga", new DateOnly(2022, 1, 17), true, "Ivanova", "+79161112234", 2 },
                    { 3, new DateOnly(1987, 12, 1), "sergey.smirnov@mail.com", "Sergey", new DateOnly(2020, 9, 12), false, "Smirnov", "+79161112235", 7 },
                    { 4, new DateOnly(1998, 4, 6), "ekaterina.novikova@mail.com", "Ekaterina", new DateOnly(2023, 3, 21), false, "Novikova", "+79161112236", 1 },
                    { 5, new DateOnly(1983, 6, 19), "dmitry.fedorov@mail.com", "Dmitry", new DateOnly(2019, 11, 30), true, "Fedorov", "+79161112237", 10 },
                    { 6, new DateOnly(2000, 9, 9), "yulia.morozova@mail.com", "Yulia", new DateOnly(2023, 7, 8), false, "Morozova", "+79161112238", 1 }
                });

            migrationBuilder.InsertData(
                schema: "tp",
                table: "style_pricings",
                columns: new[] { "id", "multicolor_multiplier", "price_per_sq_cm", "style" },
                values: new object[,]
                {
                    { 1, 1.5m, 25m, 0 },
                    { 2, 1.8m, 30m, 1 },
                    { 3, null, 20m, 2 },
                    { 4, null, 22m, 3 },
                    { 5, 1.3m, 18m, 4 },
                    { 6, null, 15m, 5 },
                    { 7, null, 19m, 8 }
                });

            migrationBuilder.InsertData(
                schema: "tp",
                table: "tattoo_artists",
                columns: new[] { "id", "date_of_birth", "email", "favorite_style", "first_name", "last_name", "phone_number", "rating", "work_start_date" },
                values: new object[,]
                {
                    { 1, new DateOnly(1988, 3, 14), "igor.volkov@tattooparlor.com", 0, "Igor", "Volkov", "+79261234567", 4.7999999999999998, new DateOnly(2012, 6, 1) },
                    { 2, new DateOnly(1992, 7, 22), "marina.sokolova@tattooparlor.com", 1, "Marina", "Sokolova", "+79261234568", 4.5999999999999996, new DateOnly(2016, 2, 15) },
                    { 3, new DateOnly(1985, 11, 3), "denis.orlov@tattooparlor.com", 2, "Denis", "Orlov", "+79261234569", 4.9000000000000004, new DateOnly(2009, 9, 10) },
                    { 4, new DateOnly(1995, 1, 30), "anna.belova@tattooparlor.com", null, "Anna", "Belova", "+79261234570", 4.2999999999999998, new DateOnly(2020, 4, 5) },
                    { 5, new DateOnly(1990, 5, 18), "pavel.kuznetsov@tattooparlor.com", 3, "Pavel", "Kuznetsov", "+79261234571", 4.7000000000000002, new DateOnly(2014, 8, 20) }
                });

            migrationBuilder.InsertData(
                schema: "tp",
                table: "tattoo_sessions",
                columns: new[] { "id", "body_zone_id", "cancel_date", "customer_id", "height_in_cm", "is_multicolor", "session_date", "style_id", "tattoo_artist_id", "width_in_cm" },
                values: new object[,]
                {
                    { new Guid("39c97262-44ce-4b30-92e2-0f0e8a168e54"), 1, null, 1, 10.0, false, new DateTime(2026, 1, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 8.0 },
                    { new Guid("39c97262-44ce-4b30-92e2-0f0e8a168e55"), 4, null, 2, 15.0, false, new DateTime(2026, 2, 5, 11, 30, 0, 0, DateTimeKind.Unspecified), 2, 2, 12.0 },
                    { new Guid("39c97262-44ce-4b30-92e2-0f0e8a168e56"), 10, null, 3, 20.0, false, new DateTime(2026, 2, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 20.0 },
                    { new Guid("39c97262-44ce-4b30-92e2-0f0e8a168e57"), 7, null, 4, 8.0, false, new DateTime(2026, 3, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, 6.0 },
                    { new Guid("39c97262-44ce-4b30-92e2-0f0e8a168e58"), 2, null, 5, 12.0, false, new DateTime(2026, 3, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), 4, 5, 9.0 },
                    { new Guid("39c97262-44ce-4b30-92e2-0f0e8a168e59"), 5, new DateTime(2026, 3, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 18.0, true, new DateTime(2026, 4, 2, 15, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 14.0 },
                    { new Guid("39c97262-44ce-4b30-92e2-0f0e8a168e5a"), 8, new DateTime(2026, 4, 19, 18, 0, 0, 0, DateTimeKind.Unspecified), 6, 6.0, false, new DateTime(2026, 4, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 7, 3, 5.0 },
                    { new Guid("39c97262-44ce-4b30-92e2-0f0e8a168e5b"), 1, null, 3, 25.0, true, new DateTime(2026, 5, 5, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 18.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tattoo_sessions_body_zone_id",
                schema: "tp",
                table: "tattoo_sessions",
                column: "body_zone_id");

            migrationBuilder.CreateIndex(
                name: "IX_tattoo_sessions_customer_id",
                schema: "tp",
                table: "tattoo_sessions",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_tattoo_sessions_style_id",
                schema: "tp",
                table: "tattoo_sessions",
                column: "style_id");

            migrationBuilder.CreateIndex(
                name: "IX_tattoo_sessions_tattoo_artist_id",
                schema: "tp",
                table: "tattoo_sessions",
                column: "tattoo_artist_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tattoo_sessions",
                schema: "tp");

            migrationBuilder.DropTable(
                name: "body_zone_pricings",
                schema: "tp");

            migrationBuilder.DropTable(
                name: "customers",
                schema: "tp");

            migrationBuilder.DropTable(
                name: "style_pricings",
                schema: "tp");

            migrationBuilder.DropTable(
                name: "tattoo_artists",
                schema: "tp");
        }
    }
}
