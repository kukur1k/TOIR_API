using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("type_to", Schema = "TOIR")]
public partial class TypeTo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("interval")]
    [StringLength(255)]
    public string? Interval { get; set; }

    [Column("duration")]
    [StringLength(255)]
    public string? Duration { get; set; }

    [Column("need_stop")]
    public bool? NeedStop { get; set; }

    [InverseProperty("TypeTo")]
    public virtual ICollection<SheduleTo> SheduleTos { get; set; } = new List<SheduleTo>();
}
