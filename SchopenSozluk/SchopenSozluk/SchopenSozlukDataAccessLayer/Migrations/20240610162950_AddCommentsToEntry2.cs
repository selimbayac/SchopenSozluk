using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchopenSozlukDataAccessLayer.Migrations
{
    public partial class AddCommentsToEntry2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                 name: "FK_Comment_AspNetUsers_UserId",
                 table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Entryler_EntryId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_EntryId",
                table: "Comments",
                newName: "IX_Comments_EntryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Entryler_EntryId",
                table: "Comments",
                column: "EntryId",
                principalTable: "Entryler",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                  name: "FK_Comments_AspNetUsers_UserId",
                  table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Entryler_EntryId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comment",
                newName: "IX_Comment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_EntryId",
                table: "Comment",
                newName: "IX_Comment_EntryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Entryler_EntryId",
                table: "Comment",
                column: "EntryId",
                principalTable: "Entryler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}