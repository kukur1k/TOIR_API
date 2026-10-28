using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("operations", Schema = "new_TOIR")]
public partial class Operation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("entity_type")]
    [StringLength(255)]
    public string EntityType { get; set; } = null!;

    [Column("entity_id")]
    [StringLength(64)]
    public string EntityId { get; set; } = null!;

    [Column("action_type")]
    [StringLength(255)]
    public string ActionType { get; set; } = null!;

    [Column("date", TypeName = "timestamp without time zone")]
    public DateTime Date { get; set; }

    [Column("old_value")]
    public string? OldValue { get; set; }

    [Column("new_value")]
    public string? NewValue { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Operations")]
    public virtual User User { get; set; } = null!;
}
