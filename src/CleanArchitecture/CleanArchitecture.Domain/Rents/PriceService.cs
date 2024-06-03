using CleanArchitecture.Domain.Vehicles;
using CleanArchitecture.Domain.Shared;

namespace   CleanArchitecture.Domain.Rents;

public class PriceService
{
    public DetailPrice CalculatePrice(Vehicle vehicle , DateRange dateRange)
    {
        var exchangeRate = vehicle.Price!.ExchangeRate;
        var priceForPeriod = new Currency( 
            dateRange.NumberDays * vehicle.Price.Amount ,
            exchangeRate);

        decimal percentageChange = 0 ;
        
        foreach(var accessory in vehicle.Accessories)
        {
            percentageChange += accessory switch
            {
                Accessory.AppleCar or Accessory.AndroidCar => 0.05m,
                Accessory.AirConditioning => 0.01m,
                Accessory.Maps => 0.01m,
                _ => 0

            };

        }
        var accessoryCharge = Currency.Zero(exchangeRate);
        if(percentageChange > 0)
        {
            accessoryCharge = new Currency(
                priceForPeriod.Amount + percentageChange,
                exchangeRate
            );

        }

        var totalPrice = Currency.Zero();
        totalPrice += priceForPeriod;

        if(!vehicle!.Maintenance.IsZero())
        {
            totalPrice += vehicle.Maintenance;
        }

        return new DetailPrice(
             priceForPeriod, 
             vehicle.Maintenance ,
             accessoryCharge,
             totalPrice
        );
    }
}