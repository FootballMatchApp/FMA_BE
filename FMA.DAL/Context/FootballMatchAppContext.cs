using System;
using System.Collections.Generic;
using FMA.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FMA.DAL.Context;

public partial class FootballMatchAppContext : DbContext
{
    public FootballMatchAppContext()
    {
    }

    public FootballMatchAppContext(DbContextOptions<FootballMatchAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<MatchPost> MatchPosts { get; set; }

    public virtual DbSet<MatchResult> MatchResults { get; set; }

    public virtual DbSet<Pitch> Pitches { get; set; }

    public virtual DbSet<PitchOwner> PitchOwners { get; set; }

    public virtual DbSet<PlayerProfile> PlayerProfiles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamMember> TeamMembers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admins__719FE488A6E77124");

            entity.Property(e => e.FullName).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Admins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Admins__UserId__440B1D61");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951AEDFF8E0B93");

            entity.Property(e => e.BookingTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Pitch).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PitchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__PitchI__534D60F1");

            entity.HasOne(d => d.Player).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK__Bookings__Player__5441852A");

            entity.HasOne(d => d.TeamBooking).WithMany(p => p.BookingTeamBookings)
                .HasForeignKey(d => d.TeamBookingId)
                .HasConstraintName("FK__Bookings__TeamBo__5535A963");

            entity.HasOne(d => d.TeamReceiving).WithMany(p => p.BookingTeamReceivings)
                .HasForeignKey(d => d.TeamReceivingId)
                .HasConstraintName("FK__Bookings__TeamRe__5629CD9C");
        });

        modelBuilder.Entity<MatchPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__MatchPos__AA12601827AB8CC1");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.LookingFor).HasMaxLength(50);
            entity.Property(e => e.MatchTime).HasColumnType("datetime");
            entity.Property(e => e.PostStatus)
                .HasMaxLength(30)
                .HasDefaultValue("Open");

            entity.HasOne(d => d.Pitch).WithMany(p => p.MatchPosts)
                .HasForeignKey(d => d.PitchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MatchPost__Pitch__5EBF139D");

            entity.HasOne(d => d.PostedByPlayer).WithMany(p => p.MatchPosts)
                .HasForeignKey(d => d.PostedByPlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MatchPost__Poste__5CD6CB2B");

            entity.HasOne(d => d.ReceivingTeam).WithMany(p => p.MatchPostReceivingTeams)
                .HasForeignKey(d => d.ReceivingTeamId)
                .HasConstraintName("FK__MatchPost__Recei__5DCAEF64");

            entity.HasOne(d => d.Team).WithMany(p => p.MatchPostTeams)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MatchPost__TeamI__5BE2A6F2");
        });

        modelBuilder.Entity<MatchResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__MatchRes__97690208D7E599AA");

            entity.Property(e => e.MatchDate).HasColumnType("datetime");

            entity.HasOne(d => d.Team1).WithMany(p => p.MatchResultTeam1s)
                .HasForeignKey(d => d.Team1Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MatchResu__Team1__619B8048");

            entity.HasOne(d => d.Team2).WithMany(p => p.MatchResultTeam2s)
                .HasForeignKey(d => d.Team2Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MatchResu__Team2__628FA481");
        });

        modelBuilder.Entity<Pitch>(entity =>
        {
            entity.HasKey(e => e.PitchId).HasName("PK__Pitches__8B89B6E67469F9E3");

            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PricePerHour).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Owner).WithMany(p => p.Pitches)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pitches__OwnerId__46E78A0C");
        });

        modelBuilder.Entity<PitchOwner>(entity =>
        {
            entity.HasKey(e => e.OwnerId).HasName("PK__PitchOwn__819385B8900AA6CC");

            entity.Property(e => e.ContactNumber).HasMaxLength(20);
            entity.Property(e => e.OwnerName).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.PitchOwners)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PitchOwne__UserI__412EB0B6");
        });

        modelBuilder.Entity<PlayerProfile>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__PlayerPr__4A4E74C8CC695828");

            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Position).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.PlayerProfiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlayerPro__UserI__3E52440B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A024B9493");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B616067514AE4").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("PK__Teams__123AE799CE325907");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.TeamName).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Teams)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Teams__CreatedBy__49C3F6B7");
        });

        modelBuilder.Entity<TeamMember>(entity =>
        {
            entity.HasKey(e => new { e.TeamId, e.PlayerId }).HasName("PK__TeamMemb__869E00D535BDE6BE");

            entity.Property(e => e.JoinDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Player).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeamMembe__Playe__4E88ABD4");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamMembers)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TeamMembe__TeamI__4D94879B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CBA10DDC5");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E472CED653").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
