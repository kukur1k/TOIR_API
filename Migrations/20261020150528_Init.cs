using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace toir_api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TOIR");


            migrationBuilder.CreateTable(
                name: "action_type",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("action_type_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "brigade_specialisation",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("brigade_specialisation_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "brigade_type",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("brigade_type_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "entity_type",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("entity_type_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "eq_status",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("eq_status_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipment",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    invent_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    model = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    serial_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    date_start_exp = table.Column<DateOnly>(type: "date", nullable: true),
                    date_start_work = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("equipment_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "material",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    unit_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    quantity_in_stock = table.Column<decimal>(type: "numeric", nullable: true),
                    min_quantity = table.Column<decimal>(type: "numeric", nullable: true),
                    cost = table.Column<decimal>(type: "numeric", nullable: true),
                    shelf_in_stock = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    cell_in_stock = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("material_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "priority_work_order",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("priority_work_order_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("roles_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "status_work_order",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("status_work_order_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type_to",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    interval = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    duration = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    need_stop = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("type_to_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type_work_order",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("type_work_order_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_status",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_status_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "brigade",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    specialisation_id = table.Column<int>(type: "integer", nullable: true),
                    brigade_type_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("brigade_pkey", x => x.id);
                    table.ForeignKey(
                        name: "brigade_brigade_type_id_fkey",
                        column: x => x.brigade_type_id,
                        principalSchema: "TOIR",
                        principalTable: "brigade_type",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "brigade_specialisation_id_fkey",
                        column: x => x.specialisation_id,
                        principalSchema: "TOIR",
                        principalTable: "brigade_specialisation",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "eq_data",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    eq_id = table.Column<int>(type: "integer", nullable: true),
                    manufacture = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    warrancy_time = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    date_create_record = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    date_last_edit_record = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    last_to_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    last_editer_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("eq_data_pkey", x => x.id);
                    table.ForeignKey(
                        name: "eq_data_eq_id_fkey",
                        column: x => x.eq_id,
                        principalSchema: "TOIR",
                        principalTable: "equipment",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "eq_history_status_update",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    eq_id = table.Column<int>(type: "integer", nullable: true),
                    old_status = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    new_status = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("eq_history_status_update_pkey", x => x.id);
                    table.ForeignKey(
                        name: "eq_history_status_update_eq_id_fkey",
                        column: x => x.eq_id,
                        principalSchema: "TOIR",
                        principalTable: "equipment",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "material_history",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    material_id = table.Column<int>(type: "integer", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    cost = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("material_history_pkey", x => x.id);
                    table.ForeignKey(
                        name: "material_history_material_id_fkey",
                        column: x => x.material_id,
                        principalSchema: "TOIR",
                        principalTable: "material",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "shedule_to",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_to_id = table.Column<int>(type: "integer", nullable: true),
                    eq_id = table.Column<int>(type: "integer", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("shedule_to_pkey", x => x.id);
                    table.ForeignKey(
                        name: "shedule_to_eq_id_fkey",
                        column: x => x.eq_id,
                        principalSchema: "TOIR",
                        principalTable: "equipment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "shedule_to_type_to_id_fkey",
                        column: x => x.type_to_id,
                        principalSchema: "TOIR",
                        principalTable: "type_to",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    last_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    first_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    patronymic = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    date_registration = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    role_id = table.Column<int>(type: "integer", nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: true),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pkey", x => x.id);
                    table.ForeignKey(
                        name: "user_role_id_fkey",
                        column: x => x.role_id,
                        principalSchema: "TOIR",
                        principalTable: "roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "user_status_id_fkey",
                        column: x => x.status_id,
                        principalSchema: "TOIR",
                        principalTable: "user_status",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "brigade_fixation",
                schema: "TOIR",
                columns: table => new
                {
                    brigade_id = table.Column<int>(type: "integer", nullable: false),
                    workshop_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("brigade_fixation_pkey", x => new { x.brigade_id, x.workshop_id });
                    table.ForeignKey(
                        name: "brigade_fixation_brigade_id_fkey",
                        column: x => x.brigade_id,
                        principalSchema: "TOIR",
                        principalTable: "brigade",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "brigade_shedule",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    brigade_id = table.Column<int>(type: "integer", nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("brigade_shedule_pkey", x => x.id);
                    table.ForeignKey(
                        name: "brigade_shedule_brigade_id_fkey",
                        column: x => x.brigade_id,
                        principalSchema: "TOIR",
                        principalTable: "brigade",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "brigade_roster",
                schema: "TOIR",
                columns: table => new
                {
                    brigade_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("brigade_roster_pkey", x => new { x.brigade_id, x.user_id });
                    table.ForeignKey(
                        name: "brigade_roster_brigade_id_fkey",
                        column: x => x.brigade_id,
                        principalSchema: "TOIR",
                        principalTable: "brigade",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "brigade_roster_user_id_fkey",
                        column: x => x.user_id,
                        principalSchema: "TOIR",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "operations",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    entity_type_id = table.Column<int>(type: "integer", nullable: true),
                    entity_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    action_type_id = table.Column<int>(type: "integer", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    old_value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    new_value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("operations_pkey", x => x.id);
                    table.ForeignKey(
                        name: "operations_action_type_id_fkey",
                        column: x => x.action_type_id,
                        principalSchema: "TOIR",
                        principalTable: "action_type",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "operations_entity_type_id_fkey",
                        column: x => x.entity_type_id,
                        principalSchema: "TOIR",
                        principalTable: "entity_type",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "operations_user_id_fkey",
                        column: x => x.user_id,
                        principalSchema: "TOIR",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    token = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()"),
                    is_revoked = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("refresh_tokens_pkey", x => x.id);
                    table.ForeignKey(
                        name: "refresh_tokens_user_id_fkey",
                        column: x => x.user_id,
                        principalSchema: "TOIR",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "work_order",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_id = table.Column<int>(type: "integer", nullable: true),
                    priority_id = table.Column<int>(type: "integer", nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: true),
                    date_create = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    reason = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    master_id = table.Column<int>(type: "integer", nullable: true),
                    eq_id = table.Column<int>(type: "integer", nullable: true),
                    plan_date_start = table.Column<DateOnly>(type: "date", nullable: true),
                    plan_date_end = table.Column<DateOnly>(type: "date", nullable: true),
                    date_start = table.Column<DateOnly>(type: "date", nullable: true),
                    date_end = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("work_order_pkey", x => x.id);
                    table.ForeignKey(
                        name: "work_order_eq_id_fkey",
                        column: x => x.eq_id,
                        principalSchema: "TOIR",
                        principalTable: "equipment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "work_order_master_id_fkey",
                        column: x => x.master_id,
                        principalSchema: "TOIR",
                        principalTable: "user",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "work_order_priority_id_fkey",
                        column: x => x.priority_id,
                        principalSchema: "TOIR",
                        principalTable: "priority_work_order",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "work_order_status_id_fkey",
                        column: x => x.status_id,
                        principalSchema: "TOIR",
                        principalTable: "status_work_order",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "work_order_type_id_fkey",
                        column: x => x.type_id,
                        principalSchema: "TOIR",
                        principalTable: "type_work_order",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "workshop",
                schema: "TOIR",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    master_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("workshop_pkey", x => x.id);
                    table.ForeignKey(
                        name: "workshop_master_id_fkey",
                        column: x => x.master_id,
                        principalSchema: "TOIR",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "materials_work_order",
                schema: "TOIR",
                columns: table => new
                {
                    work_order_id = table.Column<int>(type: "integer", nullable: false),
                    material_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("materials_work_order_pkey", x => new { x.work_order_id, x.material_id });
                    table.ForeignKey(
                        name: "materials_work_order_material_id_fkey",
                        column: x => x.material_id,
                        principalSchema: "TOIR",
                        principalTable: "material",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "materials_work_order_work_order_id_fkey",
                        column: x => x.work_order_id,
                        principalSchema: "TOIR",
                        principalTable: "work_order",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "eq_state",
                schema: "TOIR",
                columns: table => new
                {
                    equipment_id = table.Column<int>(type: "integer", nullable: false),
                    eq_status_id = table.Column<int>(type: "integer", nullable: false),
                    workshop_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("eq_state_pkey", x => new { x.equipment_id, x.eq_status_id, x.workshop_id });
                    table.ForeignKey(
                        name: "eq_state_eq_status_id_fkey",
                        column: x => x.eq_status_id,
                        principalSchema: "TOIR",
                        principalTable: "eq_status",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "eq_state_equipment_id_fkey",
                        column: x => x.equipment_id,
                        principalSchema: "TOIR",
                        principalTable: "equipment",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "eq_state_workshop_id_fkey",
                        column: x => x.workshop_id,
                        principalSchema: "TOIR",
                        principalTable: "workshop",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_brigade_brigade_type_id",
                schema: "TOIR",
                table: "brigade",
                column: "brigade_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_brigade_specialisation_id",
                schema: "TOIR",
                table: "brigade",
                column: "specialisation_id");

            migrationBuilder.CreateIndex(
                name: "IX_brigade_roster_user_id",
                schema: "TOIR",
                table: "brigade_roster",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_brigade_shedule_brigade_id",
                schema: "TOIR",
                table: "brigade_shedule",
                column: "brigade_id");

            migrationBuilder.CreateIndex(
                name: "IX_eq_data_eq_id",
                schema: "TOIR",
                table: "eq_data",
                column: "eq_id");

            migrationBuilder.CreateIndex(
                name: "IX_eq_history_status_update_eq_id",
                schema: "TOIR",
                table: "eq_history_status_update",
                column: "eq_id");

            migrationBuilder.CreateIndex(
                name: "IX_eq_state_eq_status_id",
                schema: "TOIR",
                table: "eq_state",
                column: "eq_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_eq_state_workshop_id",
                schema: "TOIR",
                table: "eq_state",
                column: "workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_material_history_material_id",
                schema: "TOIR",
                table: "material_history",
                column: "material_id");

            migrationBuilder.CreateIndex(
                name: "IX_materials_work_order_material_id",
                schema: "TOIR",
                table: "materials_work_order",
                column: "material_id");

            migrationBuilder.CreateIndex(
                name: "IX_operations_action_type_id",
                schema: "TOIR",
                table: "operations",
                column: "action_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_operations_entity_type_id",
                schema: "TOIR",
                table: "operations",
                column: "entity_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_operations_user_id",
                schema: "TOIR",
                table: "operations",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_user_id",
                schema: "TOIR",
                table: "refresh_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_shedule_to_eq_id",
                schema: "TOIR",
                table: "shedule_to",
                column: "eq_id");

            migrationBuilder.CreateIndex(
                name: "IX_shedule_to_type_to_id",
                schema: "TOIR",
                table: "shedule_to",
                column: "type_to_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_id",
                schema: "TOIR",
                table: "user",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_status_id",
                schema: "TOIR",
                table: "user",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_order_eq_id",
                schema: "TOIR",
                table: "work_order",
                column: "eq_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_order_master_id",
                schema: "TOIR",
                table: "work_order",
                column: "master_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_order_priority_id",
                schema: "TOIR",
                table: "work_order",
                column: "priority_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_order_status_id",
                schema: "TOIR",
                table: "work_order",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_order_type_id",
                schema: "TOIR",
                table: "work_order",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_workshop_master_id",
                schema: "TOIR",
                table: "workshop",
                column: "master_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "brigade_fixation",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "brigade_roster",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "brigade_shedule",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "eq_data",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "eq_history_status_update",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "eq_state",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "material_history",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "materials_work_order",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "operations",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "refresh_tokens",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "shedule_to",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "brigade",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "eq_status",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "workshop",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "material",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "work_order",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "action_type",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "entity_type",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "type_to",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "brigade_type",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "brigade_specialisation",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "equipment",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "user",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "priority_work_order",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "status_work_order",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "type_work_order",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "TOIR");

            migrationBuilder.DropTable(
                name: "user_status",
                schema: "TOIR");
        }
    }
}
