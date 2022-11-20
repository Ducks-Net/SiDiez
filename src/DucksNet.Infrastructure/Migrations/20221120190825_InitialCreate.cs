using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DucksNet.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DucksNet.Infrastructure.Prelude.IDatabaseContext.CageTimeBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CageId = table.Column<Guid>(type: "TEXT", nullable: true),
                    OccupantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DucksNet.Infrastructure.Prelude.IDatabaseContext.CageTimeBlocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DucksNet.Infrastructure.Prelude.IDatabaseContext.MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdAppointment = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdClient = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DucksNet.Infrastructure.Prelude.IDatabaseContext.MedicalRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DucksNet.Infrastructure.Prelude.IDatabaseContext.Cages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SizeId = table.Column<int>(type: "INTEGER", nullable: false),
                    LocationId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DucksNet.Infrastructure.Prelude.IDatabaseContext.Cages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DucksNet.Infrastructure.Prelude.IDatabaseContext.Cages_Size_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Size",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DucksNet.Infrastructure.Prelude.IDatabaseContext.Cages_SizeId",
                table: "DucksNet.Infrastructure.Prelude.IDatabaseContext.Cages",
                column: "SizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DucksNet.Infrastructure.Prelude.IDatabaseContext.Cages");

            migrationBuilder.DropTable(
                name: "DucksNet.Infrastructure.Prelude.IDatabaseContext.CageTimeBlocks");

            migrationBuilder.DropTable(
                name: "DucksNet.Infrastructure.Prelude.IDatabaseContext.MedicalRecords");

            migrationBuilder.DropTable(
                name: "Size");
        }
    }
}
