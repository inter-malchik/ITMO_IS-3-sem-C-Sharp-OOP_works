using Shops.Goods;
using Shops.Shops;

namespace Shops.ShopManagers;

public interface IShopManager
{
    Shop CreateShop(string name, string address);

    Good RegisterGood(string name);

    Shop GetBeneficialShop(GoodsConsignment goodsConsignment);
}