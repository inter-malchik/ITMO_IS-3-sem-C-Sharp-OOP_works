using Shops.Buyers;
using Shops.Exceptions;
using Shops.Exceptions.ShopManagers;
using Shops.Exceptions.Shops;
using Shops.Goods;
using Shops.Shops;

namespace Shops.ShopManagers;

public class ShopManager : IShopManager
{
    private readonly List<Shop> _shops;
    private readonly List<Good> _goods;

    public ShopManager(string name = "default")
    {
        _shops = new List<Shop>();
        _goods = new List<Good>();
        ManagerName = name;
    }

    public IReadOnlyList<Shop> RegisteredShops => _shops.AsReadOnly();

    public IReadOnlyList<Good> RegisteredGoods => _goods.AsReadOnly();

    public string ManagerName { get; }

    public Shop CreateShop(string name, string address)
    {
        Shop? registeredShop = _shops.FirstOrDefault(shop => shop.ShopAddress == address);

        if (registeredShop != null)
        {
            throw ShopManagersException.AlreadyRegisteredOnAddress(registeredShop, address);
        }

        var newShop = new Shop(name, address, this);

        _shops.Add(newShop);
        return newShop;
    }

    public Good RegisterGood(string name)
    {
        Good? registeredGood = _goods.FirstOrDefault(good => good.Naming == name);

        if (registeredGood != null)
        {
            throw ShopManagersException.GoodIsAlreadyRegisteredInManager(this, registeredGood);
        }

        var newGood = new Good(name);

        _goods.Add(newGood);
        return newGood;
    }

    public Shop GetBeneficialShop(GoodsConsignment goodsConsignment)
    {
        decimal cheapestTotalAmount = decimal.MaxValue;
        Shop? cheapestShop = null;

        foreach (var currentShop in _shops)
        {
            try
            {
                decimal currentTotalAmount = currentShop.PriceOfGoodsPartion(goodsConsignment);

                if (currentTotalAmount < cheapestTotalAmount)
                {
                    cheapestTotalAmount = currentTotalAmount;
                    cheapestShop = currentShop;
                }
            }
            catch (ShopsException)
            {
                continue;
            }
        }

        return cheapestShop ?? throw ShopManagersException.BeneficialShopNotFound(this);
    }
}