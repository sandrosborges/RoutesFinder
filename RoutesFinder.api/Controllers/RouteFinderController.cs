using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RoutesFinder.domain.repository;
using RoutesFinder.domain.model;
using Microsoft.Extensions.Configuration;
using RoutesFinder.helper;
using System;
using Microsoft.AspNetCore.Http;
using RoutesFinder.domain.service;

namespace RoutesFinder.api.Controllers
{
    [Route("api/[controller]/routes")]
    [ApiController]
    public class RouteFinderController : ControllerBase
    {
        private IMemoryCache _cache;
        private IConfiguration _config;
        public RouteFinderController(IMemoryCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _config = configuration;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Route>> Get()
        {
            var cacheEntry = _cache.Get<RouteData>("RouteData");

            return
            cacheEntry.GetRoutes();
        }

        // GET api/values
        [HttpGet("best")]
        public ActionResult<object> GetBest([FromQuery] string origin, [FromQuery] string destination)
        {

            var cacheData = _cache.Get<RouteData>("RouteData");

            var strRoute = string.Format("{0}-{1}", origin.ToUpper(), destination.ToUpper());

            Route route = new Route(strRoute);

            var svc = new RouteService(cacheData);

            List<BestRoute> brs = svc.FindBestRoute(route);


            List<object> r = new List<object>();

            foreach (var item in brs)
            {
                var obj = new
                {
                    rota = item.routePath,
                    custo = item.totalCost
                };

                r.Add(obj);
            }

            return new { msg = "Melhor rota:", rota = r };

        }






        [HttpPost]
        public ActionResult PostRoute([FromBody] Route route)
        {
            try
            {
                var r = new Route(route.origin, route.destination, route.cost);


                IRouteFile routeFile = new RouteFile(this._config["fileCSV"]);
                IRouteData data = new RouteData(routeFile);

                data.AddRoute(route);
                _cache.Set("RouteData", data);

                return Ok();

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

        }
    }



}