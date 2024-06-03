using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rents;
using CleanArchitecture.Domain.Vehicles;
using CleanArchitecture.Domain.Shared;


public sealed class Rent : Entity
{

    private  Rent(
        Guid id,
        Guid vehicleId,
        Guid userId,
        DateRange duration,
        Currency priceForPeriod,
        Currency maintenance,  
        Currency accessories,
        Currency totalPrice,
        RentStatus status,
        DateTime creationDate
        
        ) : base(id)
    {
        VehicleId = vehicleId;
        UserId = userId;
        Duration  = duration ;
        PriceForPeriod = priceForPeriod;
        Maintenance = maintenance;
        Accessories = accessories;
        TotalPrice = totalPrice;
        Status = status;
        CreationDate = creationDate;
    }

    public Guid VehicleId {get; private set;}

    public Guid UserId {get; private set;}
    public Currency? PriceForPeriod {get; private set;}
    public Currency? Maintenance {get; private set;}
    public Currency? Accessories {get; private set;}
    public Currency? TotalPrice {get; private set;}
    public RentStatus Status {get; private set;}

    public DateRange? Duration {get; private set;}

    public DateTime? CreationDate {get; private set;}
    public DateTime? ConfirmationDate {get; private set;}
    public DateTime? CompletedDate {get; private set;}
    public DateTime? DenialDate {get; private set;}

    public DateTime? CancellationDate {get; private set;}


    public static Rent Book(
      Vehicle vehicle,
      Guid userId,
      DateRange duration,
      DateTime creationDate,
      PriceService priceService
    )
    {
        var detailPrice = priceService.CalculatePrice(
            vehicle,
            duration
        );
        var rent = new Rent(
            Guid.NewGuid(),
            vehicle.Id,
            userId,
            duration,
            detailPrice.PriceForPeriod,
            detailPrice.Maintenance,
            detailPrice.Accessories,
            detailPrice.TotalPrice,
            RentStatus.Reserved,
            creationDate
        );
        
        rent.RaiseDomainEvent(new BookedRentDomainEvent(rent.Id!));

        vehicle.LastDateRent = creationDate;
        
        return rent;
    }

    public Result Confirm(DateTime utcNow)
    {
        if (Status != RentStatus.Reserved)
        {
            return Result.Failure(RentErrors.NotReserved);

        }
        Status = RentStatus.Confirmed;
        ConfirmationDate = utcNow;
        RaiseDomainEvent(new ConfirmedRentDomainEvent(Id));
        return Result.Success();
    }

    
    public Result Reject(DateTime utcNow)
    {
        if (Status != RentStatus.Reserved)
        {
            return Result.Failure(RentErrors.NotReserved);

        }
        Status = RentStatus.Rejected;
        ConfirmationDate = utcNow;
        RaiseDomainEvent(new RejectedRentDomainEvent(Id));
        return Result.Success();
    }


     public Result Cancel(DateTime utcNow)
    {
        if (Status != RentStatus.Confirmed)
        {
            return Result.Failure(RentErrors.NotConfirmed);

        }
        var currentDate = DateOnly.FromDateTime(utcNow);    
        if (currentDate > Duration!.Start)
        {
            return Result.Failure(RentErrors.NotConfirmed);

        }


        Status = RentStatus.Canceled;
        CancellationDate = utcNow;
        RaiseDomainEvent(new CanceledRentDomainEvent(Id));
        return Result.Success();
    }


    public Result Complete(DateTime utcNow)
    {
        if (Status != RentStatus.Confirmed)
        {
            return Result.Failure(RentErrors.NotConfirmed);

        }
        Status = RentStatus.Completed;
        CompletedDate = utcNow;
        RaiseDomainEvent(new CompletedRentDomainEvent(Id));
        return Result.Success();
    }
}

