using FMA.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FMA.DAL.Context
{
    public class FootballMatchAppContext : DbContext
    {
        public FootballMatchAppContext(DbContextOptions<FootballMatchAppContext> options) : base(options) { }

        // --------- DbSets ---------
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<MatchPost> MatchPosts { get; set; }
        public DbSet<MatchRequest> MatchRequests { get; set; }
        public DbSet<Pitch> Pitches { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }

        // --------- Fluent API ---------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --------- Booking ---------
            modelBuilder.Entity<Booking>().HasKey(b => b.BookingId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Pitch)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PitchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.MatchPost)
                .WithMany()
                .HasForeignKey(b => b.MatchPostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.MatchRequest)
                .WithMany()
                .HasForeignKey(b => b.MatchRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .Property(b => b.Duration)
                .IsRequired();

            // --------- MatchPost ---------
            modelBuilder.Entity<MatchPost>().HasKey(mp => mp.PostId);

            modelBuilder.Entity<MatchPost>()
                .HasOne(mp => mp.PostBy)
                .WithMany()
                .HasForeignKey(mp => mp.PostById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MatchPost>()
                .HasOne(mp => mp.Pitch)
                .WithMany(p => p.MatchPosts)
                .HasForeignKey(mp => mp.PitchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MatchPost>()
                .HasMany(mp => mp.MatchRequests)
                .WithOne(mr => mr.MatchPost)
                .HasForeignKey(mr => mr.MatchPostId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------- MatchRequest ---------
            modelBuilder.Entity<MatchRequest>().HasKey(mr => mr.MatchRequestId);

            modelBuilder.Entity<MatchRequest>()
                .HasOne(mr => mr.RequestBy)
                .WithMany()
                .HasForeignKey(mr => mr.RequestById)
                .OnDelete(DeleteBehavior.Restrict);

            // --------- Pitch ---------
            modelBuilder.Entity<Pitch>().HasKey(p => p.PitchId);

            modelBuilder.Entity<Pitch>()
                .HasOne(p => p.User)
                .WithMany(u => u.Pitchs)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pitch>()
                .Property(p => p.PricePerHour)
                .HasPrecision(18, 2);

            // --------- Role ---------
            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);

            // --------- Team ---------
            modelBuilder.Entity<Team>().HasKey(t => t.TeamId);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.CreateBy)
                .WithMany(u => u.Teams)
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // --------- TeamMember ---------
            modelBuilder.Entity<TeamMember>().HasKey(tm => tm.TeamMemberId);

            modelBuilder.Entity<TeamMember>()
                .HasOne(tm => tm.Team)
                .WithMany(t => t.TeamMembers)
                .HasForeignKey(tm => tm.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------- User ---------
            modelBuilder.Entity<User>().HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------- UserToken ---------
            modelBuilder.Entity<UserToken>().HasKey(ut => ut.TokenId);

            modelBuilder.Entity<UserToken>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
