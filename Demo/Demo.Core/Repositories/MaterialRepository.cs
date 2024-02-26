using Demo.Core.Context;
using Demo.Core.Entites;
using Demo.Core.Repositories.Base;

namespace Demo.Core.Repositories;

public class MaterialRepository : Repository<Material>
{
    public MaterialRepository(DBContext context) : base(context)
    {
    }
}