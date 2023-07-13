using Banks.Exceptions.Model;

namespace Banks.Entities.Clients;

public class Client : IBankClient
{
    private Client(string name, string surname, string? address, string? passportNumber)
    {
        Name = name;
        Surname = surname;
        Address = address;
        PassportNumber = passportNumber;
        Id = Guid.NewGuid();
    }

    public static ClientBuilder Builder => new ClientBuilder();

    public string Name { get; }

    public string Surname { get; }

    public string? Address { get; set; }

    public string? PassportNumber { get; set; }

    public Guid Id { get; }

    public bool IsDoubtful => PassportNumber is null || PassportNumber == string.Empty ||
                              Address is null || Address == string.Empty;

    public void Notify()
    { }

    public class ClientBuilder
    {
        private string? _name;
        private string? _surname;
        private string? _address;
        private string? _passport;

        public ClientBuilder AddFirstName(string name)
        {
            _name = name;
            return this;
        }

        public ClientBuilder AddSurname(string surname)
        {
            _surname = surname;
            return this;
        }

        public ClientBuilder AddAddress(string address)
        {
            _address = address;
            return this;
        }

        public ClientBuilder AddPassport(string passport)
        {
            _passport = passport;
            return this;
        }

        public Client Build()
        {
            return new Client(
                _name ?? throw ClientException.NoNameSpecified(),
                _surname ?? throw ClientException.NoSurnameSpecified(),
                _address,
                _passport);
        }
    }
}