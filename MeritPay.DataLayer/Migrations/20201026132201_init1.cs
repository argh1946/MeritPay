using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeritPay.Infrastructure.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MeritPay");

            migrationBuilder.CreateTable(
                name: "AdjustmentFactor",
                schema: "MeritPay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Ratio = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdjustmentFactor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                schema: "MeritPay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchCode = table.Column<int>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    ZoneCode = table.Column<int>(nullable: false),
                    ZoneName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupingRatio",
                schema: "MeritPay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Ratio = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupingRatio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeritPayType",
                schema: "MeritPay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeritPayType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Period",
                schema: "MeritPay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodTitle = table.Column<string>(maxLength: 300, nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Period", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "MeritPay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonCode = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    BirthDate = table.Column<string>(nullable: true),
                    EmployeeDate = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    StudyBranch = table.Column<string>(nullable: true),
                    StudyJob = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchGrouping",
                schema: "MeritPay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupingRatioId = table.Column<int>(nullable: false),
                    AdjustmentFactorId = table.Column<int>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    PeriodId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchGrouping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchGrouping_AdjustmentFactor_AdjustmentFactorId",
                        column: x => x.AdjustmentFactorId,
                        principalSchema: "MeritPay",
                        principalTable: "AdjustmentFactor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchGrouping_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "MeritPay",
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchGrouping_GroupingRatio_GroupingRatioId",
                        column: x => x.GroupingRatioId,
                        principalSchema: "MeritPay",
                        principalTable: "GroupingRatio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchGrouping_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalSchema: "MeritPay",
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeritPayFactor",
                schema: "MeritPay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodId = table.Column<int>(nullable: false),
                    MeritPayTypeId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Ratio = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeritPayFactor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeritPayFactor_MeritPayType_MeritPayTypeId",
                        column: x => x.MeritPayTypeId,
                        principalSchema: "MeritPay",
                        principalTable: "MeritPayType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeritPayFactor_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalSchema: "MeritPay",
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeritPayLimit",
                schema: "MeritPay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodId = table.Column<int>(nullable: false),
                    MaxDay = table.Column<int>(nullable: false),
                    MinDay = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeritPayLimit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeritPayLimit_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalSchema: "MeritPay",
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonInBranch",
                schema: "MeritPay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    BranchId = table.Column<int>(nullable: false),
                    MoveDate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonInBranch_Branch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "MeritPay",
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonInBranch_Person_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "MeritPay",
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BranchGroupingInPeriod",
                schema: "MeritPay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchGroupingId = table.Column<int>(nullable: false),
                    PublicSource = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DocumentCount = table.Column<int>(nullable: false),
                    Facilities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PeriodId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchGroupingInPeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchGroupingInPeriod_BranchGrouping_BranchGroupingId",
                        column: x => x.BranchGroupingId,
                        principalSchema: "MeritPay",
                        principalTable: "BranchGrouping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchGroupingInPeriod_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalSchema: "MeritPay",
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScoreIndex",
                schema: "MeritPay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeritPayFactorId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Ratio = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreIndex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoreIndex_MeritPayFactor_MeritPayFactorId",
                        column: x => x.MeritPayFactorId,
                        principalSchema: "MeritPay",
                        principalTable: "MeritPayFactor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonArzeshyabi",
                schema: "MeritPay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodId = table.Column<int>(nullable: false),
                    PersonInBranchId = table.Column<int>(nullable: false),
                    ArzyabiDate = table.Column<string>(nullable: true),
                    Arzyab1 = table.Column<int>(nullable: false),
                    Arzyab2 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonArzeshyabi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonArzeshyabi_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalSchema: "MeritPay",
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonArzeshyabi_PersonInBranch_PersonInBranchId",
                        column: x => x.PersonInBranchId,
                        principalSchema: "MeritPay",
                        principalTable: "PersonInBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                schema: "MeritPay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeritPayFactorId = table.Column<int>(nullable: false),
                    PersonInBranchId = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    RankInBranch = table.Column<int>(nullable: false),
                    RankInZone = table.Column<int>(nullable: false),
                    RankInBank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_MeritPayFactor_MeritPayFactorId",
                        column: x => x.MeritPayFactorId,
                        principalSchema: "MeritPay",
                        principalTable: "MeritPayFactor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Report_PersonInBranch_PersonInBranchId",
                        column: x => x.PersonInBranchId,
                        principalSchema: "MeritPay",
                        principalTable: "PersonInBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScoreSubIndex",
                schema: "MeritPay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScoreIndexId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Ratio = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreSubIndex", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoreSubIndex_ScoreIndex_ScoreIndexId",
                        column: x => x.ScoreIndexId,
                        principalSchema: "MeritPay",
                        principalTable: "ScoreIndex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonScore",
                schema: "MeritPay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScoreSubIndexId = table.Column<int>(nullable: false),
                    PersonInBranchId = table.Column<int>(nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Score = table.Column<int>(nullable: false),
                    RankInBranch = table.Column<int>(nullable: false),
                    RankInZone = table.Column<int>(nullable: false),
                    RankInBank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonScore_PersonInBranch_PersonInBranchId",
                        column: x => x.PersonInBranchId,
                        principalSchema: "MeritPay",
                        principalTable: "PersonInBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonScore_ScoreSubIndex_ScoreSubIndexId",
                        column: x => x.ScoreSubIndexId,
                        principalSchema: "MeritPay",
                        principalTable: "ScoreSubIndex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BranchGrouping_AdjustmentFactorId",
                schema: "MeritPay",
                table: "BranchGrouping",
                column: "AdjustmentFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchGrouping_BranchId",
                schema: "MeritPay",
                table: "BranchGrouping",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchGrouping_GroupingRatioId",
                schema: "MeritPay",
                table: "BranchGrouping",
                column: "GroupingRatioId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchGrouping_PeriodId",
                schema: "MeritPay",
                table: "BranchGrouping",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchGroupingInPeriod_BranchGroupingId",
                schema: "MeritPay",
                table: "BranchGroupingInPeriod",
                column: "BranchGroupingId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchGroupingInPeriod_PeriodId",
                schema: "MeritPay",
                table: "BranchGroupingInPeriod",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_MeritPayFactor_MeritPayTypeId",
                schema: "MeritPay",
                table: "MeritPayFactor",
                column: "MeritPayTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MeritPayFactor_PeriodId",
                schema: "MeritPay",
                table: "MeritPayFactor",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_MeritPayLimit_PeriodId",
                schema: "MeritPay",
                table: "MeritPayLimit",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonArzeshyabi_PeriodId",
                schema: "MeritPay",
                table: "PersonArzeshyabi",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonArzeshyabi_PersonInBranchId",
                schema: "MeritPay",
                table: "PersonArzeshyabi",
                column: "PersonInBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonInBranch_BranchId",
                schema: "MeritPay",
                table: "PersonInBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonInBranch_PersonId",
                schema: "MeritPay",
                table: "PersonInBranch",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonScore_PersonInBranchId",
                schema: "MeritPay",
                table: "PersonScore",
                column: "PersonInBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonScore_ScoreSubIndexId",
                schema: "MeritPay",
                table: "PersonScore",
                column: "ScoreSubIndexId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_MeritPayFactorId",
                schema: "MeritPay",
                table: "Report",
                column: "MeritPayFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_PersonInBranchId",
                schema: "MeritPay",
                table: "Report",
                column: "PersonInBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreIndex_MeritPayFactorId",
                schema: "MeritPay",
                table: "ScoreIndex",
                column: "MeritPayFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSubIndex_ScoreIndexId",
                schema: "MeritPay",
                table: "ScoreSubIndex",
                column: "ScoreIndexId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchGroupingInPeriod",
                schema: "MeritPay");

            migrationBuilder.DropTable(
                name: "MeritPayLimit",
                schema: "MeritPay");

            migrationBuilder.DropTable(
                name: "PersonArzeshyabi",
                schema: "MeritPay");

            migrationBuilder.DropTable(
                name: "PersonScore",
                schema: "MeritPay");

            migrationBuilder.DropTable(
                name: "Report",
                schema: "MeritPay");

            migrationBuilder.DropTable(
                name: "BranchGrouping",
                schema: "MeritPay");

            migrationBuilder.DropTable(
                name: "ScoreSubIndex",
                schema: "MeritPay");

            migrationBuilder.DropTable(
                name: "PersonInBranch",
                schema: "MeritPay");

            migrationBuilder.DropTable(
                name: "AdjustmentFactor",
                schema: "MeritPay");

            migrationBuilder.DropTable(
                name: "GroupingRatio",
                schema: "MeritPay");

            migrationBuilder.DropTable(
                name: "ScoreIndex",
                schema: "MeritPay");

            migrationBuilder.DropTable(
                name: "Branch",
                schema: "MeritPay");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "MeritPay");

            migrationBuilder.DropTable(
                name: "MeritPayFactor",
                schema: "MeritPay");

            migrationBuilder.DropTable(
                name: "MeritPayType",
                schema: "MeritPay");

            migrationBuilder.DropTable(
                name: "Period",
                schema: "MeritPay");
        }
    }
}
