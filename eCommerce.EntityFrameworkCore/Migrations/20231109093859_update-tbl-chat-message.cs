using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.EntityFrameworkCore.Migrations
{
    public partial class updatetblchatmessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MessageChatPersonals_SenderId",
                table: "MessageChatPersonals",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageChatPersonals_AspNetUsers_SenderId",
                table: "MessageChatPersonals",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageChatPersonals_AspNetUsers_SenderId",
                table: "MessageChatPersonals");

            migrationBuilder.DropIndex(
                name: "IX_MessageChatPersonals_SenderId",
                table: "MessageChatPersonals");
        }
    }
}
