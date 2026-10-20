using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("equipment", Schema = "TOIR")]
public partial class Equipment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("invent_number")]
    [StringLength(255)]
    public string? InventNumber { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string? Name { get; set; }

    [Column("model")]
    [StringLength(255)]
    public string? Model { get; set; }

    [Column("serial_number")]
    [StringLength(255)]
    public string? SerialNumber { get; set; }

    [Column("date_start_exp")]
    public DateOnly? DateStartExp { get; set; }

    [Column("date_start_work")]
    public DateOnly? DateStartWork { get; set; }

    [InverseProperty("Eq")]
    public virtual ICollection<EqDatum> EqData { get; set; } = new List<EqDatum>();

    [InverseProperty("Eq")]
    public virtual ICollection<EqHistoryStatusUpdate> EqHistoryStatusUpdates { get; set; } = new List<EqHistoryStatusUpdate>();

    [InverseProperty("Equipment")]
    public virtual ICollection<EqState> EqStates { get; set; } = new List<EqState>();

    [InverseProperty("Eq")]
    public virtual ICollection<SheduleTo> SheduleTos { get; set; } = new List<SheduleTo>();

    [InverseProperty("Eq")]
    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}
