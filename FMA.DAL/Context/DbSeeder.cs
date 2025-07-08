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
        private static readonly int AdminRole = 1;
        private static readonly int PitchOwnerRole = 2;
        private static readonly int UserRole = 3;

        // User
        private static readonly int AdminId = 1;
        private static readonly int PitchOwnerId = 2;
        private static readonly int UserId_1 = 3;
        private static readonly int UserId_2 = 4;
        private static readonly int UserId_3 = 5;

        //Player
        private static readonly int Player_1 = 1;
        private static readonly int Player_2 = 2;
        private static readonly int Player_3 = 3;

        //PitchOwner
        private static readonly int PitchOwner_1 = 2;

        //Pitch
        private static readonly int Pitch_1 = 1;

        //Team
        private static readonly int Team_1 = 1;
        private static readonly int Team_2 = 2;

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
                    PlayerId = Player_1,
                    JoinDate = DateTime.Parse("1990-01-01T00:00:00Z")
                },
                new TeamMember
                {
                    TeamId = Team_1,
                    PlayerId = Player_2,
                    JoinDate = DateTime.Parse("1990-01-01T00:00:00Z")
                },
                new TeamMember
                {
                    TeamId = Team_2,
                    PlayerId = Player_3,
                    JoinDate = DateTime.Parse("1990-01-01T00:00:00Z")
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
                    CreatedBy = UserId_1,
                    Description = "FPT University Football Club"
                },
                new Team
                {
                    TeamId = Team_2,
                    TeamName = "Thunder FC",
                    CreatedBy = UserId_2,
                    Description = "Thunder Football Club"
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
                    OwnerId = PitchOwner_1,
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