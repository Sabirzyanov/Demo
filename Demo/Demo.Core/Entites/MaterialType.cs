using System.ComponentModel.DataAnnotations.Schema;
using Demo.Core.Entites.Base;

namespace Demo.Core.Entites;

[Table("MaterialType")]
public class MaterialType : Entity
{
    public string Name { get; set; }
}