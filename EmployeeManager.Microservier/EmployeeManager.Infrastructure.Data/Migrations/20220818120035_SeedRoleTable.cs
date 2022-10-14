using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManager.Infrastructure.Data.Migrations
{
    public partial class SeedRoleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = (@"
                SET IDENTITY_INSERT Roles ON
                Insert INTO Roles (Id, HourlyRate, Name) Values (1, 100, 'Manager')
                Insert INTO Roles (Id, HourlyRate, Name) Values (2, 50, 'Worker')
                Insert INTO Roles (Id, HourlyRate, Name) Values (3, 200, 'CEO')
                SET IDENTITY_INSERT Roles OFF
            ");

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = (@"
                Delete Role Where Id = 1
                Delete Role Where Id = 2
                Delete Role Where Id = 3
            ");

            migrationBuilder.Sql(sql);
        }
    }
}
