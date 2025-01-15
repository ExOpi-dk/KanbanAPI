using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kanban.Migrations
{
    /// <inheritdoc />
    public partial class Supe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryUser_Stories_AssignedStoriesStoryId",
                table: "StoryUser");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryUser_Users_AssigneesUserId",
                table: "StoryUser");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AssigneesUserId",
                table: "StoryUser",
                newName: "StoryId");

            migrationBuilder.RenameColumn(
                name: "AssignedStoriesStoryId",
                table: "StoryUser",
                newName: "AssigneesId");

            migrationBuilder.RenameIndex(
                name: "IX_StoryUser_AssigneesUserId",
                table: "StoryUser",
                newName: "IX_StoryUser_StoryId");

            migrationBuilder.RenameColumn(
                name: "StoryId",
                table: "Stories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Statuses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BoardId",
                table: "Boards",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "AssigneeIds",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryUser_Stories_StoryId",
                table: "StoryUser",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryUser_Users_AssigneesId",
                table: "StoryUser",
                column: "AssigneesId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryUser_Stories_StoryId",
                table: "StoryUser");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryUser_Users_AssigneesId",
                table: "StoryUser");

            migrationBuilder.DropColumn(
                name: "AssigneeIds",
                table: "Stories");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "StoryId",
                table: "StoryUser",
                newName: "AssigneesUserId");

            migrationBuilder.RenameColumn(
                name: "AssigneesId",
                table: "StoryUser",
                newName: "AssignedStoriesStoryId");

            migrationBuilder.RenameIndex(
                name: "IX_StoryUser_StoryId",
                table: "StoryUser",
                newName: "IX_StoryUser_AssigneesUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Stories",
                newName: "StoryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Statuses",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Boards",
                newName: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryUser_Stories_AssignedStoriesStoryId",
                table: "StoryUser",
                column: "AssignedStoriesStoryId",
                principalTable: "Stories",
                principalColumn: "StoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryUser_Users_AssigneesUserId",
                table: "StoryUser",
                column: "AssigneesUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
