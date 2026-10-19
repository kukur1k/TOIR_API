using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TOIR_API.Models;

[PrimaryKey("WorkOrderId", "MaterialId")]
[Table("materials_work_order", Schema = "TOIR")]
public partial class MaterialsWorkOrder
{
    [Key]
    [Column("work_order_id")]
    public int WorkOrderId { get; set; }

    [Key]
    [Column("material_id")]
    public int MaterialId { get; set; }

    [Column("quantity")]
    public decimal? Quantity { get; set; }

    [ForeignKey("MaterialId")]
    [InverseProperty("MaterialsWorkOrders")]
    public virtual Material Material { get; set; } = null!;

    [ForeignKey("WorkOrderId")]
    [InverseProperty("MaterialsWorkOrders")]
    public virtual WorkOrder WorkOrder { get; set; } = null!;
}
