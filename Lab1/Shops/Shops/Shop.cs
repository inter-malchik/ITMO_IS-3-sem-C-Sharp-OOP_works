using Shops.Buyers;
using Shops.Exceptions.Buying;
using Shops.Exceptions.ShopManagers;
using Shops.Exceptions.Shops;
using Shops.Goods;
using Shops.ShopManagers;

namespace Shops.Shops;

public class Shop : IEquatable<Shop>
{
    private readonly List<GoodInfo> _goods;
    private readonly ShopManager _shopManager;

    public Shop(string name, string address, ShopManager manager)
    {
        Id = Guid.NewGuid();
        ShopName = name;
        ShopAddress = address;
        _shopManager = manager;
        _goods = new List<GoodInfo>();
    }

    public Guid Id { get; }
    public string ShopName { get; }
    public string ShopAddress { get; }

    public void SupplyGoods(GoodsConsignment goodsConsignment)
    {
        foreach (var goodStock in goodsConsignment.Goods)
        {
            SupplyGood(goodStock);
        }
    }

    public void SupplyGood(GoodAmount goodAmount)
    {
        GoodInfo goodInShop = _goods.FirstOrDefault(good => good.Good.Equals(goodAmount.Good)) ?? RegisterGood(goodAmount.Good);
        goodInShop.RedeemGood(goodAmount.Amount);
    }

    public GoodInfo RegisterGood(Good newGood, decimal price = 0)
    {
        if (_goods.Any(good => good.Good == newGood))
        {
            throw ShopsException.GoodIsAlreadyPresentInTheShop(newGood, this);
        }

        if (!_shopManager.RegisteredGoods.Contains(newGood))
        {
            throw ShopManagersException.GoodIsNotRegisteredInManager(_shopManager, newGood);
        }

        var newGoodInfo = new GoodInfo(newGood, price);
        _goods.Add(newGoodInfo);
        return newGoodInfo;
    }

    public bool Equals(Shop? other)
    {
        return other is not null && Id == other.Id;
    }

    public GoodInfo GoodInShop(Good targetGood)
    {
        return _goods.FirstOrDefault(good => good.Good.Equals(targetGood)) ??
               throw ShopsException.GoodIsNotPresentInTheShop(targetGood, this);
    }

    public void SellProduct(Buyer buyer, Good boughtGood, int amount)
    {
        GoodInfo goodInShop = GoodInShop(boughtGood);
        buyer.WithdrawMoney(goodInShop.Price * amount);
        goodInShop.SellOffGood(amount);
    }

    public decimal PriceOfProduct(Good boughtGood, int amount = 1)
    {
        GoodInfo goodInShop = GoodInShop(boughtGood);

        if (amount > goodInShop.Stock)
        {
            throw ShopsException.NotEnoughGoodInShop(goodInShop, this, amount);
        }

        return goodInShop.Price * amount;
    }

    public int AmountOfProduct(Good seekGood)
    {
        GoodInfo goodInShop = GoodInShop(seekGood);

        return goodInShop.Stock;
    }

    public decimal PriceOfGoodsPartion(GoodsConsignment goodsConsignment)
    {
        decimal totalPrice = 0;

        foreach (var goodStock in goodsConsignment.Goods)
        {
            totalPrice += PriceOfProduct(goodStock.Good, goodStock.Amount);
        }

        return totalPrice;
    }

    public void SellPartionOfProducts(Buyer buyer, GoodsConsignment goodsConsignment)
    {
        decimal expectedPrice = PriceOfGoodsPartion(goodsConsignment);

        if (expectedPrice > buyer.Money)
        {
            throw BuyingException.NotEnoughMoney(expectedPrice, buyer);
        }

        foreach (var goodStock in goodsConsignment.Goods)
        {
            SellProduct(buyer, goodStock.Good, goodStock.Amount);
        }
    }

    public void ChangeGoodPrice(Good good, decimal newprice)
    {
        GoodInfo goodInShop = GoodInShop(good);
        goodInShop.ChangePrice(newprice);
    }
}