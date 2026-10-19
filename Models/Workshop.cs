using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("workshop", Schema = "TOIR")]
public partial class Workshop
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [Column("master_id")]
    public int? MasterId { get; set; }

    [ForeignKey("MasterId")]
    [InverseProperty("Workshops")]
    public virtual User? Master { get; set; }
}
