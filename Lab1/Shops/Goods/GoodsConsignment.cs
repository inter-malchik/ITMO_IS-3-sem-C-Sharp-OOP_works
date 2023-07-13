using System.Collections;
using System.Text;

namespace Shops.Goods;

public class GoodsConsignment
{
    private readonly List<GoodAmount> _goods;

    public GoodsConsignment()
    {
        _goods = new List<GoodAmount>();
    }

    public IReadOnlyCollection<GoodAmount> Goods => _goods.AsReadOnly();

    public void AppendGood(Good good, int amount = 1)
    {
        _goods.Add(new GoodAmount(good, amount));
    }
}