using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fluentify.Web.Migrations
{
    /// <inheritdoc />
    public partial class Add_migr_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskScore",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskScore",
                table: "Tasks");
        }
    }
}
