using MediatR;

namespace Shared.Core;
public interface IQuery<out TResponse> : IRequest<TResponse>;
