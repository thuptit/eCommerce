using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace eCommerce.EntityFrameworkCore.Migrations
{
    public partial class addchat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupChats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<byte>(type: "smallint", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiorId = table.Column<long>(type: "bigint", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletorId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupChatUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    GroupChatId = table.Column<long>(type: "bigint", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiorId = table.Column<long>(type: "bigint", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletorId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupChatUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupChatUsers_GroupChats_GroupChatId",
                        column: x => x.GroupChatId,
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageChats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "text", nullable: false),
                    GroupChatId = table.Column<long>(type: "bigint", nullable: false),
                    SenderId = table.Column<long>(type: "bigint", nullable: false),
                    IsSeen = table.Column<bool>(type: "boolean", nullable: false),
                    SeenTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiorId = table.Column<long>(type: "bigint", nullable: true),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletorId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageChats_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageChats_GroupChats_GroupChatId",
                        column: x => x.GroupChatId,
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupChats_IsDeleted",
                table: "GroupChats",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatUsers_GroupChatId",
                table: "GroupChatUsers",
                column: "GroupChatId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatUsers_IsDeleted",
                table: "GroupChatUsers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatUsers_UserId",
                table: "GroupChatUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageChats_GroupChatId",
                table: "MessageChats",
                column: "GroupChatId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageChats_IsDeleted",
                table: "MessageChats",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_MessageChats_SenderId",
                table: "MessageChats",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupChatUsers");

            migrationBuilder.DropTable(
                name: "MessageChats");

            migrationBuilder.DropTable(
                name: "GroupChats");
        }
    }
}
