using Microsoft.EntityFrameworkCore.Migrations;

namespace GraphQLDemo.DataRepository.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Bonus = table.Column<decimal>(type: "numeric(6,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Department",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onUpdate: ReferentialAction.Cascade,
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_Employee",
                        column: x => x.ManagerId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Computer" },
                    { 2, "Account" },
                    { 3, "Engineering" },
                    { 4, "Human Resource" }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Bonus", "DepartmentId", "ManagerId", "Name", "Salary" },
                values: new object[] { 1, 400m, 3, null, "John", 25000 });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Bonus", "DepartmentId", "ManagerId", "Name", "Salary" },
                values: new object[] { 2, 800m, 3, 1, "Robert", 15000 });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Bonus", "DepartmentId", "ManagerId", "Name", "Salary" },
                values: new object[] { 3, 175m, 2, 2, "Richard", 10000 });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Bonus", "DepartmentId", "ManagerId", "Name", "Salary" },
                values: new object[] { 5, 0m, 1, 2, "Stefan", 5000 });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Bonus", "DepartmentId", "ManagerId", "Name", "Salary" },
                values: new object[] { 4, 0m, 2, 3, "Mark", 7500 });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ManagerId",
                table: "Employee",
                column: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
