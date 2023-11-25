using MediatR;

namespace RoomexEarth.Logic.Queries
{
    /// <summary>
    /// Indicates a CQRS query handler.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of result from the query.</typeparam>
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IRequest<TResult> { }
}
