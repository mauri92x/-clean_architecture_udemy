using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Domain.Rents;
using CleanArchitecture.Domain.Users;
using MediatR;

namespace CleanArchitecture.Application.Rents.BookRent;

internal sealed class BookRentDomainEventHandler
: INotificationHandler<BookedRentDomainEvent>
{
    private readonly IRentRepository _rentRepository;
    private readonly IUserRepository _userRepository;

    private readonly IEmailService _emailService;

    public BookRentDomainEventHandler(IRentRepository rentRepository, IUserRepository userRepository, IEmailService emailService)
    {
        _rentRepository = rentRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task  Handle(BookedRentDomainEvent notification, CancellationToken cancellationToken)
    {
        var rent = await _rentRepository.GetByIdAsync(notification.RentId, cancellationToken);

        if(rent is null)
        {
            return;
        }

        var user = await _userRepository.GetByIdAsync(
            rent.UserId,
            cancellationToken
        );

        if(user is null)
        {
            return;
        }

        await _emailService.SendAsync(
            user.Email!,
            "Rent Booked",
            "You have to confirm this booking otherwise it will be lost"
        );

    }
}