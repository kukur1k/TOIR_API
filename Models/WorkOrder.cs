using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("work_order", Schema = "TOIR")]
public partial class WorkOrder
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type_id")]
    public int? TypeId { get; set; }

    [Column("priority_id")]
    public int? PriorityId { get; set; }

    [Column("status_id")]
    public int? StatusId { get; set; }

    [Column("date_create", TypeName = "timestamp without time zone")]
    public DateTime? DateCreate { get; set; }

    [Column("reason")]
    [StringLength(255)]
    public string? Reason { get; set; }

    [Column("master_id")]
    public int? MasterId { get; set; }

    [Column("eq_id")]
    public int? EqId { get; set; }

    [Column("plan_date_start")]
    public DateOnly? PlanDateStart { get; set; }

    [Column("plan_date_end")]
    public DateOnly? PlanDateEnd { get; set; }

    [Column("date_start")]
    public DateOnly? DateStart { get; set; }

    [Column("date_end")]
    public DateOnly? DateEnd { get; set; }

    [ForeignKey("EqId")]
    [InverseProperty("WorkOrders")]
    public virtual Equipment? Eq { get; set; }

    [ForeignKey("MasterId")]
    [InverseProperty("WorkOrders")]
    public virtual User? Master { get; set; }

    [InverseProperty("WorkOrder")]
    public virtual ICollection<MaterialsWorkOrder> MaterialsWorkOrders { get; set; } = new List<MaterialsWorkOrder>();

    [ForeignKey("PriorityId")]
    [InverseProperty("WorkOrders")]
    public virtual PriorityWorkOrder? Priority { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("WorkOrders")]
    public virtual StatusWorkOrder? Status { get; set; }

    [ForeignKey("TypeId")]
    [InverseProperty("WorkOrders")]
    public virtual TypeWorkOrder? Type { get; set; }
}
