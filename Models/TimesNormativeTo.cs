using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("times_normative_to", Schema = "new_TOIR")]
[Index("TypeToId", "EqId", Name = "times_normative_to_type_to_id_eq_id_key", IsUnique = true)]
public partial class TimesNormativeTo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type_to_id")]
    public int TypeToId { get; set; }

    [Column("eq_id")]
    public int EqId { get; set; }

    [Column("duration")]
    public int Duration { get; set; }

    [ForeignKey("EqId")]
    [InverseProperty("TimesNormativeToes")]
    public virtual Equipment Eq { get; set; } = null!;

    [ForeignKey("TypeToId")]
    [InverseProperty("TimesNormativeToes")]
    public virtual TypeTo TypeTo { get; set; } = null!;
}
