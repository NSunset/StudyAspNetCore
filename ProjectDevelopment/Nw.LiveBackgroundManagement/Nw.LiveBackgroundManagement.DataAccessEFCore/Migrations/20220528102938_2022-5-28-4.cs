using Microsoft.EntityFrameworkCore.Migrations;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Migrations
{
    public partial class _20225284 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentReplyId",
                table: "CSCommentReply");

            migrationBuilder.AddColumn<string>(
                name: "ParentReplyGuid",
                table: "CSCommentReply",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentReplyGuid",
                table: "CSCommentReply");

            migrationBuilder.AddColumn<int>(
                name: "ParentReplyId",
                table: "CSCommentReply",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
