using Microsoft.EntityFrameworkCore.Migrations;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Migrations
{
    public partial class _202257 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "CSUserApply",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "CSUser",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CSScoreList",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SysUserRoleMapping_SysRoleId",
                table: "SysUserRoleMapping",
                column: "SysRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SysUserRoleMapping_SysUserId",
                table: "SysUserRoleMapping",
                column: "SysUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SysUserMenuMapping_SysMenuId",
                table: "SysUserMenuMapping",
                column: "SysMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SysUserMenuMapping_SysUserId",
                table: "SysUserMenuMapping",
                column: "SysUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SysRoleMenuMapping_SysMenuId",
                table: "SysRoleMenuMapping",
                column: "SysMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SysRoleMenuMapping_SysRoleId",
                table: "SysRoleMenuMapping",
                column: "SysRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_SysRoleMenuMapping_SysMenu_SysMenuId",
                table: "SysRoleMenuMapping",
                column: "SysMenuId",
                principalTable: "SysMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SysRoleMenuMapping_SysRole_SysRoleId",
                table: "SysRoleMenuMapping",
                column: "SysRoleId",
                principalTable: "SysRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SysUserMenuMapping_SysMenu_SysMenuId",
                table: "SysUserMenuMapping",
                column: "SysMenuId",
                principalTable: "SysMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SysUserMenuMapping_SysUser_SysUserId",
                table: "SysUserMenuMapping",
                column: "SysUserId",
                principalTable: "SysUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SysUserRoleMapping_SysRole_SysRoleId",
                table: "SysUserRoleMapping",
                column: "SysRoleId",
                principalTable: "SysRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SysUserRoleMapping_SysUser_SysUserId",
                table: "SysUserRoleMapping",
                column: "SysUserId",
                principalTable: "SysUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SysRoleMenuMapping_SysMenu_SysMenuId",
                table: "SysRoleMenuMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_SysRoleMenuMapping_SysRole_SysRoleId",
                table: "SysRoleMenuMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_SysUserMenuMapping_SysMenu_SysMenuId",
                table: "SysUserMenuMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_SysUserMenuMapping_SysUser_SysUserId",
                table: "SysUserMenuMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_SysUserRoleMapping_SysRole_SysRoleId",
                table: "SysUserRoleMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_SysUserRoleMapping_SysUser_SysUserId",
                table: "SysUserRoleMapping");

            migrationBuilder.DropIndex(
                name: "IX_SysUserRoleMapping_SysRoleId",
                table: "SysUserRoleMapping");

            migrationBuilder.DropIndex(
                name: "IX_SysUserRoleMapping_SysUserId",
                table: "SysUserRoleMapping");

            migrationBuilder.DropIndex(
                name: "IX_SysUserMenuMapping_SysMenuId",
                table: "SysUserMenuMapping");

            migrationBuilder.DropIndex(
                name: "IX_SysUserMenuMapping_SysUserId",
                table: "SysUserMenuMapping");

            migrationBuilder.DropIndex(
                name: "IX_SysRoleMenuMapping_SysMenuId",
                table: "SysRoleMenuMapping");

            migrationBuilder.DropIndex(
                name: "IX_SysRoleMenuMapping_SysRoleId",
                table: "SysRoleMenuMapping");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "CSUserApply");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "CSUser");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CSScoreList");
        }
    }
}
