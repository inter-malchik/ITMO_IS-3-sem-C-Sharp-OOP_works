using Shops.Buyers;
using Shops.Goods;
using Shops.ShopManagers;
using Shops.Shops;
using Xunit;

namespace Shops.Test;

public class ShopServiceTest
{
    [Fact]
    public void RegisterShop_ManagerHasShop()
    {
        var shopManager = new ShopManager();
        var shop = shopManager.CreateShop("shop name", "shop address");

        Assert.Contains(shop, shopManager.RegisteredShops);
    }

    [Fact]
    public void RegisterGood_ManagerHasGood()
    {
        var shopManager = new ShopManager();
        var product = shopManager.RegisterGood("product name");

        Assert.Contains(product, shopManager.RegisteredGoods);
    }

    [Fact]
    public void BuyProduct_ProductBoughtAndMoneySpent()
    {
        decimal moneyBefore = 100, productPrice = 5;
        int productCount = 100, productToBuyCount = 5;

        var person = new Buyer("name", moneyBefore);
        var shopManager = new ShopManager();
        var shop = shopManager.CreateShop("shop name", "shop address");
        var product = shopManager.RegisterGood("product name");

        shop.SupplyGood(new GoodAmount(product, productCount));
        shop.ChangeGoodPrice(product, productPrice);
        shop.SellProduct(person, product, productToBuyCount);

        Assert.True(moneyBefore - (productPrice * productToBuyCount) == person.Money);
        Assert.True(productCount - productToBuyCount == shop.AmountOfProduct(product));
    }

    [Fact]
    public void BuyProduct_BuyProductBunch()
    {
        decimal moneyBefore = 1000, moneyspent = 0;

        var person = new Buyer("name", moneyBefore);
        var shopManager = new ShopManager();
        var shop = shopManager.CreateShop("shop name", "shop address");

        decimal[] productPrices = { 3, 4, 5 };

        var products = new List<Good>();

        products.Add(shopManager.RegisterGood("Sergei Papikan"));
        products.Add(shopManager.RegisterGood("Danny Titov"));
        products.Add(shopManager.RegisterGood("Nastya Ermolaeva"));

        var goodsConsignment = new GoodsConsignment();

        for (int i = 0; i < products.Count; i++)
        {
            shop.SupplyGood(new GoodAmount(products[i], 100));
            shop.ChangeGoodPrice(products[i], productPrices[i]);
            goodsConsignment.AppendGood(products[i], i);
            moneyspent += productPrices[i] * i;
        }

        shop.SellPartionOfProducts(person, goodsConsignment);

        Assert.True(moneyBefore - moneyspent == person.Money);
    }

    [Fact]
    public void ShopManager_CheapestShop()
    {
        var shopManager = new ShopManager();

        decimal[] productPrices = { 3, 4, 5, 6, 7, 8, 9, 10, 11 };

        var shops = new List<Shop>();
        var products = new List<Good>();

        products.Add(shopManager.RegisterGood("Sergei Papikan"));
        products.Add(shopManager.RegisterGood("Danny Titov"));
        products.Add(shopManager.RegisterGood("Nastya Ermolaeva"));

        var goodsConsignment = new GoodsConsignment();

        for (int i = 0; i < products.Count; i++)
        {
            var shop = shopManager.CreateShop($"shop name{i}", $"shop address{i}");
            shops.Add(shop);

            for (int j = 0; j < products.Count; j++)
            {
                shop.SupplyGood(new GoodAmount(products[j], 100));
                shop.ChangeGoodPrice(products[j], productPrices[i + j]);
            }

            goodsConsignment.AppendGood(products[i], i);
        }

        Assert.Equal(shopManager.GetBeneficialShop(goodsConsignment), shops[0]);
    }
}