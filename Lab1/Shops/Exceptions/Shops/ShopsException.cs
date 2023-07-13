using Shops.Exceptions.ShopManagers;
using Shops.Goods;
using Shops.Shops;

namespace Shops.Exceptions.Shops;

public class ShopsException : ShopBaseException
{
    private ShopsException(string message)
        : base(message)
    { }

    public static ShopsException
        NotEnoughGoodInShop(GoodInfo goodInfo, Shop shop, int expectedAmount)
    {
        return new ShopsException($"shop {shop.ShopName} ({shop.Id}) only has {goodInfo.Stock} of good {goodInfo.Good} ({goodInfo.Good.Id}) (Expected: {expectedAmount})");
    }

    public static ShopsException
        GoodIsNotPresentInTheShop(Good good, Shop shop)
    {
        return new ShopsException($"{good.Naming} ({good.Id}) is not present in the shop {shop.ShopName} ({shop.Id})");
    }

    public static ShopsException
        GoodIsAlreadyPresentInTheShop(Good good, Shop shop)
    {
        return new ShopsException($"{good.Naming} ({good.Id}) is already present in the shop {shop.ShopName} ({shop.Id})");
    }
}