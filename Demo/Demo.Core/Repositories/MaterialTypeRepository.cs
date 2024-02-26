using Demo.Core.Context;
using Demo.Core.Entites;
using Demo.Core.Repositories.Base;

namespace Demo.Core.Repositories;

public class MaterialTypeRepository : Repository<MaterialType>
{
    public MaterialTypeRepository(DBContext context) : base(context)
    {
    }
}