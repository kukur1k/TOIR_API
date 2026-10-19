using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("eq_data", Schema = "TOIR")]
public partial class EqDatum
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("eq_id")]
    public int? EqId { get; set; }

    [Column("manufacture")]
    [StringLength(255)]
    public string? Manufacture { get; set; }

    [Column("warrancy_time")]
    [StringLength(255)]
    public string? WarrancyTime { get; set; }

    [Column("date_create_record", TypeName = "timestamp without time zone")]
    public DateTime? DateCreateRecord { get; set; }

    [Column("date_last_edit_record", TypeName = "timestamp without time zone")]
    public DateTime? DateLastEditRecord { get; set; }

    [Column("last_to_date", TypeName = "timestamp without time zone")]
    public DateTime? LastToDate { get; set; }

    [Column("last_editer_id")]
    public int? LastEditerId { get; set; }

    [ForeignKey("EqId")]
    [InverseProperty("EqData")]
    public virtual Equipment? Eq { get; set; }
}
