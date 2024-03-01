using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organizer.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingDomainEventtoTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Transactions");
        }
    }
}
