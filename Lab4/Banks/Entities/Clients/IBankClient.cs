namespace Banks.Entities.Clients;

public interface IBankClient
{
    public string Name { get; }

    public string Surname { get; }

    public string? Address { get; }

    public string? PassportNumber { get; }

    Guid Id { get; }

    bool IsDoubtful { get; }
}