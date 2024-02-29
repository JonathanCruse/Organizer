using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organizer.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingCollectives : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeministCollective_Collective_CollectiveId",
                table: "FeministCollective");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collective",
                table: "Collective");

            migrationBuilder.RenameTable(
                name: "Collective",
                newName: "Collectives");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collectives",
                table: "Collectives",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DebtorId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_AspNetUsers_CreditorId",
                        column: x => x.CreditorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Collectives_DebtorId",
                        column: x => x.DebtorId,
                        principalTable: "Collectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CreditorId",
                table: "Transactions",
                column: "CreditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DebtorId",
                table: "Transactions",
                column: "DebtorId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeministCollective_Collectives_CollectiveId",
                table: "FeministCollective",
                column: "CollectiveId",
                principalTable: "Collectives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeministCollective_Collectives_CollectiveId",
                table: "FeministCollective");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collectives",
                table: "Collectives");

            migrationBuilder.RenameTable(
                name: "Collectives",
                newName: "Collective");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collective",
                table: "Collective",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeministCollective_Collective_CollectiveId",
                table: "FeministCollective",
                column: "CollectiveId",
                principalTable: "Collective",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
