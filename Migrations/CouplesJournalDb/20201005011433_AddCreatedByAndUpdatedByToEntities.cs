using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CouplesJournal.Migrations.CouplesJournalDb
{
    public partial class AddCreatedByAndUpdatedByToEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "JournalReplies",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "JournalReplies",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "JournalEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "JournalEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "JournalReplies");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "JournalReplies");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "JournalEntries");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "JournalEntries");
        }
    }
}
