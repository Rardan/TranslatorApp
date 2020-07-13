using Microsoft.EntityFrameworkCore.Migrations;

namespace TranslatorApp.Migrations
{
    public partial class TranslationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TranslationId",
                table: "Queries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Queries_TranslationId",
                table: "Queries",
                column: "TranslationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Queries_Translations_TranslationId",
                table: "Queries",
                column: "TranslationId",
                principalTable: "Translations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Queries_Translations_TranslationId",
                table: "Queries");

            migrationBuilder.DropIndex(
                name: "IX_Queries_TranslationId",
                table: "Queries");

            migrationBuilder.DropColumn(
                name: "TranslationId",
                table: "Queries");
        }
    }
}
