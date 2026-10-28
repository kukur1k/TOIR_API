using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("work_order", Schema = "new_TOIR")]
public partial class WorkOrder
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type_wo")]
    [StringLength(255)]
    public string TypeWo { get; set; } = null!;

    [Column("priority")]
    [StringLength(255)]
    public string Priority { get; set; } = null!;

    [Column("status_wo")]
    [StringLength(255)]
    public string StatusWo { get; set; } = null!;

    [Column("date_create", TypeName = "timestamp without time zone")]
    public DateTime DateCreate { get; set; }

    [Column("reason")]
    public string? Reason { get; set; }

    [Column("master_id")]
    public int? MasterId { get; set; }

    [Column("eq_id")]
    public int EqId { get; set; }

    [Column("plan_date_start")]
    public DateOnly? PlanDateStart { get; set; }

    [Column("plan_date_end")]
    public DateOnly? PlanDateEnd { get; set; }

    [Column("date_start", TypeName = "timestamp without time zone")]
    public DateTime? DateStart { get; set; }

    [Column("date_end", TypeName = "timestamp without time zone")]
    public DateTime? DateEnd { get; set; }

    [ForeignKey("EqId")]
    [InverseProperty("WorkOrders")]
    public virtual Equipment Eq { get; set; } = null!;

    [ForeignKey("MasterId")]
    [InverseProperty("WorkOrders")]
    public virtual User? Master { get; set; }

    [InverseProperty("WorkOrder")]
    public virtual ICollection<MaterialsWorkOrder> MaterialsWorkOrders { get; set; } = new List<MaterialsWorkOrder>();

    [InverseProperty("Order")]
    public virtual ICollection<OrderHistoryStatusUpdate> OrderHistoryStatusUpdates { get; set; } = new List<OrderHistoryStatusUpdate>();
}
