using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagerRestaurant.API.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhieuOder_Ban_BanId",
                table: "PhieuOder");

            migrationBuilder.DropIndex(
                name: "IX_PhieuOder_BanId",
                table: "PhieuOder");

            migrationBuilder.DropColumn(
                name: "BanId",
                table: "PhieuOder");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuOder_IdBan",
                table: "PhieuOder",
                column: "IdBan");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuOder_Ban_IdBan",
                table: "PhieuOder",
                column: "IdBan",
                principalTable: "Ban",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhieuOder_Ban_IdBan",
                table: "PhieuOder");

            migrationBuilder.DropIndex(
                name: "IX_PhieuOder_IdBan",
                table: "PhieuOder");

            migrationBuilder.AddColumn<Guid>(
                name: "BanId",
                table: "PhieuOder",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhieuOder_BanId",
                table: "PhieuOder",
                column: "BanId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuOder_Ban_BanId",
                table: "PhieuOder",
                column: "BanId",
                principalTable: "Ban",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
