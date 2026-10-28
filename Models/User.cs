using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("users", Schema = "new_TOIR")]
[Index("Email", Name = "users_email_key", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string Email { get; set; } = null!;

    [Column("password_hash")]
    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [Column("registered_at", TypeName = "timestamp without time zone")]
    public DateTime RegisteredAt { get; set; }

    [Column("phone")]
    [StringLength(32)]
    public string? Phone { get; set; }

    [Column("last_name")]
    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [Column("first_name")]
    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [Column("patronymic")]
    [StringLength(100)]
    public string? Patronymic { get; set; }

    [Column("role")]
    [StringLength(255)]
    public string Role { get; set; } = null!;

    [Column("status")]
    [StringLength(255)]
    public string Status { get; set; } = null!;

    [InverseProperty("LastEditer")]
    public virtual ICollection<EqHistoryStatusUpdate> EqHistoryStatusUpdates { get; set; } = new List<EqHistoryStatusUpdate>();

    [InverseProperty("User")]
    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();

    [InverseProperty("LastEditer")]
    public virtual ICollection<OrderHistoryStatusUpdate> OrderHistoryStatusUpdates { get; set; } = new List<OrderHistoryStatusUpdate>();

    [InverseProperty("User")]
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    [InverseProperty("Master")]
    public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();

    [InverseProperty("Chief")]
    public virtual ICollection<Workshop> WorkshopChiefs { get; set; } = new List<Workshop>();

    [InverseProperty("Master")]
    public virtual ICollection<Workshop> WorkshopMasters { get; set; } = new List<Workshop>();

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual ICollection<Brigade> Brigades { get; set; } = new List<Brigade>();
}
