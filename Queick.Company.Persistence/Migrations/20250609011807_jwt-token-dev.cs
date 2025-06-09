using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Queick.Company.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class jwttokendev : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Branches_BranchId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Customer_CustomerId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Employee_EmployeeId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Location_LocationId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Companies_CompanyDomainId",
                table: "Branches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "Branches");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameColumn(
                name: "CompanyDomainId",
                table: "Branches",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Branches_CompanyDomainId",
                table: "Branches",
                newName: "IX_Branches_CompanyId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Addresses",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Addresses",
                newName: "IsVerified");

            migrationBuilder.RenameIndex(
                name: "IX_Address_LocationId",
                table: "Addresses",
                newName: "IX_Addresses_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_EmployeeId",
                table: "Addresses",
                newName: "IX_Addresses_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_CustomerId",
                table: "Addresses",
                newName: "IX_Addresses_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_BranchId",
                table: "Addresses",
                newName: "IX_Addresses_BranchId");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Updated",
                table: "Employee",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Employee",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Employee",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Updated",
                table: "Customer",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Customer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Customer",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Updated",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Companies",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Updated",
                table: "CommunicationInfo",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CommunicationInfo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "CommunicationInfo",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Updated",
                table: "Branches",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Branches",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Branches",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Updated",
                table: "Addresses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LocationId",
                table: "Addresses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "EmployeeId",
                table: "Addresses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "Addresses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "BranchId",
                table: "Addresses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "AddressFunctionType",
                table: "Addresses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AddressLocationType",
                table: "Addresses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AddressTitle",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApartmentNo",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BuildingNumber",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactPerson",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountyDistrict",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Addresses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryNotes",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DoorNo",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FloorNo",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Addresses",
                type: "numeric(10,8)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Addresses",
                type: "numeric(11,8)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Neighborhood",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StateProvince",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Branches_BranchId",
                table: "Addresses",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Customer_CustomerId",
                table: "Addresses",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Employee_EmployeeId",
                table: "Addresses",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Location_LocationId",
                table: "Addresses",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Companies_CompanyId",
                table: "Branches",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Branches_BranchId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Customer_CustomerId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Employee_EmployeeId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Location_LocationId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Companies_CompanyId",
                table: "Branches");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CommunicationInfo");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CommunicationInfo");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "AddressFunctionType",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressLocationType",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressTitle",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ApartmentNo",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "BuildingNumber",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ContactPerson",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CountyDistrict",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "DeliveryNotes",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "DoorNo",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "FloorNo",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Neighborhood",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "StateProvince",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Branches",
                newName: "CompanyDomainId");

            migrationBuilder.RenameIndex(
                name: "IX_Branches_CompanyId",
                table: "Branches",
                newName: "IX_Branches_CompanyDomainId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Address",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "IsVerified",
                table: "Address",
                newName: "IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_LocationId",
                table: "Address",
                newName: "IX_Address_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_EmployeeId",
                table: "Address",
                newName: "IX_Address_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_CustomerId",
                table: "Address",
                newName: "IX_Address_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_BranchId",
                table: "Address",
                newName: "IX_Address_BranchId");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Updated",
                table: "Employee",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Updated",
                table: "Customer",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Updated",
                table: "Companies",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Updated",
                table: "CommunicationInfo",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Updated",
                table: "Branches",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "Branches",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Updated",
                table: "Address",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<long>(
                name: "LocationId",
                table: "Address",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EmployeeId",
                table: "Address",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "Address",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BranchId",
                table: "Address",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Branches_BranchId",
                table: "Address",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Customer_CustomerId",
                table: "Address",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Employee_EmployeeId",
                table: "Address",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Location_LocationId",
                table: "Address",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Companies_CompanyDomainId",
                table: "Branches",
                column: "CompanyDomainId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
