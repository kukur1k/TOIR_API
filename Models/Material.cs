using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[Table("material", Schema = "TOIR")]
public partial class Material
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string? Name { get; set; }

    [Column("unit_name")]
    [StringLength(50)]
    public string? UnitName { get; set; }

    [Column("quantity_in_stock")]
    public decimal? QuantityInStock { get; set; }

    [Column("min_quantity")]
    public decimal? MinQuantity { get; set; }

    [Column("cost")]
    public decimal? Cost { get; set; }

    [Column("shelf_in_stock")]
    [StringLength(255)]
    public string? ShelfInStock { get; set; }

    [Column("cell_in_stock")]
    [StringLength(255)]
    public string? CellInStock { get; set; }

    [InverseProperty("Material")]
    public virtual ICollection<MaterialHistory> MaterialHistories { get; set; } = new List<MaterialHistory>();

    [InverseProperty("Material")]
    public virtual ICollection<MaterialsWorkOrder> MaterialsWorkOrders { get; set; } = new List<MaterialsWorkOrder>();
}
