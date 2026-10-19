using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("operations", Schema = "TOIR")]
public partial class Operation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("entity_type_id")]
    public int? EntityTypeId { get; set; }

    [Column("entity_id")]
    [StringLength(255)]
    public string? EntityId { get; set; }

    [Column("action_type_id")]
    public int? ActionTypeId { get; set; }

    [Column("date", TypeName = "timestamp without time zone")]
    public DateTime? Date { get; set; }

    [Column("old_value")]
    [StringLength(255)]
    public string? OldValue { get; set; }

    [Column("new_value")]
    [StringLength(255)]
    public string? NewValue { get; set; }

    [ForeignKey("ActionTypeId")]
    [InverseProperty("Operations")]
    public virtual ActionType? ActionType { get; set; }

    [ForeignKey("EntityTypeId")]
    [InverseProperty("Operations")]
    public virtual EntityType? EntityType { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Operations")]
    public virtual User? User { get; set; }
}
