using Demo.Core.Repositories;

namespace Demo.WPF.Utils;

public static class GlobalContext
{
    public static MaterialRepository MaterialRepository { get; set; }
    public static MaterialTypeRepository MaterialTypeRepository { get; set; }
    public static ProductRepository ProductRepository { get; set; }
}