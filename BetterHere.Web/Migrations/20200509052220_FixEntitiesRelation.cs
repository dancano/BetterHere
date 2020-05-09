using Microsoft.EntityFrameworkCore.Migrations;

namespace BetterHere.Web.Migrations
{
    public partial class FixEntitiesRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Establishments_TypeEstablishments_TypeEstablishmentId",
                table: "Establishments");

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Establishments_EstablishmentEntitiesId",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeFoods_Establishments_EstablishmentEntityId",
                table: "TypeFoods");

            migrationBuilder.DropIndex(
                name: "IX_TypeFoods_EstablishmentEntityId",
                table: "TypeFoods");

            migrationBuilder.DropIndex(
                name: "IX_Establishments_TypeEstablishmentId",
                table: "Establishments");

            migrationBuilder.DropColumn(
                name: "EstablishmentEntityId",
                table: "TypeFoods");

            migrationBuilder.DropColumn(
                name: "TypeEstablishmentId",
                table: "Establishments");

            migrationBuilder.RenameColumn(
                name: "EstablishmentEntitiesId",
                table: "Foods",
                newName: "EstablishmentLocationsId");

            migrationBuilder.RenameIndex(
                name: "IX_Foods_EstablishmentEntitiesId",
                table: "Foods",
                newName: "IX_Foods_EstablishmentLocationsId");

            migrationBuilder.AddColumn<int>(
                name: "TypeEstablishmentId",
                table: "EstablishmentLocations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstablishmentLocations_TypeEstablishmentId",
                table: "EstablishmentLocations",
                column: "TypeEstablishmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstablishmentLocations_TypeEstablishments_TypeEstablishmentId",
                table: "EstablishmentLocations",
                column: "TypeEstablishmentId",
                principalTable: "TypeEstablishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_EstablishmentLocations_EstablishmentLocationsId",
                table: "Foods",
                column: "EstablishmentLocationsId",
                principalTable: "EstablishmentLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstablishmentLocations_TypeEstablishments_TypeEstablishmentId",
                table: "EstablishmentLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_EstablishmentLocations_EstablishmentLocationsId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_EstablishmentLocations_TypeEstablishmentId",
                table: "EstablishmentLocations");

            migrationBuilder.DropColumn(
                name: "TypeEstablishmentId",
                table: "EstablishmentLocations");

            migrationBuilder.RenameColumn(
                name: "EstablishmentLocationsId",
                table: "Foods",
                newName: "EstablishmentEntitiesId");

            migrationBuilder.RenameIndex(
                name: "IX_Foods_EstablishmentLocationsId",
                table: "Foods",
                newName: "IX_Foods_EstablishmentEntitiesId");

            migrationBuilder.AddColumn<int>(
                name: "EstablishmentEntityId",
                table: "TypeFoods",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeEstablishmentId",
                table: "Establishments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeFoods_EstablishmentEntityId",
                table: "TypeFoods",
                column: "EstablishmentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Establishments_TypeEstablishmentId",
                table: "Establishments",
                column: "TypeEstablishmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Establishments_TypeEstablishments_TypeEstablishmentId",
                table: "Establishments",
                column: "TypeEstablishmentId",
                principalTable: "TypeEstablishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Establishments_EstablishmentEntitiesId",
                table: "Foods",
                column: "EstablishmentEntitiesId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeFoods_Establishments_EstablishmentEntityId",
                table: "TypeFoods",
                column: "EstablishmentEntityId",
                principalTable: "Establishments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
