using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Swolf_Calculation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Swolf",
                table: "WorkoutIntervals",
                nullable: true,
                computedColumnSql: "case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 50 / dbo.fnGetIntervalDistance(Id) + StrokeCount * 2 end",
                oldClrType: typeof(float),
                oldNullable: true,
                oldComputedColumnSql: "case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 50 / dbo.fnGetIntervalDistance(Id) + StrokeCount end");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Swolf",
                table: "WorkoutIntervals",
                nullable: true,
                computedColumnSql: "case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 50 / dbo.fnGetIntervalDistance(Id) + StrokeCount end",
                oldClrType: typeof(float),
                oldNullable: true,
                oldComputedColumnSql: "case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 50 / dbo.fnGetIntervalDistance(Id) + StrokeCount * 2 end");
        }
    }
}
