using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TOIR_API.Models;

namespace TOIR_API.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brigade> Brigades { get; set; }

    public virtual DbSet<BrigadeShedule> BrigadeShedules { get; set; }

    public virtual DbSet<EqHistoryStatusUpdate> EqHistoryStatusUpdates { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialHistory> MaterialHistories { get; set; }

    public virtual DbSet<MaterialsWorkOrder> MaterialsWorkOrders { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<OrderHistoryStatusUpdate> OrderHistoryStatusUpdates { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<SheduleTo> SheduleTos { get; set; }

    public virtual DbSet<TimesNormativeTo> TimesNormativeToes { get; set; }

    public virtual DbSet<TypeTo> TypeTos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WorkOrder> WorkOrders { get; set; }

    public virtual DbSet<Workshop> Workshops { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.HasDefaultSchema("new_TOIR");
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Brigade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("brigade_pkey");

            entity.HasMany(d => d.Users).WithMany(p => p.Brigades)
                .UsingEntity<Dictionary<string, object>>(
                    "BrigadeRoster",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("brigade_roster_user_id_fkey"),
                    l => l.HasOne<Brigade>().WithMany()
                        .HasForeignKey("BrigadeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("brigade_roster_brigade_id_fkey"),
                    j =>
                    {
                        j.HasKey("BrigadeId", "UserId").HasName("brigade_roster_pkey");
                        j.ToTable("brigade_roster", "new_TOIR");
                        j.IndexerProperty<int>("BrigadeId").HasColumnName("brigade_id");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                    });

            entity.HasMany(d => d.Workshops).WithMany(p => p.Brigades)
                .UsingEntity<Dictionary<string, object>>(
                    "BrigadeFixation",
                    r => r.HasOne<Workshop>().WithMany()
                        .HasForeignKey("WorkshopId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("brigade_fixation_workshop_id_fkey"),
                    l => l.HasOne<Brigade>().WithMany()
                        .HasForeignKey("BrigadeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("brigade_fixation_brigade_id_fkey"),
                    j =>
                    {
                        j.HasKey("BrigadeId", "WorkshopId").HasName("brigade_fixation_pkey");
                        j.ToTable("brigade_fixation", "new_TOIR");
                        j.IndexerProperty<int>("BrigadeId").HasColumnName("brigade_id");
                        j.IndexerProperty<int>("WorkshopId").HasColumnName("workshop_id");
                    });
        });

        modelBuilder.Entity<BrigadeShedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("brigade_shedule_pkey");

            entity.HasOne(d => d.Brigade).WithMany(p => p.BrigadeShedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("brigade_shedule_brigade_id_fkey");
        });

        modelBuilder.Entity<EqHistoryStatusUpdate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("eq_history_status_update_pkey");

            entity.Property(e => e.Date).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Eq).WithMany(p => p.EqHistoryStatusUpdates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eq_history_status_update_eq_id_fkey");

            entity.HasOne(d => d.LastEditer).WithMany(p => p.EqHistoryStatusUpdates).HasConstraintName("eq_history_status_update_last_editer_id_fkey");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("equipment_pkey");

            entity.Property(e => e.DateCreateRecord).HasDefaultValueSql("CURRENT_DATE");
            entity.Property(e => e.EqStatus).HasDefaultValueSql("'active'::character varying");

            entity.HasOne(d => d.Workshop).WithMany(p => p.Equipment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("equipment_workshop_id_fkey");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("material_pkey");
        });

        modelBuilder.Entity<MaterialHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("material_history_pkey");

            entity.Property(e => e.Date).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Material).WithMany(p => p.MaterialHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("material_history_material_id_fkey");
        });

        modelBuilder.Entity<MaterialsWorkOrder>(entity =>
        {
            entity.HasKey(e => new { e.WorkOrderId, e.MaterialId }).HasName("materials_work_order_pkey");

            entity.HasOne(d => d.Material).WithMany(p => p.MaterialsWorkOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("materials_work_order_material_id_fkey");

            entity.HasOne(d => d.WorkOrder).WithMany(p => p.MaterialsWorkOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("materials_work_order_work_order_id_fkey");
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("operations_pkey");

            entity.Property(e => e.Date).HasDefaultValueSql("now()");
            entity.Property(e => e.EntityType).HasDefaultValueSql("'user'::character varying");

            entity.HasOne(d => d.User).WithMany(p => p.Operations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("operations_user_id_fkey");
        });

        modelBuilder.Entity<OrderHistoryStatusUpdate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_history_status_update_pkey");

            entity.Property(e => e.Date).HasDefaultValueSql("now()");

            entity.HasOne(d => d.LastEditer).WithMany(p => p.OrderHistoryStatusUpdates).HasConstraintName("order_history_status_update_last_editer_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderHistoryStatusUpdates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_history_status_update_order_id_fkey");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("refresh_tokens_pkey");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.IsRevoked).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens).HasConstraintName("refresh_tokens_user_id_fkey");
        });

        modelBuilder.Entity<SheduleTo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shedule_to_pkey");

            entity.HasOne(d => d.Brigade).WithMany(p => p.SheduleTos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shedule_to_brigade_id_fkey");

            entity.HasOne(d => d.To).WithMany(p => p.SheduleTos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shedule_to_to_id_fkey");
        });

        modelBuilder.Entity<TimesNormativeTo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("times_normative_to_pkey");

            entity.HasOne(d => d.Eq).WithMany(p => p.TimesNormativeToes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("times_normative_to_eq_id_fkey");

            entity.HasOne(d => d.TypeTo).WithMany(p => p.TimesNormativeToes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("times_normative_to_type_to_id_fkey");
        });

        modelBuilder.Entity<TypeTo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("type_to_pkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.Property(e => e.RegisteredAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Role).HasDefaultValueSql("'employee'::character varying");
            entity.Property(e => e.Status).HasDefaultValueSql("'active'::character varying");
        });

        modelBuilder.Entity<WorkOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("work_order_pkey");

            entity.Property(e => e.DateCreate).HasDefaultValueSql("now()");
            entity.Property(e => e.Priority).HasDefaultValueSql("'normal'::character varying");
            entity.Property(e => e.StatusWo).HasDefaultValueSql("'new'::character varying");
            entity.Property(e => e.TypeWo).HasDefaultValueSql("'repair'::character varying");

            entity.HasOne(d => d.Eq).WithMany(p => p.WorkOrders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("work_order_eq_id_fkey");

            entity.HasOne(d => d.Master).WithMany(p => p.WorkOrders).HasConstraintName("work_order_master_id_fkey");
        });

        modelBuilder.Entity<Workshop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workshop_pkey");

            entity.HasOne(d => d.Chief).WithMany(p => p.WorkshopChiefs).HasConstraintName("workshop_chief_id_fkey");

            entity.HasOne(d => d.Master).WithMany(p => p.WorkshopMasters).HasConstraintName("workshop_master_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
