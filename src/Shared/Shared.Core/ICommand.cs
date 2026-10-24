using MediatR;

namespace Shared.Core;

public interface ICommand<out TResponse> : IRequest<TResponse>;

public interface ICommand : IRequest;
