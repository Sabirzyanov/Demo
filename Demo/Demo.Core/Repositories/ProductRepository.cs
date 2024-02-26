using Demo.Core.Context;
using Demo.Core.Repositories.Base;
using Warehouse.Core.DB.Entities;

namespace Demo.Core.Repositories;

public class ProductRepository : Repository<Product>
{
    public ProductRepository(DBContext context) : base(context)
    {
    }
}