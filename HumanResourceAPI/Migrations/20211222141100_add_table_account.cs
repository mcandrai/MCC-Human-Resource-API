using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanResourceAPI.Migrations
{
    public partial class add_table_account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_t_account",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", maxLength: 16, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_account", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_t_account_tb_m_employees_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_employees",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_t_account");
        }
    }
}
