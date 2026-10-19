using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("material_history", Schema = "TOIR")]
public partial class MaterialHistory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("material_id")]
    public int? MaterialId { get; set; }

    [Column("date", TypeName = "timestamp without time zone")]
    public DateTime? Date { get; set; }

    [Column("cost")]
    public decimal? Cost { get; set; }

    [ForeignKey("MaterialId")]
    [InverseProperty("MaterialHistories")]
    public virtual Material? Material { get; set; }
}
