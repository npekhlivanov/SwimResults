﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataModels.Workout", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<double>("ActiveTime")
                        .HasColumnType("float");

                    b.Property<double>("CourseLength")
                        .HasColumnType("float");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<double>("Duration")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<double>("Pace")
                        .HasColumnType("float");

                    b.Property<string>("Place")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("WorkoutDate")
                        .HasColumnName("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("DataModels.WorkoutInterval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Distance")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("float")
                        .HasComputedColumnSql("dbo.fnGetIntervalDistance(Id)");

                    b.Property<double>("Duration")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("float")
                        .HasComputedColumnSql("dbo.fnGetIntervalDuration(Id)");

                    b.Property<int?>("IntervalNo")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<double?>("Pace")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("float")
                        .HasComputedColumnSql("case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 100 / dbo.fnGetIntervalDistance(Id) end");

                    b.Property<double>("StrokeCount")
                        .HasColumnType("float");

                    b.Property<int?>("StrokeTypeId")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("int")
                        .HasComputedColumnSql("dbo.fnGetIntervalStrokeType(Id)");

                    b.Property<double?>("Swolf")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("float")
                        .HasComputedColumnSql("case when dbo.fnGetIntervalDistance(Id)=0 then null else dbo.fnGetIntervalDuration(Id) * 50 / dbo.fnGetIntervalDistance(Id) + StrokeCount end");

                    b.Property<double>("TimeOffset")
                        .HasColumnType("float");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.Property<int?>("WorkoutIntervalTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.HasIndex("WorkoutIntervalTypeId");

                    b.ToTable("WorkoutIntervals");
                });

            modelBuilder.Entity("DataModels.WorkoutIntervalLength", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<double>("Duration")
                        .HasColumnType("float");

                    b.Property<int?>("LengthNo")
                        .HasColumnType("int");

                    b.Property<int>("StrokeCount")
                        .HasColumnType("int");

                    b.Property<int>("StrokeTypeId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutIntervalId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutIntervalId");

                    b.ToTable("WorkoutIntervalLengths");
                });

            modelBuilder.Entity("DataModels.WorkoutIntervalType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("WorkoutIntervalType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Warm up"
                        },
                        new
                        {
                            Id = 2,
                            Name = "First quick freestyle"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Second quick freestyle"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Drill with fins"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Drill (other)"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Freestyle series"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Freestyle series with pull-buoy"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Freestyle series with paddles"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Final quick freestyle"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Backstroke"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Manually added"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Pre warm-up"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Intermediate quick freestyle"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Final quick freestyle 2"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Other freestyle"
                        });
                });

            modelBuilder.Entity("DataModels.WorkoutInterval", b =>
                {
                    b.HasOne("DataModels.Workout", "Workout")
                        .WithMany("Intervals")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataModels.WorkoutIntervalType", "WorkoutIntervalType")
                        .WithMany()
                        .HasForeignKey("WorkoutIntervalTypeId");
                });

            modelBuilder.Entity("DataModels.WorkoutIntervalLength", b =>
                {
                    b.HasOne("DataModels.WorkoutInterval", "WorkoutInterval")
                        .WithMany("Lengths")
                        .HasForeignKey("WorkoutIntervalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
