using MediatR;

namespace CleanArchitecture.Domain.Abstractions.Messaging;

public interface IQuery <TResponse>: IRequest<Result<TResponse>>
 {
    
 }