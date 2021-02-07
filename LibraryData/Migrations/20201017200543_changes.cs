using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryData.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryAssets_LibraryBranch_LibraryBranchId",
                table: "LibraryAssets");

            migrationBuilder.DropIndex(
                name: "IX_LibraryAssets_LibraryBranchId",
                table: "LibraryAssets");

            migrationBuilder.DropColumn(
                name: "LibraryBranchId",
                table: "LibraryAssets");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "LibraryAssets",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "LibraryAssets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LibraryAssets_LocationId",
                table: "LibraryAssets",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryAssets_LibraryBranch_LocationId",
                table: "LibraryAssets",
                column: "LocationId",
                principalTable: "LibraryBranch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LibraryAssets_LibraryBranch_LocationId",
                table: "LibraryAssets");

            migrationBuilder.DropIndex(
                name: "IX_LibraryAssets_LocationId",
                table: "LibraryAssets");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "LibraryAssets");

            migrationBuilder.AlterColumn<int>(
                name: "Title",
                table: "LibraryAssets",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "LibraryBranchId",
                table: "LibraryAssets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LibraryAssets_LibraryBranchId",
                table: "LibraryAssets",
                column: "LibraryBranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryAssets_LibraryBranch_LibraryBranchId",
                table: "LibraryAssets",
                column: "LibraryBranchId",
                principalTable: "LibraryBranch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
