using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("equipment", Schema = "new_TOIR")]
[Index("InventNumber", Name = "equipment_invent_number_key", IsUnique = true)]
public partial class Equipment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("workshop_id")]
    public int WorkshopId { get; set; }

    [Column("eq_status")]
    [StringLength(255)]
    public string EqStatus { get; set; } = null!;

    [Column("invent_number")]
    [StringLength(64)]
    public string InventNumber { get; set; } = null!;

    [Column("name")]
    [StringLength(200)]
    public string Name { get; set; } = null!;

    [Column("model")]
    [StringLength(200)]
    public string? Model { get; set; }

    [Column("serial_number")]
    [StringLength(128)]
    public string? SerialNumber { get; set; }

    [Column("warrancy_time")]
    public DateOnly? WarrancyTime { get; set; }

    [Column("manufacture")]
    [StringLength(200)]
    public string? Manufacture { get; set; }

    [Column("date_create_record")]
    public DateOnly DateCreateRecord { get; set; }

    [Column("date_start_exp")]
    public DateOnly? DateStartExp { get; set; }

    [Column("date_start_work")]
    public DateOnly? DateStartWork { get; set; }

    [Column("last_to_date", TypeName = "timestamp without time zone")]
    public DateTime? LastToDate { get; set; }

    [InverseProperty("Eq")]
    public virtual ICollection<EqHistoryStatusUpdate> EqHistoryStatusUpdates { get; set; } = new List<EqHistoryStatusUpdate>();

    [InverseProperty("Eq")]
    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();

    [ForeignKey("WorkshopId")]
    [InverseProperty("Equipment")]
    public virtual Workshop Workshop { get; set; } = null!;
}
