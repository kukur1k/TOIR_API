using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("type_to", Schema = "new_TOIR")]
public partial class TypeTo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(200)]
    public string Title { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("interval")]
    [StringLength(64)]
    public string? Interval { get; set; }

    [InverseProperty("To")]
    public virtual ICollection<SheduleTo> SheduleTos { get; set; } = new List<SheduleTo>();

    [InverseProperty("TypeTo")]
    public virtual ICollection<TimesNormativeTo> TimesNormativeToes { get; set; } = new List<TimesNormativeTo>();
}
