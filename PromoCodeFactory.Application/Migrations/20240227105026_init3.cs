using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromoCodeFactory.Application.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoCode_Partners_PartnerId",
                table: "PromoCode");

            migrationBuilder.DropColumn(
                name: "PromoCodeId",
                table: "Partners");

            migrationBuilder.AlterColumn<Guid>(
                name: "PartnerId",
                table: "PromoCode",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCode_Partners_PartnerId",
                table: "PromoCode",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoCode_Partners_PartnerId",
                table: "PromoCode");

            migrationBuilder.AlterColumn<Guid>(
                name: "PartnerId",
                table: "PromoCode",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "PromoCodeId",
                table: "Partners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_PromoCode_Partners_PartnerId",
                table: "PromoCode",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id");
        }
    }
}
