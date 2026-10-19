using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("brigade", Schema = "TOIR")]
public partial class Brigade
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [Column("specialisation_id")]
    public int? SpecialisationId { get; set; }

    [Column("brigade_type_id")]
    public int? BrigadeTypeId { get; set; }

    [InverseProperty("Brigade")]
    public virtual ICollection<BrigadeFixation> BrigadeFixations { get; set; } = new List<BrigadeFixation>();

    [InverseProperty("Brigade")]
    public virtual ICollection<BrigadeShedule> BrigadeShedules { get; set; } = new List<BrigadeShedule>();

    [ForeignKey("BrigadeTypeId")]
    [InverseProperty("Brigades")]
    public virtual BrigadeType? BrigadeType { get; set; }

    [ForeignKey("SpecialisationId")]
    [InverseProperty("Brigades")]
    public virtual BrigadeSpecialisation? Specialisation { get; set; }

    [ForeignKey("BrigadeId")]
    [InverseProperty("Brigades")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
