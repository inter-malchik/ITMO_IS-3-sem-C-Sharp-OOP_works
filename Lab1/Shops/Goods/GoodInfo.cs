using Shops.Exceptions.Buying;
using Shops.Exceptions.Validation;

namespace Shops.Goods;

public class GoodInfo
{
    private int _stock;
    public GoodInfo(Good good, decimal price, int stock = 0)
    {
        Good = good;
        Price = price;
        Stock = stock;
    }

    public GoodInfo(Good good, int stock = 0)
    {
        Good = good;
        Stock = stock;
    }

    public decimal Price { get; private set; }

    public int Stock
    {
        get { return _stock; }
        private set { _stock = ValidateStock(value); }
    }

    public Good Good { get; }

    public bool Equals(GoodInfo? other)
    {
        return other is not null && Good == other.Good;
    }

    public void RedeemGood(int deltaAmount)
    {
        Stock += deltaAmount;
    }

    public void SellOffGood(int deltaAmount)
    {
        if (deltaAmount > Stock)
        {
            throw BuyingException.NotEnoughGoods(this, deltaAmount);
        }

        Stock -= deltaAmount;
    }

    public void ChangePrice(decimal newPrice)
    {
        if (newPrice < 0)
        {
            throw ValidationException.NegativeMoney(newPrice);
        }

        Price = newPrice;
    }

    private static int ValidateStock(int stock)
    {
        if (stock < 0)
        {
            throw ValidationException.NegativeAmount(stock);
        }

        return stock;
    }
}