using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("material", Schema = "new_TOIR")]
public partial class Material
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(200)]
    public string Name { get; set; } = null!;

    [Column("unit_name")]
    [StringLength(32)]
    public string UnitName { get; set; } = null!;

    [Column("quantity_in_stock")]
    [Precision(14, 3)]
    public decimal QuantityInStock { get; set; }

    [Column("min_quantity")]
    [Precision(14, 3)]
    public decimal MinQuantity { get; set; }

    [Column("cost")]
    [Precision(14, 2)]
    public decimal Cost { get; set; }

    [Column("shelf_in_stock")]
    [StringLength(64)]
    public string? ShelfInStock { get; set; }

    [Column("cell_in_stock")]
    [StringLength(64)]
    public string? CellInStock { get; set; }

    [InverseProperty("Material")]
    public virtual ICollection<MaterialHistory> MaterialHistories { get; set; } = new List<MaterialHistory>();

    [InverseProperty("Material")]
    public virtual ICollection<MaterialsWorkOrder> MaterialsWorkOrders { get; set; } = new List<MaterialsWorkOrder>();
}
