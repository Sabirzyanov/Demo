using System.ComponentModel.DataAnnotations.Schema;
using Demo.Core.Entites.Base;

namespace Warehouse.Core.DB.Entities;

[Table("Product")]
public class Product : Entity
{
    public string Name { get; set; }
    public string Articul { get; set; }
    public decimal MinimalCost { get; set; }
    public int ProductTypeId { get; set; }
    public byte[] Image { get; set; }
}