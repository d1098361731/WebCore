using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreEf.Migrations
{
    public partial class coreWeb20191202 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
