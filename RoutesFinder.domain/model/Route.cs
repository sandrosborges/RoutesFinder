using System;
using RoutesFinder.domain.Exceptions;

namespace RoutesFinder.domain.model
{
    public class Route
    {
        public string origin { get; set; }
        public string destination { get; set; }
        public double cost { get; set; }

        public Route(string origin, string destination, double cost)
        {
           if(!validate(origin.ToUpper()) || !validate(destination.ToUpper()))
                throw new InvalidRouteException();

           if(origin.ToUpper() == destination.ToUpper())
                throw new InvalidRouteException("Origem e destino não podem ser iguais.");

            this.origin = origin.ToUpper();
            this.destination = destination.ToUpper();
            this.cost = cost;
        }

        public Route(string strRoute)
        {
            SetRoute(strRoute);
        }

        public void SetRoute(string strRoute)
        {
            string[] r = strRoute.Split('-');

            if (r.Length != 2)
                throw new InvalidRouteException();

            foreach (var item in r)
            {
                validate(item);
            }

            this.origin = r[0];
            this.destination = r[1];
            this.cost = 0;

        }

        private bool validate(string toBeTested)
        {
            return System.Text.RegularExpressions.Regex.Match(toBeTested, "^[A-Z]{3}").Success;
        }

        public Route() { }
    }
}