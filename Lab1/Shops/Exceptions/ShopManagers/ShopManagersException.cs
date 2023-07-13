using Shops.Buyers;
using Shops.Goods;
using Shops.ShopManagers;
using Shops.Shops;

namespace Shops.Exceptions.ShopManagers;

public class ShopManagersException : ShopBaseException
{
    private ShopManagersException(string message)
        : base(message)
    { }

    public static ShopManagersException
        AlreadyRegisteredOnAddress(Shop shop, string address)
    {
        return new ShopManagersException($"Shop {shop.ShopName} ({shop.Id}) already present on address {address}");
    }

    public static ShopManagersException
        GoodIsNotRegisteredInManager(ShopManager shopManager, Good good)
    {
        return new ShopManagersException($"Good {good.Naming} ({good.Id}) not present on manager {shopManager.ManagerName}");
    }

    public static ShopManagersException
        GoodIsAlreadyRegisteredInManager(ShopManager shopManager, Good good)
    {
        return new ShopManagersException($"Good {good.Naming} ({good.Id}) already present on manager {shopManager.ManagerName}");
    }

    public static ShopManagersException
        BuyerIsAlreadyRegisteredInManager(ShopManager shopManager, Buyer buyer)
    {
        return new ShopManagersException($"Good {buyer.Name} ({buyer.Id}) already present on manager {shopManager.ManagerName}");
    }

    public static ShopManagersException
        BeneficialShopNotFound(ShopManager shopManager)
    {
        return new ShopManagersException($"Can't find cheapest shop in {shopManager.ManagerName}");
    }

    public static ShopManagersException
        CanNotBuyGoods(ShopManager shopManager)
    {
        return new ShopManagersException($"GoodsConsignment cannot be bought in any of the shop in {shopManager.ManagerName}");
    }
}