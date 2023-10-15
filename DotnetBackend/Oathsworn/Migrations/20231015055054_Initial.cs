using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oathsworn.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AbilityClass = table.Column<int>(type: "INTEGER", nullable: false),
                    Animus = table.Column<int>(type: "INTEGER", nullable: false),
                    Battleflow = table.Column<int>(type: "INTEGER", nullable: false),
                    Effects = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Boss = table.Column<string>(type: "TEXT", nullable: true),
                    Stage = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Encounters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encounters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FreeCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreeCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Defence = table.Column<int>(type: "INTEGER", nullable: false),
                    Might = table.Column<string>(type: "TEXT", nullable: true),
                    Battleflow = table.Column<int>(type: "INTEGER", nullable: false),
                    Effects = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bosses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EncounterId = table.Column<int>(type: "INTEGER", nullable: false),
                    Health = table.Column<string>(type: "TEXT", nullable: true),
                    Defence = table.Column<int>(type: "INTEGER", nullable: false),
                    XPosition = table.Column<int>(type: "INTEGER", nullable: false),
                    YPosition = table.Column<int>(type: "INTEGER", nullable: false),
                    Might = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bosses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bosses_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EncounterMightDecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EncounterId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsFreeCompanyDeck = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncounterMightDecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EncounterMightDecks_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Minons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EncounterId = table.Column<int>(type: "INTEGER", nullable: false),
                    Health = table.Column<int>(type: "INTEGER", nullable: false),
                    Defence = table.Column<int>(type: "INTEGER", nullable: false),
                    XPosition = table.Column<int>(type: "INTEGER", nullable: false),
                    YPosition = table.Column<int>(type: "INTEGER", nullable: false),
                    Might = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Minons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Minons_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FreeCompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Class = table.Column<int>(type: "INTEGER", nullable: false),
                    Health = table.Column<int>(type: "INTEGER", nullable: false),
                    Defence = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxAnimus = table.Column<int>(type: "INTEGER", nullable: false),
                    AnimusRegen = table.Column<int>(type: "INTEGER", nullable: false),
                    Might = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_FreeCompanies_FreeCompanyId",
                        column: x => x.FreeCompanyId,
                        principalTable: "FreeCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BossActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BossId = table.Column<int>(type: "INTEGER", nullable: false),
                    ActionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BossActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BossActions_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BossActions_Bosses_BossId",
                        column: x => x.BossId,
                        principalTable: "Bosses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EncounterPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EncounterId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    XPosition = table.Column<int>(type: "INTEGER", nullable: false),
                    YPosition = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentHealth = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentAnimus = table.Column<int>(type: "INTEGER", nullable: false),
                    Tokens = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncounterPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EncounterPlayers_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EncounterPlayers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MightCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeckId = table.Column<int>(type: "INTEGER", nullable: false),
                    AttackId = table.Column<int>(type: "INTEGER", nullable: true),
                    Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCritical = table.Column<bool>(type: "INTEGER", nullable: false),
                    EncounterMightDeckId = table.Column<int>(type: "INTEGER", nullable: true),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MightCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MightCards_EncounterMightDecks_EncounterMightDeckId",
                        column: x => x.EncounterMightDeckId,
                        principalTable: "EncounterMightDecks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MightCards_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlayerAbilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Battleflow = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerAbilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerAbilities_Abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerAbilities_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Battleflow = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerItems_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BossActions_ActionId",
                table: "BossActions",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_BossActions_BossId",
                table: "BossActions",
                column: "BossId");

            migrationBuilder.CreateIndex(
                name: "IX_Bosses_EncounterId",
                table: "Bosses",
                column: "EncounterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EncounterMightDecks_EncounterId",
                table: "EncounterMightDecks",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_EncounterPlayers_EncounterId",
                table: "EncounterPlayers",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_EncounterPlayers_PlayerId",
                table: "EncounterPlayers",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MightCards_EncounterMightDeckId",
                table: "MightCards",
                column: "EncounterMightDeckId");

            migrationBuilder.CreateIndex(
                name: "IX_MightCards_PlayerId",
                table: "MightCards",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Minons_EncounterId",
                table: "Minons",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAbilities_AbilityId",
                table: "PlayerAbilities",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerAbilities_PlayerId",
                table: "PlayerAbilities",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerItems_ItemId",
                table: "PlayerItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerItems_PlayerId",
                table: "PlayerItems",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_FreeCompanyId",
                table: "Players",
                column: "FreeCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BossActions");

            migrationBuilder.DropTable(
                name: "EncounterPlayers");

            migrationBuilder.DropTable(
                name: "MightCards");

            migrationBuilder.DropTable(
                name: "Minons");

            migrationBuilder.DropTable(
                name: "PlayerAbilities");

            migrationBuilder.DropTable(
                name: "PlayerItems");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "Bosses");

            migrationBuilder.DropTable(
                name: "EncounterMightDecks");

            migrationBuilder.DropTable(
                name: "Abilities");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Encounters");

            migrationBuilder.DropTable(
                name: "FreeCompanies");
        }
    }
}
