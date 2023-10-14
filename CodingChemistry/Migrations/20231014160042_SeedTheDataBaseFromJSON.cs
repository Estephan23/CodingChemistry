using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingChemistry.Migrations
{
    /// <inheritdoc />
    public partial class SeedTheDataBaseFromJSON : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "elements",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AbbreviatedElectronConfiguration",
                table: "elements",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "AtomicMass",
                table: "elements",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "ElectronConfiguration",
                table: "elements",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Electronegativity",
                table: "elements",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Group",
                table: "elements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "elements",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "elements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "elements",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbbreviatedElectronConfiguration",
                table: "elements");

            migrationBuilder.DropColumn(
                name: "AtomicMass",
                table: "elements");

            migrationBuilder.DropColumn(
                name: "ElectronConfiguration",
                table: "elements");

            migrationBuilder.DropColumn(
                name: "Electronegativity",
                table: "elements");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "elements");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "elements");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "elements");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "elements");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "elements",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
