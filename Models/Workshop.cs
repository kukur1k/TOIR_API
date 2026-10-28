using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("workshop", Schema = "new_TOIR")]
public partial class Workshop
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(200)]
    public string Title { get; set; } = null!;

    [Column("master_id")]
    public int? MasterId { get; set; }

    [Column("chief_id")]
    public int? ChiefId { get; set; }

    [ForeignKey("ChiefId")]
    [InverseProperty("WorkshopChiefs")]
    public virtual User? Chief { get; set; }

    [InverseProperty("Workshop")]
    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();

    [ForeignKey("MasterId")]
    [InverseProperty("WorkshopMasters")]
    public virtual User? Master { get; set; }

    [ForeignKey("WorkshopId")]
    [InverseProperty("Workshops")]
    public virtual ICollection<Brigade> Brigades { get; set; } = new List<Brigade>();
}
