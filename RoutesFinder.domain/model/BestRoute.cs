using System.Collections.Generic;
using System.Linq;

namespace RoutesFinder.domain.model
{
    public class BestRoute
    {
        public Route lastRouteAdded { get; set; }
        public string routePath
        {
            get
            {
                var _path = "";
                bool _first = true;
                foreach (var item in this._routes)
                {
                    _path += string.Format(_first ? "{0}-{1}" : "-{1}", item.origin, item.destination);
                    _first = false;
                }

                return _path;
            }
        }

        public double totalCost { get { return _routes.Sum(x => x.cost); } }
        private List<Route> _routes;


        public BestRoute()
        {
            this._routes = new List<Route>();
        }
        public BestRoute(Route route)
        {
            this._routes = new List<Route>();
            this.addRoute(route);
        }

        public void addRoute(Route route)
        {
            var hasItem = (from Route x in this._routes
                           where x.origin == route.origin && x.destination == route.destination
                           select x).FirstOrDefault();

            if (hasItem == null)
            {
                if (this._routes == null)
                    this._routes = new List<Route>();

                this._routes.Add(route);

            }

            this.lastRouteAdded = route;

            // this.routePath += ( string.IsNullOrWhiteSpace(this.routePath) ? string.Format("{0}-{1}", route.origin, route.destination) : string.Format("-{0}", route.destination));
        }
    }
}