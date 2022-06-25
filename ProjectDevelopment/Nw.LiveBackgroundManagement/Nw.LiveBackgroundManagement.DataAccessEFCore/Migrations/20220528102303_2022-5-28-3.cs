using Microsoft.EntityFrameworkCore.Migrations;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Migrations
{
    public partial class _20225283 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplyId",
                table: "CSCommentReply");

            migrationBuilder.AddColumn<string>(
                name: "ReplyGuid",
                table: "CSCommentReply",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplyGuid",
                table: "CSCommentReply");

            migrationBuilder.AddColumn<int>(
                name: "ReplyId",
                table: "CSCommentReply",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
