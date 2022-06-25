using Microsoft.EntityFrameworkCore.Migrations;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Migrations
{
    public partial class _2022527last : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripton",
                table: "CSRoom",
                newName: "RoomImgUrl");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "CSUser",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CSWorksId",
                table: "CSScoreList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CSRoom",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "ToUserId",
                table: "CSComment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "FromUserId",
                table: "CSComment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CSCommentType",
                table: "CSComment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CSWorksId",
                table: "CSComment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FromUserName",
                table: "CSComment",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ToUserName",
                table: "CSComment",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "CSUser");

            migrationBuilder.DropColumn(
                name: "CSWorksId",
                table: "CSScoreList");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CSRoom");

            migrationBuilder.DropColumn(
                name: "CSCommentType",
                table: "CSComment");

            migrationBuilder.DropColumn(
                name: "CSWorksId",
                table: "CSComment");

            migrationBuilder.DropColumn(
                name: "FromUserName",
                table: "CSComment");

            migrationBuilder.DropColumn(
                name: "ToUserName",
                table: "CSComment");

            migrationBuilder.RenameColumn(
                name: "RoomImgUrl",
                table: "CSRoom",
                newName: "Descripton");

            migrationBuilder.AlterColumn<string>(
                name: "ToUserId",
                table: "CSComment",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FromUserId",
                table: "CSComment",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
