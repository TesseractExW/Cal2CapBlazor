using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cal2CapBlazor.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMealAccountForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", maxLength: 90, nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", maxLength: 24, nullable: false),
                    HashedPassword = table.Column<string>(type: "TEXT", maxLength: 90, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MealName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MealDetails = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    MealType = table.Column<int>(type: "INTEGER", nullable: false),
                    InTakeTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NutrientProfile = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_AccountId",
                table: "Meals",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
