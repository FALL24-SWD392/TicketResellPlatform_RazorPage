using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Business;

public partial class TicketResellContext : DbContext
{
    public TicketResellContext()
    {
    }

    public TicketResellContext(DbContextOptions<TicketResellContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<Chatbox> Chatboxes { get; set; }

    public virtual DbSet<ChatboxStatus> ChatboxStatuses { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Membership> Memberships { get; set; }

    public virtual DbSet<MembershipStatus> MembershipStatuses { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<ReportStatus> ReportStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subcription> Subcriptions { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketStatus> TicketStatuses { get; set; }

    public virtual DbSet<TicketType> TicketTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserStatus> UserStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Uid=sa;Pwd=12345;Database=ticket_resell;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chat_mes__3214EC07639E6FE2");

            entity.Property(e => e.CreateAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ChatBox).WithMany(p => p.ChatMessages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat_mess__ChatB__5EBF139D");

            entity.HasOne(d => d.Sender).WithMany(p => p.ChatMessages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chat_mess__Sende__5FB337D6");
        });

        modelBuilder.Entity<Chatbox>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chatboxe__3214EC071A2FC1C3");

            entity.Property(e => e.CreateAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Buyer).WithMany(p => p.ChatboxBuyers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chatboxes__Buyer__5812160E");

            entity.HasOne(d => d.Seller).WithMany(p => p.ChatboxSellers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chatboxes__Selle__59063A47");

            entity.HasOne(d => d.Status).WithMany(p => p.Chatboxes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chatboxes__Statu__5AEE82B9");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Chatboxes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__chatboxes__Ticke__59FA5E80");
        });

        modelBuilder.Entity<ChatboxStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__chatbox___3214EC07202A0641");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__feedback__3214EC07FEC2C8D9");

            entity.Property(e => e.CreateAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__feedbacks__Order__6754599E");
        });

        modelBuilder.Entity<Membership>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__membersh__3214EC07605AF28E");

            entity.Property(e => e.StartDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Owner).WithMany(p => p.Memberships)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__membershi__Owner__00200768");

            entity.HasOne(d => d.Status).WithMany(p => p.Memberships)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__membershi__Statu__02084FDA");

            entity.HasOne(d => d.Supscription).WithMany(p => p.Memberships)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__membershi__Supsc__01142BA1");
        });

        modelBuilder.Entity<MembershipStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__membersh__3214EC072D07D917");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders__3214EC07E754A9A2");

            entity.Property(e => e.CreateAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.ChatBox).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orders__ChatBoxI__6383C8BA");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__reports__3214EC07781CA119");

            entity.ToTable("reports", tb => tb.HasTrigger("trg_UpdateAt_OnUpdate_Report"));

            entity.Property(e => e.CreateAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Order).WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reports__OrderId__6E01572D");

            entity.HasOne(d => d.Sender).WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reports__SenderI__6EF57B66");

            entity.HasOne(d => d.Status).WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reports__StatusI__6FE99F9F");
        });

        modelBuilder.Entity<ReportStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__report_s__3214EC07948645BF");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3214EC07E760A5C8");
        });

        modelBuilder.Entity<Subcription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__subcript__3214EC07C7BFDFCE");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tickets__3214EC076BB99AAF");

            entity.ToTable("tickets", tb => tb.HasTrigger("trg_UpdateAt_OnUpdate_Ticket"));

            entity.Property(e => e.CreateAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdateAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Owner).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tickets__OwnerId__4F7CD00D");

            entity.HasOne(d => d.Status).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tickets__StatusI__5165187F");

            entity.HasOne(d => d.Type).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tickets__TypeId__5070F446");
        });

        modelBuilder.Entity<TicketStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ticket_s__3214EC078153EF15");
        });

        modelBuilder.Entity<TicketType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ticket_t__3214EC079E43584C");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3214EC07B5325AA0");

            entity.ToTable("users", tb => tb.HasTrigger("trg_UpdateAt_OnUpdate_User"));

            entity.Property(e => e.CreateAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rating).HasDefaultValue(0.0);
            entity.Property(e => e.Reputation).HasDefaultValue(100);
            entity.Property(e => e.UpdateAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__users__RoleId__44FF419A");

            entity.HasOne(d => d.Status).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__users__StatusId__45F365D3");
        });

        modelBuilder.Entity<UserStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_sta__3214EC070021F5ED");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
