using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("shedule_to", Schema = "TOIR")]
public partial class SheduleTo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type_to_id")]
    public int? TypeToId { get; set; }

    [Column("eq_id")]
    public int? EqId { get; set; }

    [Column("date")]
    public DateOnly? Date { get; set; }

    [ForeignKey("EqId")]
    [InverseProperty("SheduleTos")]
    public virtual Equipment? Eq { get; set; }

    [ForeignKey("TypeToId")]
    [InverseProperty("SheduleTos")]
    public virtual TypeTo? TypeTo { get; set; }
}
