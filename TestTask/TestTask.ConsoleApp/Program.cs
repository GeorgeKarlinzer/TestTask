using System;
using System.Linq;
using System.Xml.Linq;
using TestTask.Logic;
using TestTask.Logic.Dtos;

namespace TestTask.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var membersSource = "./struktura.xml";
            var transfersSource = "./przelewy.xml";

            /* Używam MemberDto oraz TransferDto, by uniezależnić program od typu danych wejściowych.
             * Głównie używam tego do testów.
             * 
             * Jedynym warunkiem jest to, że dane wejściowe muszą zachowywać strukturę pliku xml:
             *      ! Dziecięcy element zawsze idzie po rodzicu !
             */
            var memberDtos = XDocument.Load(membersSource)
                .Descendants("uczestnik")
                .Select(x => new MemberDto()
                {
                    Id = (int)x.Attribute("id"),
                    SupervisorId = (int?)x.Parent.Attribute("id")
                });

            var transferDtos = XDocument.Load(transfersSource)
                .Descendants("przelew")
                .Select(x => new TransferDto()
                {
                    From = (int)x.Attribute("od"),
                    Amount = (int)x.Attribute("kwota")
                });

            var cs = new CommissionSystem();

            var results = cs.CalculateStats(memberDtos, transferDtos);

            foreach (var line in results)
                Console.WriteLine(line);
        }
    }
}
