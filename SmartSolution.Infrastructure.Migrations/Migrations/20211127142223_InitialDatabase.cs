using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSolution.Infrastructure.Migrations.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateSequence(
                name: "OrganizationSeq",
                schema: "Identity",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "RoleSeq",
                schema: "Identity",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "UserSeq",
                schema: "Identity",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "requests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "Identity",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CrmTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(nullable: false),
                    RecordDateTime = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true),
                    LastUpdateDateTime = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Deadline = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrmTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrmTasks_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CrmTasks_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(nullable: false),
                    RecordDateTime = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<int>(nullable: true),
                    LastUpdateDateTime = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CrmTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskEmployees_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskEmployees_CrmTasks_CrmTaskId",
                        column: x => x.CrmTaskId,
                        principalTable: "CrmTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskEmployees_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskEmployees_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrmTasks_CreatedById",
                table: "CrmTasks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CrmTasks_UpdatedById",
                table: "CrmTasks",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_requests_Key",
                table: "requests",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskEmployees_CreatedById",
                table: "TaskEmployees",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEmployees_CrmTaskId",
                table: "TaskEmployees",
                column: "CrmTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEmployees_UpdatedById",
                table: "TaskEmployees",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEmployees_UserId",
                table: "TaskEmployees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                schema: "Identity",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                schema: "Identity",
                table: "Users",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                schema: "Identity",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                schema: "Identity",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "requests");

            migrationBuilder.DropTable(
                name: "TaskEmployees");

            migrationBuilder.DropTable(
                name: "CrmTasks");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Identity");

            migrationBuilder.DropSequence(
                name: "OrganizationSeq",
                schema: "Identity");

            migrationBuilder.DropSequence(
                name: "RoleSeq",
                schema: "Identity");

            migrationBuilder.DropSequence(
                name: "UserSeq",
                schema: "Identity");
        }
    }
}
