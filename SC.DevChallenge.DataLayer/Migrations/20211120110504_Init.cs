using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SC.DevChallenge.DataLayer.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinanceInstrument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Portfolio = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Owner = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Instrument = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeSlot = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceInstrument", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinanceInstrument");
        }
    }
}
