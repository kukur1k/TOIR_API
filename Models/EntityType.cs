using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("entity_type", Schema = "TOIR")]
public partial class EntityType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [InverseProperty("EntityType")]
    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();
}
