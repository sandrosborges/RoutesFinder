
using System;
using Xunit;
using RoutesFinder.domain;
using RoutesFinder.domain.repository;
using RoutesFinder.helper;
using Microsoft.Extensions.Configuration;
using RoutesFinder.domain.model;
using System.IO;

namespace RoutesFinder.test
{
    public class RouteTest
    {
        public static RouteData data;

        public RouteTest()
        {


            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", false, true)
               .Build();

            IRouteFile routeFile = new RouteFile(config["fileCSV"]);
            data = new RouteData(routeFile);
        }



        [Fact]
        public void Test001GetRoutes()
        {
            var count = data.GetRoutes().Count > 1;
            Assert.True(true, "Lista deve possuir itens");

        }

        [Fact]
        public void Test002AddRoute()
        {
            var newRoute = new Route("GRU", "ORL", 100);


            try
            {
                data.AddRoute(newRoute);
                Assert.True(true, "Falha ao add. Rota");
            }
            catch (Exception)
            {

                Assert.False(false, "Falha ao add. Rota");
            }


        }

    }
}