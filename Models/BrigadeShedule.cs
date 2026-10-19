using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("brigade_shedule", Schema = "TOIR")]
public partial class BrigadeShedule
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("brigade_id")]
    public int? BrigadeId { get; set; }

    [Column("start_date")]
    public DateOnly? StartDate { get; set; }

    [Column("end_date")]
    public DateOnly? EndDate { get; set; }

    [ForeignKey("BrigadeId")]
    [InverseProperty("BrigadeShedules")]
    public virtual Brigade? Brigade { get; set; }
}
