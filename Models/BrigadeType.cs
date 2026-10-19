using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("brigade_type", Schema = "TOIR")]
public partial class BrigadeType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [InverseProperty("BrigadeType")]
    public virtual ICollection<Brigade> Brigades { get; set; } = new List<Brigade>();
}
