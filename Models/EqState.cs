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
}
