using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[PrimaryKey("EquipmentId", "EqStatusId", "WorkshopId")]
[Table("eq_state", Schema = "TOIR")]
public partial class EqState
{
    [Key]
    [Column("equipment_id")]
    public int EquipmentId { get; set; }

    [Key]
    [Column("eq_status_id")]
    public int EqStatusId { get; set; }

    [Key]
    [Column("workshop_id")]
    public int WorkshopId { get; set; }

    [ForeignKey("EqStatusId")]
    [InverseProperty("EqStates")]
    public virtual EqStatus EqStatus { get; set; } = null!;

    [ForeignKey("EquipmentId")]
    [InverseProperty("EqStates")]
    public virtual Equipment Equipment { get; set; } = null!;

    [ForeignKey("WorkshopId")]
    [InverseProperty("EqStates")]
    public virtual Workshop Workshop { get; set; } = null!;
}
