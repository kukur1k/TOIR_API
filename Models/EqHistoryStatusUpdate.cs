using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("eq_history_status_update", Schema = "new_TOIR")]
public partial class EqHistoryStatusUpdate
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("eq_id")]
    public int EqId { get; set; }

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

    [ForeignKey("EqId")]
    [InverseProperty("EqHistoryStatusUpdates")]
    public virtual Equipment Eq { get; set; } = null!;

    [ForeignKey("LastEditerId")]
    [InverseProperty("EqHistoryStatusUpdates")]
    public virtual User? LastEditer { get; set; }
}
