using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehicles;
using CleanArchitecture.Domain.Rents;
using CleanArchitecture.Application.Abstractions.Clock;

namespace CleanArchitecture.Application.Rents.BookRent;

internal sealed class BookRentCommandHandler : ICommandHandler<BookRentCommand,Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IVehicleRepository _vehicleRepository;

    private readonly IRentRepository _rentRepository;

    private readonly PriceService _priceService;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IDateTimeProvider _dateTimeProvider;

    public BookRentCommandHandler(IUserRepository userRepository, IVehicleRepository vehicleRepository, IRentRepository rentRepository, PriceService priceService, IUnitOfWork unitOfWork , IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _vehicleRepository = vehicleRepository;
        _rentRepository = rentRepository;
        _priceService = priceService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(
        BookRentCommand request ,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null){
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId , cancellationToken);
         if (vehicle is null){
            return Result.Failure<Guid>(VehicleErrors.NotFound);
        }

        var duration = DateRange.Create(request.StartDate, request.EndDate);

        if(await _rentRepository.IsOverlappingAsync(vehicle,duration,cancellationToken))
        {
            return Result.Failure<Guid>(RentErrors.Overlap);
        }

        var rent = Rent.Book(vehicle, request.VehicleId,duration,_dateTimeProvider.CurrentTime,_priceService);
        _rentRepository.Add(rent);

        await _unitOfWork.SaveChangeAsync(cancellationToken);
        return rent.Id;
    }

}