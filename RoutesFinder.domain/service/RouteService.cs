using System.Collections.Generic;
using System.Linq;
using RoutesFinder.domain.model;
using RoutesFinder.domain.repository;

namespace RoutesFinder.domain.service
{
    public class RouteService
    {
        private IRouteData _routeData;

        public RouteService(IRouteData routeData)
        {
            this._routeData = routeData;
        }

        public List<BestRoute> FindBestRoute(Route routeToFind)
        {
            List<BestRoute> bestRoutes = new List<BestRoute>();

            List<Route> auxRoutes = (from Route r in _routeData.GetRoutes()
                                     where r.origin == routeToFind.origin
                                     select r).ToList();

            foreach (Route auxroute in auxRoutes)
            {

                var auxBestRoute = new BestRoute(auxroute);
                MountRoute(routeToFind, ref auxBestRoute);

                if (auxBestRoute.lastRouteAdded.destination == routeToFind.destination)
                {
                    if (bestRoutes.Count == 0)
                    {
                        bestRoutes.Add(auxBestRoute);
                        continue;
                    }

                    if (auxBestRoute.totalCost < bestRoutes.FirstOrDefault().totalCost)
                    {
                        bestRoutes.Clear();
                        bestRoutes.Add(auxBestRoute);
                    }
                    else if (auxBestRoute.totalCost == bestRoutes.FirstOrDefault().totalCost)
                        bestRoutes.Add(auxBestRoute);

                }


            }

            return bestRoutes;

        }

        private void MountRoute(Route routeToFind, ref BestRoute bestroute)
        {

            if (routeToFind.destination == bestroute.lastRouteAdded.destination)
                return;

            string lastDestinationAdd = string.Empty;
            Route r;

            if (bestroute == null)
                lastDestinationAdd = routeToFind.destination;
            else
                if (bestroute.lastRouteAdded == null)
                lastDestinationAdd = routeToFind.destination;
            else
                lastDestinationAdd = bestroute.lastRouteAdded.destination;


            r = this._routeData.GetRoutes().Where(_ => _.origin == lastDestinationAdd).FirstOrDefault();

            if (r == null)
                return;

            bestroute.addRoute(r);

            //if (routeToFind.destination == bestroute.lastRouteAdded.destination)
            //    return;
            //else
                MountRoute(routeToFind, ref bestroute);

        }


    }
}