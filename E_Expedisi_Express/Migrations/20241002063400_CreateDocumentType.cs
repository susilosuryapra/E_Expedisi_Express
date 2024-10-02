using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Expedisi_Express.Migrations
{
    /// <inheritdoc />
    public partial class CreateDocumentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "DocumentType",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "DocumentType",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DocumentType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "DocumentType",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DocumentType");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DocumentType");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "DocumentType",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "DocumentType",
                newName: "CreatedAt");
        }
    }
}
