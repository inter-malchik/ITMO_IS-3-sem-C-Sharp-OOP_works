using Shops.Buyers;
using Shops.Exceptions.ShopManagers;
using Shops.Goods;

namespace Shops.Exceptions.Buying;

public class BuyingException : ShopBaseException
{
    private BuyingException(string message)
        : base(message)
    { }

    public static BuyingException
        NotEnoughGoods(GoodInfo goodInfo, int amount)
    {
        return new BuyingException($"Not ehough good: {goodInfo.Good.Naming} ({goodInfo.Good.Id}): {goodInfo.Stock} for {amount}");
    }

    public static BuyingException
        NotEnoughMoney(decimal moneyAmount, Buyer buyer)
    {
        return new BuyingException($"Buyer {buyer.Name} ({buyer.Id}) can't spend {moneyAmount} because he has only {buyer.Money}");
    }
}