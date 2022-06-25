using Microsoft.EntityFrameworkCore.Migrations;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Migrations
{
    public partial class _2022529 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentName",
                table: "CSCommentReply");

            migrationBuilder.RenameColumn(
                name: "ReplyGuid",
                table: "CSCommentReply",
                newName: "ToUserName");

            migrationBuilder.RenameColumn(
                name: "RepliesName",
                table: "CSCommentReply",
                newName: "ImgUrl");

            migrationBuilder.RenameColumn(
                name: "ParentReplyGuid",
                table: "CSCommentReply",
                newName: "FromUserName");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "CSCommentReply",
                newName: "ToUserId");

            migrationBuilder.AddColumn<int>(
                name: "FromUserId",
                table: "CSCommentReply",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "CSComment",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromUserId",
                table: "CSCommentReply");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "CSComment");

            migrationBuilder.RenameColumn(
                name: "ToUserName",
                table: "CSCommentReply",
                newName: "ReplyGuid");

            migrationBuilder.RenameColumn(
                name: "ToUserId",
                table: "CSCommentReply",
                newName: "Level");

            migrationBuilder.RenameColumn(
                name: "ImgUrl",
                table: "CSCommentReply",
                newName: "RepliesName");

            migrationBuilder.RenameColumn(
                name: "FromUserName",
                table: "CSCommentReply",
                newName: "ParentReplyGuid");

            migrationBuilder.AddColumn<string>(
                name: "CommentName",
                table: "CSCommentReply",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
