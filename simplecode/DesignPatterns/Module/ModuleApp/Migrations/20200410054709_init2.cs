using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuleApp.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Core_EntityType",
                columns: new[] { "Id", "AreaName", "IsMenuable", "RoutingAction", "RoutingController" },
                values: new object[] { "Vendor", "Core", false, "VendorDetail", "Vendor" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Core_EntityType",
                keyColumn: "Id",
                keyValue: "Vendor");
        }
    }
}
