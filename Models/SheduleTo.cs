using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("shedule_to", Schema = "new_TOIR")]
public partial class SheduleTo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("to_id")]
    public int ToId { get; set; }

    [Column("brigade_id")]
    public int BrigadeId { get; set; }

    [Column("eq_id")]
    public int EqId { get; set; }

    [Column("status_to")]
    [StringLength(255)]
    public string StatusTo { get; set; } = null!;

    [ForeignKey("BrigadeId")]
    [InverseProperty("SheduleTos")]
    public virtual Brigade Brigade { get; set; } = null!;

    [ForeignKey("EqId")]
    [InverseProperty("SheduleTos")]
    public virtual Equipment Eq { get; set; } = null!;

    [ForeignKey("ToId")]
    [InverseProperty("SheduleTos")]
    public virtual TypeTo To { get; set; } = null!;
}
