
using System;
namespace RoutesFinder.domain.Exceptions
{
    public class InvalidRouteDataException : System.Exception
    {
        public InvalidRouteDataException() : base("Dados de rota são inválidos")
        { }

        public InvalidRouteDataException(string message) : base(message)
        { }

        public InvalidRouteDataException(string message, System.Exception inner)
            : base(message, inner)
        { }
    }
}