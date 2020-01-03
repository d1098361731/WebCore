using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreEf.Migrations
{
    public partial class WebCore_1224 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: new Guid("61eb672f-22b3-44ce-9fbc-ff4445feb1a4"));

            migrationBuilder.DeleteData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: new Guid("81f34cd6-9da3-4a34-b800-e5cade90df8f"));

            migrationBuilder.DeleteData(
                table: "Sys_User",
                keyColumn: "Id",
                keyValue: new Guid("7e7bfb7d-0527-4176-9321-1586b4fb4e43"));

            migrationBuilder.CreateTable(
                name: "Sys_RoleMenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(maxLength: 40, nullable: false),
                    MenuId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_RoleMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sys_RoleMenu_Sys_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Sys_Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sys_RoleMenu_Sys_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Sys_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Sys_Menu",
                columns: new[] { "Id", "Icon", "Index", "Label", "Name", "ParentMentId", "Path", "Remarks", "Tooptip" },
                values: new object[] { new Guid("95f9e740-a05e-4e88-88da-c566ab42ae3f"), "el-icon-s-custom", 0, "主界面", "Main", null, "/", "", "主界面提示" });

            migrationBuilder.InsertData(
                table: "Sys_Menu",
                columns: new[] { "Id", "Icon", "Index", "Label", "Name", "ParentMentId", "Path", "Remarks", "Tooptip" },
                values: new object[] { new Guid("ad718b11-5353-44c1-844f-0bbf05742cbf"), "el-icon-s-custom", 1, "成员管理", "userManager", null, "/userManager", "", "成员管理" });

            migrationBuilder.InsertData(
                table: "Sys_User",
                columns: new[] { "Id", "Address", "Company", "Name", "Password", "Remarks", "State" },
                values: new object[] { new Guid("37a4fb87-6e0e-4fb0-9df5-a9b1948ac660"), "管理员测试地址", "西安新时间", "admin", "admin", "", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Sys_RoleMenu_MenuId",
                table: "Sys_RoleMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Sys_RoleMenu_RoleId",
                table: "Sys_RoleMenu",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_RoleMenu");

            migrationBuilder.DeleteData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: new Guid("95f9e740-a05e-4e88-88da-c566ab42ae3f"));

            migrationBuilder.DeleteData(
                table: "Sys_Menu",
                keyColumn: "Id",
                keyValue: new Guid("ad718b11-5353-44c1-844f-0bbf05742cbf"));

            migrationBuilder.DeleteData(
                table: "Sys_User",
                keyColumn: "Id",
                keyValue: new Guid("37a4fb87-6e0e-4fb0-9df5-a9b1948ac660"));

            migrationBuilder.InsertData(
                table: "Sys_Menu",
                columns: new[] { "Id", "Icon", "Index", "Label", "Name", "ParentMentId", "Path", "Remarks", "Tooptip" },
                values: new object[] { new Guid("81f34cd6-9da3-4a34-b800-e5cade90df8f"), "el-icon-s-custom", 0, "主界面", "Main", null, "/", "", "主界面提示" });

            migrationBuilder.InsertData(
                table: "Sys_Menu",
                columns: new[] { "Id", "Icon", "Index", "Label", "Name", "ParentMentId", "Path", "Remarks", "Tooptip" },
                values: new object[] { new Guid("61eb672f-22b3-44ce-9fbc-ff4445feb1a4"), "el-icon-s-custom", 1, "成员管理", "userManager", null, "/userManager", "", "成员管理" });

            migrationBuilder.InsertData(
                table: "Sys_User",
                columns: new[] { "Id", "Address", "Company", "Name", "Password", "Remarks", "State" },
                values: new object[] { new Guid("7e7bfb7d-0527-4176-9321-1586b4fb4e43"), "管理员测试地址", "西安新时间", "admin", "admin", "", 1 });
        }
    }
}
