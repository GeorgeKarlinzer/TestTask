using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestTask.Logic;
using TestTask.Logic.Dtos;

namespace TestTask.Tests
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var commissionSystem = new CommissionSystem();
            var expectedResult = new List<string>()
            {
                "1 0 3 1152",
                "2 1 2 575",
                "3 2 1 250",
                "4 3 0 0",
                "5 2 0 0",
                "6 1 1 13",
                "7 2 0 0",
            };

            List<MemberDto> memberDtos;
            List<TransferDto> transferDtos;

            memberDtos = new()
            {
                new() { Id = 1, SupervisorId = null },
                new() { Id = 2, SupervisorId = 1 },
                new() { Id = 3, SupervisorId = 2 },
                new() { Id = 4, SupervisorId = 3 },
                new() { Id = 5, SupervisorId = 2 },
                new() { Id = 6, SupervisorId = 1 },
                new() { Id = 7, SupervisorId = 6 },
            };

            transferDtos = new()
            {
                new() { From = 3, Amount = 150 },
                new() { From = 6, Amount = 75 },
                new() { From = 1, Amount = 30 },
                new() { From = 2, Amount = 120 },
                new() { From = 2, Amount = 90 },
                new() { From = 4, Amount = 1000 },
                new() { From = 5, Amount = 500 },
                new() { From = 7, Amount = 25 },
            };

            var res = commissionSystem.CalculateStats(memberDtos, transferDtos).ToList();

            for (int i = 0; i < res.Count; i++)
                Assert.That(res[i], Is.EqualTo(expectedResult[i]));
        }

        [Test]
        public void Test2()
        {
            var commissionSystem = new CommissionSystem();
            var expectedResult = new List<string>()
            {
                "1 0 1 198",
                "2 1 1 93",
                "3 2 1 41",
                "4 3 1 22",
                "5 4 1 10",
                "6 5 1 6",
                "7 6 1 2",
                "8 7 1 4",
                "9 8 0 0",
            };

            List<MemberDto> memberDtos;
            List<TransferDto> transferDtos;

            memberDtos = new()
            {
                new() { Id = 1, SupervisorId = null },
                new() { Id = 2, SupervisorId = 1 },
                new() { Id = 3, SupervisorId = 2 },
                new() { Id = 4, SupervisorId = 3 },
                new() { Id = 5, SupervisorId = 4 },
                new() { Id = 6, SupervisorId = 5 },
                new() { Id = 7, SupervisorId = 6 },
                new() { Id = 8, SupervisorId = 7 },
                new() { Id = 9, SupervisorId = 8 },
            };

            transferDtos = new()
            {
                new() { From = 9, Amount = 10 },
                new() { From = 9, Amount = 25 },
                new() { From = 9, Amount = 128 },
                new() { From = 9, Amount = 76 },
                new() { From = 7, Amount = 93 },
                new() { From = 3, Amount = 22 },
                new() { From = 1, Amount = 22 },
            };

            var res = commissionSystem.CalculateStats(memberDtos, transferDtos).ToList();

            for (int i = 0; i < res.Count; i++)
                Assert.That(res[i], Is.EqualTo(expectedResult[i]));
        }

        [Test]
        public void Test3()
        {
            var commissionSystem = new CommissionSystem();
            var expectedResult = new List<string>()
            {
                "1 0 2 300",
                "2 1 0 0",
                "3 1 1 150",
                "4 2 0 0",
            };

            List<MemberDto> memberDtos;
            List<TransferDto> transferDtos;

            memberDtos = new()
            {
                new() { Id = 1, SupervisorId = null },
                new() { Id = 2, SupervisorId = 1 },
                new() { Id = 3, SupervisorId = 1 },
                new() { Id = 4, SupervisorId = 3 },
            };

            transferDtos = new()
            {
                new() { From = 2, Amount = 100 },
                new() { From = 3, Amount = 50 },
                new() { From = 4, Amount = 100 },
                new() { From = 4, Amount = 200 },
            };

            var res = commissionSystem.CalculateStats(memberDtos, transferDtos).ToList();

            for (int i = 0; i < res.Count; i++)
                Assert.That(res[i], Is.EqualTo(expectedResult[i]));
        }

        [Test]
        public void Test4()
        {
            var commissionSystem = new CommissionSystem();
            var expectedResult = new List<string>()
            {
                "1 0 15 1819",
                "2 1 7 33",
                "3 2 0 0",
                "4 2 6 29",
                "5 3 0 0",
                "6 3 0 0",
                "7 3 4 6",
                "8 4 0 0",
                "9 4 0 0",
                "10 4 0 0",
                "11 4 0 0",
                "12 1 8 646",
                "13 2 0 0",
                "14 2 1 0",
                "15 3 0 0",
                "16 2 6 169",
                "17 3 0 0",
                "18 3 3 22",
                "19 4 0 0",
                "20 4 0 0",
                "21 4 0 0",
                "22 3 2 0",
                "23 4 0 0",
                "24 4 0 0",
            };

            List<MemberDto> memberDtos;
            List<TransferDto> transferDtos;

            memberDtos = new()
            {
                new() { Id = 1, SupervisorId = null },
                new() { Id = 2, SupervisorId = 1 },
                new() { Id = 3, SupervisorId = 2 },
                new() { Id = 4, SupervisorId = 2 },
                new() { Id = 5, SupervisorId = 4 },
                new() { Id = 6, SupervisorId = 4 },
                new() { Id = 7, SupervisorId = 4 },
                new() { Id = 8, SupervisorId = 7 },
                new() { Id = 9, SupervisorId = 7 },
                new() { Id = 10, SupervisorId = 7 },
                new() { Id = 11, SupervisorId = 7 },
                new() { Id = 12, SupervisorId = 1 },
                new() { Id = 13, SupervisorId = 12 },
                new() { Id = 14, SupervisorId = 12 },
                new() { Id = 15, SupervisorId = 14 },
                new() { Id = 16, SupervisorId = 12 },
                new() { Id = 17, SupervisorId = 16 },
                new() { Id = 18, SupervisorId = 16 },
                new() { Id = 19, SupervisorId = 18 },
                new() { Id = 20, SupervisorId = 18 },
                new() { Id = 21, SupervisorId = 18 },
                new() { Id = 22, SupervisorId = 16 },
                new() { Id = 23, SupervisorId = 22 },
                new() { Id = 24, SupervisorId = 22 },
            };

            transferDtos = new()
            {
                new() { From = 6, Amount = 100 },
                new() { From = 10, Amount = 33 },
                new() { From = 20, Amount = 171 },
                new() { From = 13, Amount = 912 },
                new() { From = 22, Amount = 78 },
                new() { From = 2, Amount = 7 },
                new() { From = 11, Amount = 1 },
                new() { From = 18, Amount = 512 },
                new() { From = 1, Amount = 910 },
            };

            var res = commissionSystem.CalculateStats(memberDtos, transferDtos).ToList();

            for (int i = 0; i < res.Count; i++)
                Assert.That(res[i], Is.EqualTo(expectedResult[i]));
        }

        [Test]
        public void AverageCaseSpeedTest()
        {
            var commissionSystem = new CommissionSystem();

            commissionSystem.CalculateStats(GetMemberDtos(2, 1 * (int)10e6), GetTransferDtos(1 * (int)10e6));

            static IEnumerable<MemberDto> GetMemberDtos(int childByParent, int count)
            {
                yield return new() { Id = 0, SupervisorId = null };

                for (int i = 1; i <= count; i++)
                    yield return new() { Id = i, SupervisorId = (i - 1) / childByParent };
            }

            static IEnumerable<TransferDto> GetTransferDtos(int count)
            {
                var rand = new Random();

                for (var i = 0; i <= count; i++)
                    yield return new() { From = rand.Next(0, i + 1), Amount = rand.Next(1000) };
            }
        }

        [Test]
        public void WorstCaseSpeedTest()
        {
            var commissionSystem = new CommissionSystem();

            commissionSystem.CalculateStats(GetMemberDtos(1 * (int)10e3), GetTransferDtos(1 * (int)10e3));


            static IEnumerable<MemberDto> GetMemberDtos(int count)
            {
                yield return new() { Id = 0, SupervisorId = null };

                for (int i = 1; i <= count; i++)
                    yield return new() { Id = i, SupervisorId = i - 1 };
            }

            static IEnumerable<TransferDto> GetTransferDtos(int count)
            {
                var rand = new Random();

                for (var i = 0; i <= count; i++)
                    yield return new() { From = rand.Next(0, i + 1), Amount = rand.Next(1000) };
            }
        }
    }
}