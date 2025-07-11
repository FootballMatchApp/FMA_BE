using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FMA.DAL.Context
{
    public class DbSeeder
    {
        private readonly FootballMatchAppContext _context;

        // Role
        private static readonly Guid AdminRole = Guid.Parse("11111111-2222-3333-4444-555555555555");
        private static readonly Guid PitchOwnerRole = Guid.Parse("66666666-7777-8888-9999-000000000000");
        private static readonly Guid UserRole = Guid.Parse("AAAAAAAA-BBBB-CCCC-DDDD-EEEEEEEEEEEE");

        // User
        private static readonly Guid AdminId = Guid.Parse("12345678-90AB-CDEF-1234-567890ABCDEF");
        private static readonly Guid PitchOwnerId = Guid.Parse("22345678-90AB-CDEF-1234-567890ABCDEF");
        private static readonly Guid UserId_1 = Guid.Parse("32345678-90AB-CDEF-1234-567890ABCDEF");
        private static readonly Guid UserId_2 = Guid.Parse("42345678-90AB-CDEF-1234-567890ABCDEF");
        private static readonly Guid UserId_3 = Guid.Parse("52345678-90AB-CDEF-1234-567890ABCDEF");

        //Player
        private static readonly Guid Player_1 = Guid.Parse("62345678-90AB-CDEF-1234-567890ABCDEF");
        private static readonly Guid Player_2 = Guid.Parse("72345678-90AB-CDEF-1234-567890ABCDEF");
        private static readonly Guid Player_3 = Guid.Parse("82345678-90AB-CDEF-1234-567890ABCDEF");



        //Pitch
        private static readonly Guid Pitch_1 = Guid.Parse("A2345678-90AB-CDEF-1234-567890ABCDEF");

        //Team
        private static readonly Guid Team_1 = Guid.Parse("23456789-0ABC-DEF1-2345-67890ABCDEFA");
        private static readonly Guid Team_2 = Guid.Parse("B3456789-0ABC-DEF1-2345-67890ABCDEFA");

        public DbSeeder(FootballMatchAppContext context)
        {
            _context = context;
        }

        public static void Seed(ModelBuilder modelBuilder)
        {
            Seeder_User(modelBuilder);
            Seeder_Role(modelBuilder);

            Seeder_Pitch(modelBuilder);
            Seeder_Team(modelBuilder);
            Seeder_TeamMember(modelBuilder);

        }

        // ADD MORE SEEDER

        private static void Seeder_TeamMember(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamMember>().HasData(
                new TeamMember
                {
                    TeamId = Team_1,
                    TeamMemberId = Player_1,
                    JoinDate = DateTime.Parse("1990-01-01T00:00:00Z"),
                    Position = "Forward",
                    UserId = UserId_1

                },
                new TeamMember
                {
                    TeamId = Team_1,
                    TeamMemberId = Player_2,
                    JoinDate = DateTime.Parse("1990-01-01T00:00:00Z"),
                    Position = "Midfielder",
                    UserId = UserId_2
                },
                new TeamMember
                {
                    TeamId = Team_2,
                    TeamMemberId = Player_3,
                    JoinDate = DateTime.Parse("1990-01-01T00:00:00Z"),
                    Position = "Defender",
                    UserId = UserId_3
                }
            );
        }
        private static void Seeder_Team(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasData(
                new Team
                {
                    TeamId = Team_1,
                    TeamName = "FPT FC",
                    CreatedById = UserId_1,

                },
                new Team
                {
                    TeamId = Team_2,
                    TeamName = "Thunder FC",
                    CreatedById = UserId_2
                }
            );
        }
        private static void Seeder_Pitch(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pitch>().HasData(
                new Pitch
                {
                    PitchId = Pitch_1,
                    Name = "SAN BONG DA CUC KY DANG CAP",
                    OwnerId = PitchOwnerId,
                    Location = "Đại học Bách Khoa – Đại học Quốc gia TP.HCM, quận 10, Thành phố Hồ Chí Minh, Việt Nam.",
                    PricePerHour = 300,
                    Latitude = 10.762622,
                    Longitude = 106.660172,
                    ContactNumber = "0123456789",
                    Status = Common.Enums.PitchStatus.AVAILABLE
                }
            );
        }
        
        private static void Seeder_Role(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    RoleId = AdminRole,
                    RoleName = "Admin"
                },
                new Role
                {
                    RoleId = PitchOwnerRole,
                    RoleName = "PitchOwner"
                },
                new Role
                {
                    RoleId = UserRole,
                    RoleName = "User"
                }
            );
        }
        private static void Seeder_User(ModelBuilder modelBuilder)
        {
            string fixedHashedPassword = "$2a$11$rTz6DZiEeBqhVrzF25CgTOBPf41jpn2Tg/nnIqnX8KS6uIerB/1dm";
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = AdminId,
                    Username = "admin",
                    PasswordHash = fixedHashedPassword,
                    Email = "admin@gmail.com",
                    RoleId = AdminRole,
                    PhoneNumber = "0123456789",
                    Address = "123 Admin Street, Admin City, Admin Country"


                },
                new User
                {
                    UserId = PitchOwnerId,
                    Username = "pitchowner",
                    PasswordHash = fixedHashedPassword,
                    Email = "pitchowner@gmail.com",
                    RoleId = PitchOwnerRole,
                    PhoneNumber = "0987654321",
                    Address = "456 Pitch Owner Street, Pitch Owner City, Pitch Owner Country"
                },
                new User
                {
                    UserId = UserId_1,
                    Username = "user1",
                    PasswordHash = fixedHashedPassword,
                    Email = "user-1@gmail.com",
                    RoleId = UserRole,
                    PhoneNumber = "1234567890",
                    Address = "789 User1 Street, User1 City, User1 Country"
                },
                new User
                {
                    UserId = UserId_2,
                    Username = "user2",
                    PasswordHash = fixedHashedPassword,
                    Email = "user-2@gmail.com",
                    RoleId = UserRole,
                    PhoneNumber = "1234567890",
                    Address = "101 User2 Street, User2 City, User2 Country"
                },
                new User
                {
                    UserId = UserId_3,
                    Username = "user3",
                    PasswordHash = fixedHashedPassword,
                    Email = "user-3@gmail.com",
                    RoleId = UserRole,
                    PhoneNumber = "1234567890",
                    Address = "102 User3 Street, User3 City, User3 Country"
                }

            );
        }
    }
}