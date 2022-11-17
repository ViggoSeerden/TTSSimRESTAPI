﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTSSimRESTAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewsDateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Edited",
                table: "News",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Edited",
                table: "News");
        }
    }
}
