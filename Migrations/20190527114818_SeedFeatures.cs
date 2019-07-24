﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace aspdotnetcoreangular.Migrations
{
    public partial class SeedFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Features(Name) Values('Feature1') ");
            migrationBuilder.Sql("INSERT INTO Features(Name) Values ('Feature2')");
            migrationBuilder.Sql("INSERT INTO Features(Name) Values ('Feature3')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Features");
        }
    }
}
