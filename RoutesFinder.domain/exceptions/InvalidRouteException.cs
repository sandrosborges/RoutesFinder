
using System;
namespace RoutesFinder.domain.Exceptions
{
    public class InvalidRouteException : Exception
    {
        public InvalidRouteException() : base("Rota informada não é valida.")
        { }

        public InvalidRouteException(string message) : base(message)
        { }

        public InvalidRouteException(string message, System.Exception inner)
            : base(message, inner)
        { }
    }
}