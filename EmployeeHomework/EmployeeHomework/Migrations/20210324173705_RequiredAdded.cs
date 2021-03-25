using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeHomework.Migrations
{
    public partial class RequiredAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTable_EmployeeTable_BossId",
                table: "EmployeeTable");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTable_BossId",
                table: "EmployeeTable");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "EmployeeTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "EmployeeTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HomeAddress",
                table: "EmployeeTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "EmployeeTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "EmployeeTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "EmployeeTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "HomeAddress",
                table: "EmployeeTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "EmployeeTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTable_BossId",
                table: "EmployeeTable",
                column: "BossId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTable_EmployeeTable_BossId",
                table: "EmployeeTable",
                column: "BossId",
                principalTable: "EmployeeTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
