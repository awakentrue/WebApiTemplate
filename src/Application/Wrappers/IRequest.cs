namespace Application.Wrappers;

public interface IRequest<T> : MediatR.IRequest<Response<T>>
{
}