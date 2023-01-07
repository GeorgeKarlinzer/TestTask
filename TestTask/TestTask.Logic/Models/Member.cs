using System.Collections.Generic;
using System.Linq;

namespace TestTask.Logic.Models
{
    internal class Member
    {
        public int Id { get; }
        public Member Supervisor { get; set; }
        public int Level => Supervisor?.Level + 1 ?? 0;
        public int SubordinatesNumber { get; set; }
        public int TotalCommission { get; private set; }

        public Member(int id)
        {
            Id = id;
            TotalCommission = 0;
        }

        public void ProcessTransfer(int amount)
        {
            /* Założyciel nie ma przełożonego i otrzymuje całą wpłaconą kwotę w postaci prowizji
             */
            if (Supervisor is null)
            {
                TotalCommission += amount;
                return;
            }

            var hierarchy = new Stack<Member>();
            var sup = Supervisor;

            /* Wkładamy kolejno wszystkich przełożonych do stosu
             */
            while (sup is not null)
            {
                hierarchy.Push(sup);
                sup = sup.Supervisor;
            }

            /* Wyciągamy kolejno przełożonych ze stosu, zaczynając od założyciela.
             * Jeżeli przełożony nie jest bezpośredni dla uczestnika, to do prowizji dodajemy połowę pozostałem kwoty.
             * W przeciwnym przypadku, dodajemy całą pozostałą kwotę.
             * 
             * Złożoność obliczeniowa to O(k), gdzie k - ilość przełożonych uczestnika
             */
            while (hierarchy.Any())
            {
                var mem = hierarchy.Pop();
                var commission = mem == Supervisor ? amount : amount / 2;
                mem.TotalCommission += commission;
                amount -= commission;
            }
        }
    }
}
