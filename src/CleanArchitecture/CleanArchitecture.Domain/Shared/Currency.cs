namespace CleanArchitecture.Domain.Shared;

public record Currency(decimal Amount , ExchangeRate ExchangeRate)
{

    public static Currency operator +(Currency first , Currency second)
    {
        if(first.ExchangeRate != second.ExchangeRate)
        {
            throw new ApplicationException("Incorrect Currency"); 
        }

        return new Currency(first.Amount + second.Amount , first.ExchangeRate);
      
    }

    public static Currency Zero() => new (0, ExchangeRate.None);

    public static Currency Zero(ExchangeRate exchangeRate) => new (0, exchangeRate);

    public bool IsZero() => this == Zero(ExchangeRate);


}



