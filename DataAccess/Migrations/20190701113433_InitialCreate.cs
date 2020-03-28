using System;
using Constants;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

            migrationBuilder.CreateTable(
                name: "WorkoutIntervalType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutIntervalType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Distance = table.Column<float>(nullable: false),
                    Duration = table.Column<float>(nullable: false),
                    Pace = table.Column<float>(nullable: false),
                    Place = table.Column<string>(maxLength: 100, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Start = table.Column<DateTime>(type: "datetime", nullable: false),
                    Note = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutIntervals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkoutId = table.Column<int>(nullable: false),
                    TimeOffset = table.Column<float>(nullable: false),
                    WorkoutIntervalTypeId = table.Column<int>(nullable: true),
                    Notes = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutIntervals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutIntervals_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutIntervals_WorkoutIntervalType_WorkoutIntervalTypeId",
                        column: x => x.WorkoutIntervalTypeId,
                        principalTable: "WorkoutIntervalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutIntervalLengths",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkoutIntervalId = table.Column<int>(nullable: false),
                    Duration = table.Column<float>(nullable: false),
                    StrokeTypeId = table.Column<int>(nullable: false),
                    StrokeCount = table.Column<int>(nullable: false),
                    Distance = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutIntervalLengths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutIntervalLengths_WorkoutIntervals_WorkoutIntervalId",
                        column: x => x.WorkoutIntervalId,
                        principalTable: "WorkoutIntervals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "WorkoutIntervalType",
                columns: new[] { "Id", "Name" },
                values: new[]
                {
                    new { Id = WorkoutIntervalTypes.WarmUpId,  Name = "Warm up" },
                    new { Id = WorkoutIntervalTypes.FirstQuickFreestyleId,  Name = "First quick freestyle" },
                    new { Id = WorkoutIntervalTypes.SecondQuickFreestyleId,  Name = "Second quick freestyle" },
                    new { Id = WorkoutIntervalTypes.DrillWithFinsId,  Name = "Drill with fins" },
                    new { Id = WorkoutIntervalTypes.DrillOtherId,  Name = "Drill (other)" },
                    new { Id = WorkoutIntervalTypes.FreestyleSeriesId,  Name = "Freestyle series" },
                    new { Id = WorkoutIntervalTypes.FreestyleSeriesWithPullBuoyId,  Name = "Freestyle series with pull-buoy" },
                    new { Id = WorkoutIntervalTypes.FreestyleSeriesWithPaddlesId,  Name = "Freestyle series with paddles" },
                    new { Id = WorkoutIntervalTypes.FinalQuickFreestyleId,  Name = "Final quick freestyle" },
                    new { Id = WorkoutIntervalTypes.BackstrokeId, Name = "Backstroke" },
                    new { Id = WorkoutIntervalTypes.ManuallyAddedId, Name = "Manually added" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutIntervalLengths_WorkoutIntervalId",
                table: "WorkoutIntervalLengths",
                column: "WorkoutIntervalId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutIntervals_WorkoutId",
                table: "WorkoutIntervals",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutIntervals_WorkoutIntervalTypeId",
                table: "WorkoutIntervals",
                column: "WorkoutIntervalTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

            migrationBuilder.DropTable(
                name: "WorkoutIntervalLengths");

            migrationBuilder.DropTable(
                name: "WorkoutIntervals");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "WorkoutIntervalType");
        }
    }
}
