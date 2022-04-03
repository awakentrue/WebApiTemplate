namespace Application.Wrappers;

public interface IRequestHandler<in TQuery, TResult> : MediatR.IRequestHandler<TQuery, Response<TResult>> where TQuery : IRequest<TResult>
{
}