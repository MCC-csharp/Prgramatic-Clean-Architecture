﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookify.Infrastructure.Migrations;

/// <inheritdoc />
public partial class authorization_database_update : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "identity_id",
            table: "users",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "user_name",
            table: "users",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.CreateIndex(
            name: "ix_users_identity_id",
            table: "users",
            column: "identity_id",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "ix_users_identity_id",
            table: "users");

        migrationBuilder.DropColumn(
            name: "identity_id",
            table: "users");

        migrationBuilder.DropColumn(
            name: "user_name",
            table: "users");
    }
}
