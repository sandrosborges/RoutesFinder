using System.Collections.Generic;
using RoutesFinder.domain.model;

namespace RoutesFinder.domain.repository
{
    public interface IRouteData
    {
        List<Route> GetRoutes();
        void AddRoute(Route route);

    }
}