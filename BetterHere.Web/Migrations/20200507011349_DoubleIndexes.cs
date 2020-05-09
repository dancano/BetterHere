using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BetterHere.Web.Migrations
{
    public partial class DoubleIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FoodType",
                table: "TypeFoods",
                newName: "FoodTypeName");

            migrationBuilder.AlterColumn<string>(
                name: "FoodName",
                table: "Foods",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstablishmentEntitiesId",
                table: "Foods",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstablishmentId",
                table: "Foods",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CitiesId",
                table: "EstablishmentLocations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foods_EstablishmentEntitiesId",
                table: "Foods",
                column: "EstablishmentEntitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_FoodName_EstablishmentId",
                table: "Foods",
                columns: new[] { "FoodName", "EstablishmentId" },
                unique: true,
                filter: "[FoodName] IS NOT NULL AND [EstablishmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EstablishmentLocations_CitiesId",
                table: "EstablishmentLocations",
                column: "CitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstablishmentLocations_Cities_CitiesId",
                table: "EstablishmentLocations",
                column: "CitiesId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Establishments_EstablishmentEntitiesId",
                table: "Foods",
                column: "EstablishmentEntitiesId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstablishmentLocations_Cities_CitiesId",
                table: "EstablishmentLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Establishments_EstablishmentEntitiesId",
                table: "Foods");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Foods_EstablishmentEntitiesId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_FoodName_EstablishmentId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_EstablishmentLocations_CitiesId",
                table: "EstablishmentLocations");

            migrationBuilder.DropColumn(
                name: "EstablishmentEntitiesId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "EstablishmentId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "CitiesId",
                table: "EstablishmentLocations");

            migrationBuilder.RenameColumn(
                name: "FoodTypeName",
                table: "TypeFoods",
                newName: "FoodType");

            migrationBuilder.AlterColumn<string>(
                name: "FoodName",
                table: "Foods",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
