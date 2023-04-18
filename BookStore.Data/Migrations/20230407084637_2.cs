using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentBorrowBooks_Books_BookId",
                table: "StudentBorrowBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentBorrowBooks_Students_StudentId",
                table: "StudentBorrowBooks");

            migrationBuilder.DropIndex(
                name: "IX_StudentBorrowBooks_BookId",
                table: "StudentBorrowBooks");

            migrationBuilder.DropIndex(
                name: "IX_StudentBorrowBooks_StudentId",
                table: "StudentBorrowBooks");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "StudentBorrowBooks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "StudentBorrowBooks");

            migrationBuilder.CreateIndex(
                name: "IX_StudentBorrowBooks_BookId",
                table: "StudentBorrowBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentBorrowBooks_StudentId",
                table: "StudentBorrowBooks",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentBorrowBooks_Books_BookId",
                table: "StudentBorrowBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentBorrowBooks_Students_StudentId",
                table: "StudentBorrowBooks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
