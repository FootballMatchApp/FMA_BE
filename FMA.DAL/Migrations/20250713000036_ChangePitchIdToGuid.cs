using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FMA.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangePitchIdToGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pitches",
                columns: table => new
                {
                    PitchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pitches", x => x.PitchId);
                    table.ForeignKey(
                        name: "FK_Pitches_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Teams_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    TokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchPosts",
                columns: table => new
                {
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PitchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceivingUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostByTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MatchTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPosts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_MatchPosts_Pitches_PitchId",
                        column: x => x.PitchId,
                        principalTable: "Pitches",
                        principalColumn: "PitchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchPosts_Teams_PostByTeamId",
                        column: x => x.PostByTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId");
                    table.ForeignKey(
                        name: "FK_MatchPosts_Users_PostById",
                        column: x => x.PostById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchPosts_Users_ReceivingUserId",
                        column: x => x.ReceivingUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    TeamMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.TeamMemberId);
                    table.ForeignKey(
                        name: "FK_TeamMembers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamMembers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchRequests",
                columns: table => new
                {
                    MatchRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestByTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DecisionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchRequests", x => x.MatchRequestId);
                    table.ForeignKey(
                        name: "FK_MatchRequests_MatchPosts_MatchPostId",
                        column: x => x.MatchPostId,
                        principalTable: "MatchPosts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchRequests_Teams_RequestByTeamId",
                        column: x => x.RequestByTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId");
                    table.ForeignKey(
                        name: "FK_MatchRequests_Users_RequestById",
                        column: x => x.RequestById,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PitchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    BookingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_MatchPosts_MatchPostId",
                        column: x => x.MatchPostId,
                        principalTable: "MatchPosts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_MatchRequests_MatchRequestId",
                        column: x => x.MatchRequestId,
                        principalTable: "MatchRequests",
                        principalColumn: "MatchRequestId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Pitches_PitchId",
                        column: x => x.PitchId,
                        principalTable: "Pitches",
                        principalColumn: "PitchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { new Guid("11111111-2222-3333-4444-555555555555"), "Admin" },
                    { new Guid("66666666-7777-8888-9999-000000000000"), "PitchOwner" },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"), "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Email", "PasswordHash", "PhoneNumber", "RoleId", "Username" },
                values: new object[,]
                {
                    { new Guid("12345678-90ab-cdef-1234-567890abcdef"), "123 Admin Street, Admin City, Admin Country", "admin@gmail.com", "$2a$11$rTz6DZiEeBqhVrzF25CgTOBPf41jpn2Tg/nnIqnX8KS6uIerB/1dm", "0123456789", new Guid("11111111-2222-3333-4444-555555555555"), "admin" },
                    { new Guid("22345678-90ab-cdef-1234-567890abcdef"), "456 Pitch Owner Street, Pitch Owner City, Pitch Owner Country", "pitchowner@gmail.com", "$2a$11$rTz6DZiEeBqhVrzF25CgTOBPf41jpn2Tg/nnIqnX8KS6uIerB/1dm", "0987654321", new Guid("66666666-7777-8888-9999-000000000000"), "pitchowner" },
                    { new Guid("32345678-90ab-cdef-1234-567890abcdef"), "789 User1 Street, User1 City, User1 Country", "user-1@gmail.com", "$2a$11$rTz6DZiEeBqhVrzF25CgTOBPf41jpn2Tg/nnIqnX8KS6uIerB/1dm", "1234567890", new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"), "user1" },
                    { new Guid("42345678-90ab-cdef-1234-567890abcdef"), "101 User2 Street, User2 City, User2 Country", "user-2@gmail.com", "$2a$11$rTz6DZiEeBqhVrzF25CgTOBPf41jpn2Tg/nnIqnX8KS6uIerB/1dm", "1234567890", new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"), "user2" },
                    { new Guid("52345678-90ab-cdef-1234-567890abcdef"), "102 User3 Street, User3 City, User3 Country", "user-3@gmail.com", "$2a$11$rTz6DZiEeBqhVrzF25CgTOBPf41jpn2Tg/nnIqnX8KS6uIerB/1dm", "1234567890", new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"), "user3" }
                });

            migrationBuilder.InsertData(
                table: "Pitches",
                columns: new[] { "PitchId", "ContactNumber", "Latitude", "Location", "Longitude", "Name", "OwnerId", "PricePerHour", "Status" },
                values: new object[] { new Guid("a2345678-90ab-cdef-1234-567890abcdef"), "0123456789", 10.762622, "Đại học Bách Khoa – Đại học Quốc gia TP.HCM, quận 10, Thành phố Hồ Chí Minh, Việt Nam.", 106.660172, "SAN BONG DA CUC KY DANG CAP", new Guid("22345678-90ab-cdef-1234-567890abcdef"), 300m, 0 });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "CreatedById", "Description", "TeamName" },
                values: new object[,]
                {
                    { new Guid("23456789-0abc-def1-2345-67890abcdefa"), new Guid("32345678-90ab-cdef-1234-567890abcdef"), null, "FPT FC" },
                    { new Guid("b3456789-0abc-def1-2345-67890abcdefa"), new Guid("42345678-90ab-cdef-1234-567890abcdef"), null, "Thunder FC" }
                });

            migrationBuilder.InsertData(
                table: "TeamMembers",
                columns: new[] { "TeamMemberId", "JoinDate", "Position", "TeamId", "UserId" },
                values: new object[,]
                {
                    { new Guid("62345678-90ab-cdef-1234-567890abcdef"), new DateTime(1990, 1, 1, 7, 0, 0, 0, DateTimeKind.Local), "Forward", new Guid("23456789-0abc-def1-2345-67890abcdefa"), new Guid("32345678-90ab-cdef-1234-567890abcdef") },
                    { new Guid("72345678-90ab-cdef-1234-567890abcdef"), new DateTime(1990, 1, 1, 7, 0, 0, 0, DateTimeKind.Local), "Midfielder", new Guid("23456789-0abc-def1-2345-67890abcdefa"), new Guid("42345678-90ab-cdef-1234-567890abcdef") },
                    { new Guid("82345678-90ab-cdef-1234-567890abcdef"), new DateTime(1990, 1, 1, 7, 0, 0, 0, DateTimeKind.Local), "Defender", new Guid("b3456789-0abc-def1-2345-67890abcdefa"), new Guid("52345678-90ab-cdef-1234-567890abcdef") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_MatchPostId",
                table: "Bookings",
                column: "MatchPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_MatchRequestId",
                table: "Bookings",
                column: "MatchRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PitchId",
                table: "Bookings",
                column: "PitchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPosts_PitchId",
                table: "MatchPosts",
                column: "PitchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPosts_PostById",
                table: "MatchPosts",
                column: "PostById");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPosts_PostByTeamId",
                table: "MatchPosts",
                column: "PostByTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPosts_ReceivingUserId",
                table: "MatchPosts",
                column: "ReceivingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchRequests_MatchPostId",
                table: "MatchRequests",
                column: "MatchPostId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchRequests_RequestById",
                table: "MatchRequests",
                column: "RequestById");

            migrationBuilder.CreateIndex(
                name: "IX_MatchRequests_RequestByTeamId",
                table: "MatchRequests",
                column: "RequestByTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Pitches_OwnerId",
                table: "Pitches",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_TeamId",
                table: "TeamMembers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_UserId",
                table: "TeamMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CreatedById",
                table: "Teams",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "MatchRequests");

            migrationBuilder.DropTable(
                name: "MatchPosts");

            migrationBuilder.DropTable(
                name: "Pitches");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
