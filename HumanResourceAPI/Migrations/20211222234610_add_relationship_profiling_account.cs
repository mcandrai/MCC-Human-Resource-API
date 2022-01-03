using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanResourceAPI.Migrations
{
    public partial class add_relationship_profiling_account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NIK",
                table: "tb_t_profiling",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_profiling_tb_t_account_NIK",
                table: "tb_t_profiling",
                column: "NIK",
                principalTable: "tb_t_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_profiling_tb_t_account_NIK",
                table: "tb_t_profiling");

            migrationBuilder.AlterColumn<string>(
                name: "NIK",
                table: "tb_t_profiling",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)");
        }
    }
}
