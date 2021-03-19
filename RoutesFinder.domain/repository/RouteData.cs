
using RoutesFinder.helper;
using RoutesFinder.domain.model;
using System;
using System.Collections.Generic;
using RoutesFinder.domain.Exceptions;
using System.Linq;

namespace RoutesFinder.domain.repository
{
    public class RouteData : IRouteData
    {
        private IRouteFile _routeFile;
        private List<Route> _routes { get; }

        public RouteData(IRouteFile routeFile)
        {
            this._routeFile = routeFile;
            this._routes = new List<Route>();
            this.setRoutes();
        }

        private void setRoutes()
        {

            if (!string.IsNullOrWhiteSpace(_routeFile.content))
            {
                string[] lines = _routeFile.content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                foreach (string line in lines)
                {
                    // elimina linha em branco
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] routeArray = line.Split(',');
                    if (routeArray.Length != 3)
                        throw new InvalidRouteDataException();

                    Route r = new Route(routeArray[0], routeArray[1], double.Parse(routeArray[2]));

                    this._routes.Add(r);

                }
            }

        }

        public List<Route> GetRoutes()
        {
            return this._routes;
        }

        public void AddRoute(Route route)
        {
            var ret = (from Route x in this._routes
                      where x.origin == route.origin && x.destination == route.destination
                      select x).FirstOrDefault();

            // atualizacao (atualiza o custo)
            if(ret != null)
                this._routes.Remove(ret);

            this._routes.Add(route);
            this.save();

        }

        private void save()
        {
            List<string> lines = new List<string>();

            foreach (var item in this._routes)             
                lines.Add(string.Format("{0},{1},{2}", item.origin, item.destination,item.cost));

            this._routeFile.saveFile(lines.ToArray());


        }
    }

}