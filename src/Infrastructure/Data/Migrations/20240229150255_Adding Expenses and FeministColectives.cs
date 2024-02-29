using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organizer.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingExpensesandFeministColectives : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeministCollective_AspNetUsers_FeministId",
                table: "FeministCollective");

            migrationBuilder.DropForeignKey(
                name: "FK_FeministCollective_Collectives_CollectiveId",
                table: "FeministCollective");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_CreditorId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeministCollective",
                table: "FeministCollective");

            migrationBuilder.RenameTable(
                name: "FeministCollective",
                newName: "FeministCollectives");

            migrationBuilder.RenameIndex(
                name: "IX_FeministCollective_FeministId",
                table: "FeministCollectives",
                newName: "IX_FeministCollectives_FeministId");

            migrationBuilder.RenameIndex(
                name: "IX_FeministCollective_CollectiveId",
                table: "FeministCollectives",
                newName: "IX_FeministCollectives_CollectiveId");

            migrationBuilder.AlterColumn<string>(
                name: "CreditorId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FeministId",
                table: "FeministCollectives",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeministCollectives",
                table: "FeministCollectives",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    DebtorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreditorId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_AspNetUsers_DebtorId",
                        column: x => x.DebtorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expenses_Collectives_CreditorId",
                        column: x => x.CreditorId,
                        principalTable: "Collectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CreditorId",
                table: "Expenses",
                column: "CreditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_DebtorId",
                table: "Expenses",
                column: "DebtorId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeministCollectives_AspNetUsers_FeministId",
                table: "FeministCollectives",
                column: "FeministId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeministCollectives_Collectives_CollectiveId",
                table: "FeministCollectives",
                column: "CollectiveId",
                principalTable: "Collectives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_CreditorId",
                table: "Transactions",
                column: "CreditorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeministCollectives_AspNetUsers_FeministId",
                table: "FeministCollectives");

            migrationBuilder.DropForeignKey(
                name: "FK_FeministCollectives_Collectives_CollectiveId",
                table: "FeministCollectives");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_CreditorId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeministCollectives",
                table: "FeministCollectives");

            migrationBuilder.RenameTable(
                name: "FeministCollectives",
                newName: "FeministCollective");

            migrationBuilder.RenameIndex(
                name: "IX_FeministCollectives_FeministId",
                table: "FeministCollective",
                newName: "IX_FeministCollective_FeministId");

            migrationBuilder.RenameIndex(
                name: "IX_FeministCollectives_CollectiveId",
                table: "FeministCollective",
                newName: "IX_FeministCollective_CollectiveId");

            migrationBuilder.AlterColumn<string>(
                name: "CreditorId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FeministId",
                table: "FeministCollective",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeministCollective",
                table: "FeministCollective",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeministCollective_AspNetUsers_FeministId",
                table: "FeministCollective",
                column: "FeministId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeministCollective_Collectives_CollectiveId",
                table: "FeministCollective",
                column: "CollectiveId",
                principalTable: "Collectives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_CreditorId",
                table: "Transactions",
                column: "CreditorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
