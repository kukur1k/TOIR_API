using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("order_history_status_update", Schema = "new_TOIR")]
public partial class OrderHistoryStatusUpdate
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("old_status")]
    [StringLength(255)]
    public string? OldStatus { get; set; }

    [Column("new_status")]
    [StringLength(255)]
    public string NewStatus { get; set; } = null!;

    [Column("date", TypeName = "timestamp without time zone")]
    public DateTime Date { get; set; }

    [Column("last_editer_id")]
    public int? LastEditerId { get; set; }

    [ForeignKey("LastEditerId")]
    [InverseProperty("OrderHistoryStatusUpdates")]
    public virtual User? LastEditer { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderHistoryStatusUpdates")]
    public virtual WorkOrder Order { get; set; } = null!;
}
