using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanResourceAPI.Migrations
{
    public partial class add_column_otp_account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredOTP",
                table: "tb_t_account",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsUse",
                table: "tb_t_account",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OTP",
                table: "tb_t_account",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredOTP",
                table: "tb_t_account");

            migrationBuilder.DropColumn(
                name: "IsUse",
                table: "tb_t_account");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "tb_t_account");

        }
    }
}
