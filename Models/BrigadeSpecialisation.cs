using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("brigade_specialisation", Schema = "TOIR")]
public partial class BrigadeSpecialisation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [InverseProperty("Specialisation")]
    public virtual ICollection<Brigade> Brigades { get; set; } = new List<Brigade>();
}
