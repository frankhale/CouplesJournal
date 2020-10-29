using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CouplesJournal.Blazor.Migrations.CouplesJournalDb
{
    public partial class InitialDbModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "JournalEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    fk_journalstatus = table.Column<int>(type: "INTEGER", nullable: true),
                    JournalStatusId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MarkedForDeletion = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalEntries_JournalStatuses_fk_journalstatus",
                        column: x => x.fk_journalstatus,
                        principalTable: "JournalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalEntries_JournalStatuses_JournalStatusId",
                        column: x => x.JournalStatusId,
                        principalTable: "JournalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JournalReplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    JournalEntryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    fk_journalentry = table.Column<Guid>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MarkedForDeletion = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalReplies_JournalEntries_fk_journalentry",
                        column: x => x.fk_journalentry,
                        principalTable: "JournalEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalReplies_JournalEntries_JournalEntryId",
                        column: x => x.JournalEntryId,
                        principalTable: "JournalEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_JournalEntries_fk_journalstatus",
                table: "JournalEntries",
                column: "fk_journalstatus");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_JournalStatusId",
                table: "JournalEntries",
                column: "JournalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalReplies_fk_journalentry",
                table: "JournalReplies",
                column: "fk_journalentry");

            migrationBuilder.CreateIndex(
                name: "IX_JournalReplies_JournalEntryId",
                table: "JournalReplies",
                column: "JournalEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalReplies");

            migrationBuilder.DropTable(
                name: "JournalEntries");

            migrationBuilder.DropTable(
                name: "JournalStatuses");
        }
    }
}
