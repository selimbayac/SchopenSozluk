using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchopenSozlukDataAccessLayer.Migrations
{
    public partial class mig_like : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.AddColumn<int>(
                name: "DislikesCount",
                table: "Entryler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LikesCount",
                table: "Entryler",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

     
        }
    }
}
