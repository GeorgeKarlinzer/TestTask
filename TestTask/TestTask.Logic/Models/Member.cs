using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace TestTask.Logic.Models
{
    internal class Member
    {
        public int Id { get; }
        public Member Supervisor { get; set; }
        public int Level { get; set; }
        public int TotalCommission { get; private set; }
        public int SubordinatesNumber { get; set; }

        public Member(int id)
        {
            Id = id;
            Level = 0;
            TotalCommission = 0;
        }

        public void ProcessTransfer(int amount)
        {
            if(Supervisor is null)
            {
                TotalCommission += amount;
                return;
            }

            var hierarchy = new Stack<Member>();
            var sup = Supervisor;

            while(sup is not null)
            {
                hierarchy.Push(sup);
                sup = sup.Supervisor;
            }

            while(hierarchy.Any())
            {
                var mem = hierarchy.Pop();
                var commission = mem == Supervisor ? amount : amount / 2;
                mem.TotalCommission += commission;
                amount -= commission;
            }
        }
    }
}
