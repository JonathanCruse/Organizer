using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organizer.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingMonthlyincome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "MonthlyIncome",
                table: "AspNetUsers",
                type: "real",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Collective",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collective", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeministCollective",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectiveId = table.Column<int>(type: "int", nullable: false),
                    FeministId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrentBalance = table.Column<float>(type: "real", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeministCollective", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeministCollective_AspNetUsers_FeministId",
                        column: x => x.FeministId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeministCollective_Collective_CollectiveId",
                        column: x => x.CollectiveId,
                        principalTable: "Collective",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeministCollective_CollectiveId",
                table: "FeministCollective",
                column: "CollectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_FeministCollective_FeministId",
                table: "FeministCollective",
                column: "FeministId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeministCollective");

            migrationBuilder.DropTable(
                name: "Collective");

            migrationBuilder.DropColumn(
                name: "MonthlyIncome",
                table: "AspNetUsers");
        }
    }
}
