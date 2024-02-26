using System.ComponentModel.DataAnnotations.Schema;
using Demo.Core.Entites.Base;

namespace Demo.Core.Entites;

[Table("Material")]
public class Material : Entity
{
    public string Name { get; set; }
    public int MaterialTypeId { get; set; }
}