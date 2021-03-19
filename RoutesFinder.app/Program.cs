
using RoutesFinder.helper;
using RoutesFinder.domain.repository;
using RoutesFinder.domain.model;
using System;
using RoutesFinder.domain.service;
using System.Collections.Generic;

namespace RoutesFinder.app
{
    class Program
    {

        public static RouteData data;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                WriteLn("Nenhum parametro informado.");
                return;
            }

            IRouteFile routeFile = new RouteFile(args[0]);
            data = new RouteData(routeFile);

             

            ShowMenu();
        }

        private static void printMenu()
        {
            WriteLn("//////////// MENU ///////////");
            WriteLn("1. Listar Rotas Disponiveis");
            WriteLn("2. Consultar Melhor Rota");
            WriteLn("3. Adicionar Nova Rota");
            WriteLn("4. Sair");
            WriteLn("////////////////////////////");
        }

        public static void ShowMenu()
        {

            while (true)
            {
                Console.Clear();
                printMenu();
                var opcao = Console.ReadLine();


                switch (opcao)
                {
                    case "1":
                        ShowRoutes();
                        break;
                    case "2":
                        FindBestRoute();
                        break;
                    case "3":
                        AddRoute();
                        break;
                    case "4":
                        return;

                }

            }

        }

        private static void AddRoute()
        {
            string orig, dest;
            double custo;

            WriteLn("Digite a rota\n\n");

            WriteLn("Origin:");
            orig = Console.ReadLine();

            WriteLn("Destination:");
            dest = Console.ReadLine();

            WriteLn("Custo:");

            if (!double.TryParse(Console.ReadLine(), out custo))
            {
                WriteLn("Custo não é válido.");
                return;
            }


            var route = new Route(orig, dest, custo);
            data.AddRoute(route);



        }

        public static void ShowRoutes()
        {
            Console.Clear();
            WriteLn("/// ROTAS ///");
            WriteLn(string.Empty);

            foreach (Route r in data.GetRoutes())
                WriteLn(string.Format("origem:{0}, destino:{1}, custo:{2}", r.origin, r.destination, r.cost));

            WriteLn(string.Empty);
            PrintAndWaitMsg();
            return;

        }

        public static void FindBestRoute()
        {

            Console.Clear();
            WriteLn("Por favor digite a rota desejada. Ex.: GRU-CDG");

            string strRoute = Console.ReadLine().ToUpper();

            try
            {
                Route route = new Route(strRoute);

                var svc = new RouteService(data);

                List<BestRoute> brs = svc.FindBestRoute(route);

                // É possível que existam 2 rotas com o mesmo custo, então
                // deve ser exibida ambas as rotas
                WriteLn("A melhor rota:");

                foreach (var br in brs)
                {
                    WriteLn(br.routePath);
                    WriteLn("Custo:" + br.totalCost + "\n");

                }

                PrintAndWaitMsg("");
                return;
            }
            catch (Exception ex)
            {
                PrintAndWaitMsg(ex.Message);
                return;
            }


        }

        private static void WriteLn(string msg)
        {
            Console.WriteLine(msg);
        }

        private static void PrintAndWaitMsg(string msg = null)
        {
            WriteLn(msg == null ? "Tecle algo para continuar" : msg);
            Console.ReadKey();
        }
    }
}
