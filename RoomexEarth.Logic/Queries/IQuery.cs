using MediatR;

namespace RoomexEarth.Logic.Queries
{
    /// <summary>
    /// Indicates a CQRS query.
    /// </summary>
    /// <typeparam name="TResult">The type of result from the query.</typeparam>
    public interface IQuery<out TResult> : IRequest<TResult> { }
}
