using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lowtab.Monster.Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "group_tags");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tags",
                table: "tags");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "tags",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "articles",
                newName: "preview_image_url");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "tags",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "group",
                table: "tags",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "body",
                table: "articles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "articles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tags",
                table: "tags",
                columns: new[] { "id", "group" });

            migrationBuilder.CreateTable(
                name: "article_entity_tag_entity",
                columns: table => new
                {
                    article_entity_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tags_id = table.Column<string>(type: "text", nullable: false),
                    tags_group = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_article_entity_tag_entity", x => new { x.article_entity_id, x.tags_id, x.tags_group });
                    table.ForeignKey(
                        name: "fk_article_entity_tag_entity_articles_article_entity_id",
                        column: x => x.article_entity_id,
                        principalTable: "articles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_article_entity_tag_entity_tags_tags_id_tags_group",
                        columns: x => new { x.tags_id, x.tags_group },
                        principalTable: "tags",
                        principalColumns: new[] { "id", "group" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_article_entity_tag_entity_tags_id_tags_group",
                table: "article_entity_tag_entity",
                columns: new[] { "tags_id", "tags_group" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_entity_tag_entity");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tags",
                table: "tags");

            migrationBuilder.DropColumn(
                name: "group",
                table: "tags");

            migrationBuilder.DropColumn(
                name: "body",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "title",
                table: "articles");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "tags",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "preview_image_url",
                table: "articles",
                newName: "name");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tags",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tags",
                table: "tags",
                column: "id");

            migrationBuilder.CreateTable(
                name: "group_tags",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_group_tags", x => x.id);
                });
        }
    }
}
