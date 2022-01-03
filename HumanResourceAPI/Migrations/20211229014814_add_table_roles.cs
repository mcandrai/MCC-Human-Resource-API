using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanResourceAPI.Migrations
{
    public partial class add_table_roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
   

            migrationBuilder.CreateTable(
                name: "tb_m_roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_roles", x => x.RoleId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_roles");

        }
    }
}
