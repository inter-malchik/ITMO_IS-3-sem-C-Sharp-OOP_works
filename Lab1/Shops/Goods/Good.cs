namespace Shops.Goods;

public class Good : IEquatable<Good>
{
    public Good(string name)
    {
        Naming = name;
        Id = Guid.NewGuid();
    }

    public string Naming { get; }

    public Guid Id { get; }

    public bool Equals(Good? other)
    {
        return other is not null && Id == other.Id;
    }
}