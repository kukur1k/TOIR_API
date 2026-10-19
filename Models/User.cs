using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("user", Schema = "TOIR")]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("phone")]
    [StringLength(50)]
    public string? Phone { get; set; }

    [Column("last_name")]
    [StringLength(255)]
    public string? LastName { get; set; }

    [Column("first_name")]
    [StringLength(255)]
    public string? FirstName { get; set; }

    [Column("patronymic")]
    [StringLength(255)]
    public string? Patronymic { get; set; }

    [Column("date_registration", TypeName = "timestamp without time zone")]
    public DateTime? DateRegistration { get; set; }

    [Column("role_id")]
    public int? RoleId { get; set; }

    [Column("status_id")]
    public int? StatusId { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role? Role { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Users")]
    public virtual UserStatus? Status { get; set; }

    [InverseProperty("Master")]
    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();

    [InverseProperty("Master")]
    public virtual ICollection<Workshop> Workshops { get; set; } = new List<Workshop>();

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual ICollection<Brigade> Brigades { get; set; } = new List<Brigade>();
}
