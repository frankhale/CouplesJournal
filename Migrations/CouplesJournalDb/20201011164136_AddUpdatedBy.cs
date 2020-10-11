using Microsoft.EntityFrameworkCore.Migrations;

namespace CouplesJournal.Migrations.CouplesJournalDb
{
    public partial class AddUpdatedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "JournalReplies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "JournalEntries",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "JournalReplies");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "JournalEntries");
        }
    }
}
