using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TOIR_API.Models;

namespace TOIR_API.Data;

public partial class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActionType> ActionTypes { get; set; }

    public virtual DbSet<Brigade> Brigades { get; set; }

    public virtual DbSet<BrigadeFixation> BrigadeFixations { get; set; }

    public virtual DbSet<BrigadeShedule> BrigadeShedules { get; set; }

    public virtual DbSet<BrigadeSpecialisation> BrigadeSpecialisations { get; set; }

    public virtual DbSet<BrigadeType> BrigadeTypes { get; set; }

    public virtual DbSet<EntityType> EntityTypes { get; set; }

    public virtual DbSet<EqDatum> EqData { get; set; }

    public virtual DbSet<EqHistoryStatusUpdate> EqHistoryStatusUpdates { get; set; }

    public virtual DbSet<EqState> EqStates { get; set; }

    public virtual DbSet<EqStatus> EqStatuses { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialHistory> MaterialHistories { get; set; }

    public virtual DbSet<MaterialsWorkOrder> MaterialsWorkOrders { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<PriorityWorkOrder> PriorityWorkOrders { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SheduleTo> SheduleTos { get; set; }

    public virtual DbSet<StatusWorkOrder> StatusWorkOrders { get; set; }

    public virtual DbSet<TypeTo> TypeTos { get; set; }

    public virtual DbSet<TypeWorkOrder> TypeWorkOrders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserStatus> UserStatuses { get; set; }

    public virtual DbSet<WorkOrder> WorkOrders { get; set; }

    public virtual DbSet<Workshop> Workshops { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ActionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("action_type_pkey");
        });

        modelBuilder.Entity<Brigade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("brigade_pkey");

            entity.HasOne(d => d.BrigadeType).WithMany(p => p.Brigades).HasConstraintName("brigade_brigade_type_id_fkey");

            entity.HasOne(d => d.Specialisation).WithMany(p => p.Brigades).HasConstraintName("brigade_specialisation_id_fkey");

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
                        j.ToTable("brigade_roster", "TOIR");
                        j.IndexerProperty<int>("BrigadeId").HasColumnName("brigade_id");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                    });
        });

        modelBuilder.Entity<BrigadeFixation>(entity =>
        {
            entity.HasKey(e => new { e.BrigadeId, e.WorkshopId }).HasName("brigade_fixation_pkey");

            entity.HasOne(d => d.Brigade).WithMany(p => p.BrigadeFixations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("brigade_fixation_brigade_id_fkey");
        });

        modelBuilder.Entity<BrigadeShedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("brigade_shedule_pkey");

            entity.HasOne(d => d.Brigade).WithMany(p => p.BrigadeShedules).HasConstraintName("brigade_shedule_brigade_id_fkey");
        });

        modelBuilder.Entity<BrigadeSpecialisation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("brigade_specialisation_pkey");
        });

        modelBuilder.Entity<BrigadeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("brigade_type_pkey");
        });

        modelBuilder.Entity<EntityType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("entity_type_pkey");
        });

        modelBuilder.Entity<EqDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("eq_data_pkey");

            entity.HasOne(d => d.Eq).WithMany(p => p.EqData).HasConstraintName("eq_data_eq_id_fkey");
        });

        modelBuilder.Entity<EqHistoryStatusUpdate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("eq_history_status_update_pkey");

            entity.HasOne(d => d.Eq).WithMany(p => p.EqHistoryStatusUpdates).HasConstraintName("eq_history_status_update_eq_id_fkey");
        });

        modelBuilder.Entity<EqState>(entity =>
        {
            entity.HasKey(e => new { e.EquipmentId, e.EqStatusId, e.WorkshopId }).HasName("eq_state_pkey");

            entity.HasOne(d => d.EqStatus).WithMany(p => p.EqStates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eq_state_eq_status_id_fkey");

            entity.HasOne(d => d.Equipment).WithMany(p => p.EqStates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eq_state_equipment_id_fkey");

            entity.HasOne(d => d.Workshop).WithMany(p => p.EqStates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eq_state_workshop_id_fkey");
        });

        modelBuilder.Entity<EqStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("eq_status_pkey");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("equipment_pkey");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("material_pkey");
        });

        modelBuilder.Entity<MaterialHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("material_history_pkey");

            entity.HasOne(d => d.Material).WithMany(p => p.MaterialHistories).HasConstraintName("material_history_material_id_fkey");
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

            entity.HasOne(d => d.ActionType).WithMany(p => p.Operations).HasConstraintName("operations_action_type_id_fkey");

            entity.HasOne(d => d.EntityType).WithMany(p => p.Operations).HasConstraintName("operations_entity_type_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Operations).HasConstraintName("operations_user_id_fkey");
        });

        modelBuilder.Entity<PriorityWorkOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("priority_work_order_pkey");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("refresh_tokens_pkey");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.IsRevoked).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens).HasConstraintName("refresh_tokens_user_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");
        });

        modelBuilder.Entity<SheduleTo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shedule_to_pkey");

            entity.HasOne(d => d.Eq).WithMany(p => p.SheduleTos).HasConstraintName("shedule_to_eq_id_fkey");

            entity.HasOne(d => d.TypeTo).WithMany(p => p.SheduleTos).HasConstraintName("shedule_to_type_to_id_fkey");
        });

        modelBuilder.Entity<StatusWorkOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("status_work_order_pkey");
        });

        modelBuilder.Entity<TypeTo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("type_to_pkey");
        });

        modelBuilder.Entity<TypeWorkOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("type_work_order_pkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Users).HasConstraintName("user_role_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Users).HasConstraintName("user_status_id_fkey");
        });

        modelBuilder.Entity<UserStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_status_pkey");
        });

        modelBuilder.Entity<WorkOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("work_order_pkey");

            entity.HasOne(d => d.Eq).WithMany(p => p.WorkOrders).HasConstraintName("work_order_eq_id_fkey");

            entity.HasOne(d => d.Master).WithMany(p => p.WorkOrders).HasConstraintName("work_order_master_id_fkey");

            entity.HasOne(d => d.Priority).WithMany(p => p.WorkOrders).HasConstraintName("work_order_priority_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.WorkOrders).HasConstraintName("work_order_status_id_fkey");

            entity.HasOne(d => d.Type).WithMany(p => p.WorkOrders).HasConstraintName("work_order_type_id_fkey");
        });

        modelBuilder.Entity<Workshop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workshop_pkey");

            entity.HasOne(d => d.Master).WithMany(p => p.Workshops).HasConstraintName("workshop_master_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
