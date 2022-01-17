﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Api_Pcto.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eletelecamere",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", nullable: false),
                    link = table.Column<string>(type: "TEXT", nullable: false),
                    num_like = table.Column<int>(type: "INTEGER", nullable: false),
                    num_salvati = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eletelecamere", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eletelecamere");
        }
    }
}
