using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[PrimaryKey("BrigadeId", "WorkshopId")]
[Table("brigade_fixation", Schema = "TOIR")]
public partial class BrigadeFixation
{
    [Key]
    [Column("brigade_id")]
    public int BrigadeId { get; set; }

    [Key]
    [Column("workshop_id")]
    public int WorkshopId { get; set; }

    [ForeignKey("BrigadeId")]
    [InverseProperty("BrigadeFixations")]
    public virtual Brigade Brigade { get; set; } = null!;
}
