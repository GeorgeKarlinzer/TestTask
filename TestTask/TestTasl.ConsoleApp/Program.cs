using System;
using System.Linq;
using System.Xml.Linq;
using TestTask.Logic;
using TestTask.Logic.Dtos;

namespace TestTasl.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var membersSource = "./struktura.xml";
            var transfersSource = "./przelewy.xml";

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

            var calc = new CommissionSystem();

            var results = calc.CalculateStats(memberDtos, transferDtos);

            foreach (var line in results)
                Console.WriteLine(line);
        }
    }
}
