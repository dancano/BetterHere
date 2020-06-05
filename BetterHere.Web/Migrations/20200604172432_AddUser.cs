using Microsoft.EntityFrameworkCore.Migrations;

namespace BetterHere.Web.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Foods_FoodName_EstablishmentId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "EstablishmentLocations");

            migrationBuilder.RenameColumn(
                name: "EstablishmentId",
                table: "Foods",
                newName: "EstablishmentLocationId");

            migrationBuilder.AddColumn<int>(
                name: "LoginType",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_FoodName_EstablishmentLocationId",
                table: "Foods",
                columns: new[] { "FoodName", "EstablishmentLocationId" },
                unique: true,
                filter: "[FoodName] IS NOT NULL AND [EstablishmentLocationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EstablishmentLocations_SourceLatitude_SourceLongitude",
                table: "EstablishmentLocations",
                columns: new[] { "SourceLatitude", "SourceLongitude" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Foods_FoodName_EstablishmentLocationId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_EstablishmentLocations_SourceLatitude_SourceLongitude",
                table: "EstablishmentLocations");

            migrationBuilder.DropColumn(
                name: "LoginType",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "EstablishmentLocationId",
                table: "Foods",
                newName: "EstablishmentId");

            migrationBuilder.AddColumn<float>(
                name: "Qualification",
                table: "EstablishmentLocations",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_FoodName_EstablishmentId",
                table: "Foods",
                columns: new[] { "FoodName", "EstablishmentId" },
                unique: true,
                filter: "[FoodName] IS NOT NULL AND [EstablishmentId] IS NOT NULL");
        }
    }
}
