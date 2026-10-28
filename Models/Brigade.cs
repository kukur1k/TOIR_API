using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("brigade", Schema = "new_TOIR")]
public partial class Brigade
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(200)]
    public string Title { get; set; } = null!;

    [Column("brigade_specialisation")]
    [StringLength(255)]
    public string? BrigadeSpecialisation { get; set; }

    [Column("brigade_type")]
    [StringLength(255)]
    public string? BrigadeType { get; set; }

    [InverseProperty("Brigade")]
    public virtual ICollection<BrigadeShedule> BrigadeShedules { get; set; } = new List<BrigadeShedule>();

    [InverseProperty("Brigade")]
    public virtual ICollection<SheduleTo> SheduleTos { get; set; } = new List<SheduleTo>();

    [ForeignKey("BrigadeId")]
    [InverseProperty("Brigades")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();

    [ForeignKey("BrigadeId")]
    [InverseProperty("Brigades")]
    public virtual ICollection<Workshop> Workshops { get; set; } = new List<Workshop>();
}
