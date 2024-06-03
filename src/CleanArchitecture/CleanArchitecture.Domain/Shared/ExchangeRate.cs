namespace CleanArchitecture.Domain.Shared;

public record ExchangeRate
{
    public static readonly ExchangeRate None = new("");
    public static readonly ExchangeRate Usd = new("USD");
    public static readonly ExchangeRate Eur = new("EUR");
    private ExchangeRate(string code) => Code = code;
    public string? Code {get; init;}
    public static readonly IReadOnlyCollection<ExchangeRate> All = new[] 
    {
        Usd,
        Eur
    };
    public static ExchangeRate  FromCode(string code)
    {
        return All.FirstOrDefault(c=> c.Code == code)??
            throw new ApplicationException("Currency Invalid"); 
    }
}



