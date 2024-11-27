using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AimTrainerGameHighscores",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Score = table.Column<int>(type: "integer", nullable: false),
                Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AimTrainerGameHighscores", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "DotCountGameScores",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Value = table.Column<int>(type: "integer", nullable: false),
                Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DotCountGameScores", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Puzzles",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Content = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Puzzles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ReactionTimeGameScores",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Value = table.Column<int>(type: "integer", nullable: false),
                Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ReactionTimeGameScores", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AimTrainerGameHighscores");

        migrationBuilder.DropTable(
            name: "DotCountGameScores");

        migrationBuilder.DropTable(
            name: "Puzzles");

        migrationBuilder.DropTable(
            name: "ReactionTimeGameScores");
    }
}
