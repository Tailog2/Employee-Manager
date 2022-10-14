using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManager.Infrastructure.Data.Migrations
{
    public partial class SeedDepartmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = (@"
                SET IDENTITY_INSERT Departments ON
                Insert INTO Departments (Id, Name) Values (1, 'Headquarter')
                Insert INTO Departments (Id, Name) Values (2, 'London Branch')
                SET IDENTITY_INSERT Departments OFF
            ");

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = (@"
                Delete Departments Where Id = 1
                Delete Departments Where Id = 2
            ");

            migrationBuilder.Sql(sql);
        }
    }
}
