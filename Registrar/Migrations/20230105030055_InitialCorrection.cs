using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Registrar.Migrations
{
    public partial class InitialCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentDepartmnets_Departments_DepartmentId",
                table: "StudentDepartmnets");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentDepartmnets_Students_StudentId",
                table: "StudentDepartmnets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentDepartmnets",
                table: "StudentDepartmnets");

            migrationBuilder.RenameTable(
                name: "StudentDepartmnets",
                newName: "StudentDepartments");

            migrationBuilder.RenameIndex(
                name: "IX_StudentDepartmnets_StudentId",
                table: "StudentDepartments",
                newName: "IX_StudentDepartments_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentDepartmnets_DepartmentId",
                table: "StudentDepartments",
                newName: "IX_StudentDepartments_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentDepartments",
                table: "StudentDepartments",
                column: "StudentDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentDepartments_Departments_DepartmentId",
                table: "StudentDepartments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentDepartments_Students_StudentId",
                table: "StudentDepartments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentDepartments_Departments_DepartmentId",
                table: "StudentDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentDepartments_Students_StudentId",
                table: "StudentDepartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentDepartments",
                table: "StudentDepartments");

            migrationBuilder.RenameTable(
                name: "StudentDepartments",
                newName: "StudentDepartmnets");

            migrationBuilder.RenameIndex(
                name: "IX_StudentDepartments_StudentId",
                table: "StudentDepartmnets",
                newName: "IX_StudentDepartmnets_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentDepartments_DepartmentId",
                table: "StudentDepartmnets",
                newName: "IX_StudentDepartmnets_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentDepartmnets",
                table: "StudentDepartmnets",
                column: "StudentDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentDepartmnets_Departments_DepartmentId",
                table: "StudentDepartmnets",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentDepartmnets_Students_StudentId",
                table: "StudentDepartmnets",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
