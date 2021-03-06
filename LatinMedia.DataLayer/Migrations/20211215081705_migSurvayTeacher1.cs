using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatinMedia.DataLayer.Migrations
{
    public partial class migSurvayTeacher1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reportForSurvayTeachers",
                columns: table => new
                {
                    AcademyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcademyFullName = table.Column<string>(nullable: true),
                    Poll1_1 = table.Column<int>(nullable: false),
                    Poll1_2 = table.Column<int>(nullable: false),
                    Poll1_3 = table.Column<int>(nullable: false),
                    Poll1_4 = table.Column<int>(nullable: false),
                    Poll2_1 = table.Column<int>(nullable: false),
                    Poll2_2 = table.Column<int>(nullable: false),
                    Poll2_3 = table.Column<int>(nullable: false),
                    Poll2_4 = table.Column<int>(nullable: false),
                    Poll3_1 = table.Column<int>(nullable: false),
                    Poll3_2 = table.Column<int>(nullable: false),
                    Poll3_3 = table.Column<int>(nullable: false),
                    Poll3_4 = table.Column<int>(nullable: false),
                    Poll4_1 = table.Column<int>(nullable: false),
                    Poll4_2 = table.Column<int>(nullable: false),
                    Poll4_3 = table.Column<int>(nullable: false),
                    Poll4_4 = table.Column<int>(nullable: false),
                    Poll5_1 = table.Column<int>(nullable: false),
                    Poll5_2 = table.Column<int>(nullable: false),
                    Poll5_3 = table.Column<int>(nullable: false),
                    Poll5_4 = table.Column<int>(nullable: false),
                    Poll6_1 = table.Column<int>(nullable: false),
                    Poll6_2 = table.Column<int>(nullable: false),
                    Poll6_3 = table.Column<int>(nullable: false),
                    Poll6_4 = table.Column<int>(nullable: false),
                    UserCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportForSurvayTeachers", x => x.AcademyId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reportForSurvayTeachers");
        }
    }
}
