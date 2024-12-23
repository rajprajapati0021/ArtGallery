//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace ArtGallery.Migrations
//{
//    /// <inheritdoc />
//    public partial class secondMigration : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropForeignKey(
//                name: "FK_Likes_Products_ProductId",
//                table: "Likes");

//            migrationBuilder.AlterColumn<long>(
//                name: "ProductId",
//                table: "Likes",
//                type: "bigint",
//                nullable: false,
//                defaultValue: 0L,
//                oldClrType: typeof(long),
//                oldType: "bigint",
//                oldNullable: true);

//            migrationBuilder.AddForeignKey(
//                name: "FK_Likes_Products_ProductId",
//                table: "Likes",
//                column: "ProductId",
//                principalTable: "Products",
//                principalColumn: "Id",
//                onDelete: ReferentialAction.Cascade);
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropForeignKey(
//                name: "FK_Likes_Products_ProductId",
//                table: "Likes");

//            migrationBuilder.AlterColumn<long>(
//                name: "ProductId",
//                table: "Likes",
//                type: "bigint",
//                nullable: true,
//                oldClrType: typeof(long),
//                oldType: "bigint");

//            migrationBuilder.AddForeignKey(
//                name: "FK_Likes_Products_ProductId",
//                table: "Likes",
//                column: "ProductId",
//                principalTable: "Products",
//                principalColumn: "Id");
//        }
//    }
//}
