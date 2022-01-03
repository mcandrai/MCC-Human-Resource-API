using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanResourceAPI.Migrations
{
    public partial class rename_table_accountrole_relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_tb_m_roles_RoleId",
                table: "AccountRole");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRole_tb_t_account_NIK",
                table: "AccountRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRole",
                table: "AccountRole");

            migrationBuilder.RenameTable(
                name: "AccountRole",
                newName: "tb_t_account_role");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRole_RoleId",
                table: "tb_t_account_role",
                newName: "IX_tb_t_account_role_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_t_account_role",
                table: "tb_t_account_role",
                columns: new[] { "NIK", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_account_role_tb_m_roles_RoleId",
                table: "tb_t_account_role",
                column: "RoleId",
                principalTable: "tb_m_roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_account_role_tb_t_account_NIK",
                table: "tb_t_account_role",
                column: "NIK",
                principalTable: "tb_t_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_account_role_tb_m_roles_RoleId",
                table: "tb_t_account_role");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_account_role_tb_t_account_NIK",
                table: "tb_t_account_role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_t_account_role",
                table: "tb_t_account_role");

            migrationBuilder.RenameTable(
                name: "tb_t_account_role",
                newName: "AccountRole");

            migrationBuilder.RenameIndex(
                name: "IX_tb_t_account_role_RoleId",
                table: "AccountRole",
                newName: "IX_AccountRole_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRole",
                table: "AccountRole",
                columns: new[] { "NIK", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_tb_m_roles_RoleId",
                table: "AccountRole",
                column: "RoleId",
                principalTable: "tb_m_roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_tb_t_account_NIK",
                table: "AccountRole",
                column: "NIK",
                principalTable: "tb_t_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
