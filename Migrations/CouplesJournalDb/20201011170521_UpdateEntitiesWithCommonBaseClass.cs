using Microsoft.EntityFrameworkCore.Migrations;

namespace CouplesJournal.Migrations.CouplesJournalDb
{
    public partial class UpdateEntitiesWithCommonBaseClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MarkedForDeletion",
                table: "JournalReplies",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MarkedForDeletion",
                table: "JournalEntries",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarkedForDeletion",
                table: "JournalReplies");

            migrationBuilder.DropColumn(
                name: "MarkedForDeletion",
                table: "JournalEntries");
        }
    }
}
