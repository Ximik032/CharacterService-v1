using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CharacterService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Characters",
                table: "Characters");

            migrationBuilder.RenameTable(
                name: "Characters",
                newName: "characters");

            migrationBuilder.RenameColumn(
                name: "Traits",
                table: "characters",
                newName: "traits");

            migrationBuilder.RenameColumn(
                name: "Skills",
                table: "characters",
                newName: "skills");

            migrationBuilder.RenameColumn(
                name: "Quirks",
                table: "characters",
                newName: "quirks");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "characters",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "characters",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Background",
                table: "characters",
                newName: "background");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "characters",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "characters",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "characters",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "SystemPrompt",
                table: "characters",
                newName: "system_prompt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "characters",
                newName: "created_at");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "characters",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "characters",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "system_prompt",
                table: "characters",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_characters",
                table: "characters",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_characters_user_id",
                table: "characters",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_characters",
                table: "characters");

            migrationBuilder.DropIndex(
                name: "IX_characters_user_id",
                table: "characters");

            migrationBuilder.RenameTable(
                name: "characters",
                newName: "Characters");

            migrationBuilder.RenameColumn(
                name: "traits",
                table: "Characters",
                newName: "Traits");

            migrationBuilder.RenameColumn(
                name: "skills",
                table: "Characters",
                newName: "Skills");

            migrationBuilder.RenameColumn(
                name: "quirks",
                table: "Characters",
                newName: "Quirks");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Characters",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Characters",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "background",
                table: "Characters",
                newName: "Background");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Characters",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Characters",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Characters",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "system_prompt",
                table: "Characters",
                newName: "SystemPrompt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Characters",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Characters",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Characters",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SystemPrompt",
                table: "Characters",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Characters",
                table: "Characters",
                column: "Id");
        }
    }
}
