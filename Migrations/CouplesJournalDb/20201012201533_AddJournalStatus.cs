using Microsoft.EntityFrameworkCore.Migrations;

namespace CouplesJournal.Migrations.CouplesJournalDb
{
    public partial class AddJournalStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "JournalReplies",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "JournalEntries",
                type: "TEXT",
                maxLength: 512,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "JournalEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "JournalEntries",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JournalStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalStatuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "JournalStatuses",
                columns: new[] { "Id", "Value" },
                values: new object[] { 1, "Draft" });

            migrationBuilder.InsertData(
                table: "JournalStatuses",
                columns: new[] { "Id", "Value" },
                values: new object[] { 2, "Final" });

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_StatusId",
                table: "JournalEntries",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntries_JournalStatuses_StatusId",
                table: "JournalEntries",
                column: "StatusId",
                principalTable: "JournalStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntries_JournalStatuses_StatusId",
                table: "JournalEntries");

            migrationBuilder.DropTable(
                name: "JournalStatuses");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntries_StatusId",
                table: "JournalEntries");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "JournalEntries");

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "JournalReplies",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "JournalEntries",
                type: "TEXT",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 512);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "JournalEntries",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
