using System.Collections.Generic;
using System.Data;
using System.Linq;
using TestTask.Logic.Dtos;
using TestTask.Logic.Models;

namespace TestTask.Logic
{
    public class CommissionSystem
    {
        public IEnumerable<string> CalculateStats(IEnumerable<MemberDto> memberDtos, IEnumerable<TransferDto> transferDtos)
        {
            /* MemberDtos (dane wejściowe) używamy, do tworzenia obiektów typu Member.
             * Te obiekty zapisujemy do HashTable, co się przyda później.
             */
            var membersMap = new Dictionary<int, Member>();

            /* Iterując MemberDtos możemy stworzyć obiekt Member, dodać go do membersMap oraz przypisać wartość Supervisor.
             * 
             * Korzystamy tutaj z faktu, że dziecięcy element nie może się pojawić wcześniej niż rodzic (dzięki strukturze pliku xml).
             * Ten fakt przyda się również później.
             * 
             * Złożoność obliczeniowa: O(n), gdzie n - liczba uczestników.
             */
            foreach (var dto in memberDtos)
            {
                var member = membersMap[dto.Id] = new(dto.Id);

                if (dto.SupervisorId is not null)
                    member.Supervisor = membersMap[(int)dto.SupervisorId];
            }

            /* W tym miejscu również dziękujemy strukturze xml,
             * Ponieważ umożliwia ona obliczanie podwładnych nie mających swoich podwładnych dla każdego uczestnika w O(n).
             * 
             * Odwracamy dane wejściowe, więc mamy przeciwną sytuację niż w kroku wyżej,
             * Czyli rodzić nie może się pojawić wcześniej niż wszystkie jego dzieci.
             * 
             * Jeżeli uczestnik nie ma dzieci (SubordinatesNumber = 0), to inkrementujemy liczbę podwładnych jego rodzica,
             * W przeciwnym przypadku, dodajemy do liczby podwładnych rodzica liczbę podwładnych danego uczestnika.
             * 
             * W tym miejscu nam się przydaje membersMap,
             * Pozwala to na wyciąganie Member po Id w czasie O(1),
             * Co pozwala uzyskać całkowity czas O(n)
             */
            foreach (var dto in memberDtos.Reverse())
            {
                if (dto.SupervisorId is null)
                    continue;

                var mem = membersMap[dto.Id];
                var subNum = mem.SubordinatesNumber;
                mem.Supervisor.SubordinatesNumber += subNum == 0 ? 1 : subNum;
            }

            /* W kolejnym kroku chcemy obliczyć prowizję, dla każdego uczestnika.
             * Jest to wąskie gardło naszego programu,
             * Gdyż złożoność obliczeniowa to O(k*m), gdzie m - ilość przelewów, k - wysokość drzewa uczestników.
             * Jest tak, ponieważ dla każdego przelewu (O(m)) musimy wykonać operację ProcessTransfer (O(k), uzasadnienie tej złożoności jest w opisie ProcessTranser)
             * 
             * W najgorszym przypadku (każdy uczestnik ma jednego podwładnego), czyli k = n, co zwiększa czas do O(n*m),
             * Ale średnio, k będzie się równać logn i w tym przypadku mamy złożoność logliniową: O(m*logn)
             * 
             * P.S. W zasadzie złożoność obliczeniowa jest O(k*m*p), gdzie O(p) to złożoność operacji wyciągnięcia Member po Id,
             * Ale w naszym przypadku, dzięki membersMap, O(p) = O(1)
             */
            foreach (var dto in transferDtos)
                membersMap[dto.From].ProcessTransfer(dto.Amount);

            /* Finalnym krokiem jest sortowanie uczestników po Id i zwracanie odpowiedniego wyniku
             * Złożoność obliczeniowa zależy od złożoności operacji OrderBy, która używa QuickSort, czyli ma złożoność O(nlogn)
             * 
             * Ewentualnie można korzystać z RadixSort albo CountingSort (w przypadku, gdy max(Id) - max(Id) ≈ n).
             * Jest to możliwe, bo Id jest liczbą naturalną.
             * 
             * Te algorytmy pozwalają uzyskać złożoność czasową O(n),
             * Ale również potrzebują więcej miejsca (O(n+2^d)/O(n+r) w porównaniu do O(logn) dla QuickSort)
             * Oraz mogą mieć duże stałe, nieuwzględnione we wzorze.
             */
            var result = membersMap.Values.OrderBy(x => x.Id)
                .Select(x => $"{x.Id} {x.Level} {x.SubordinatesNumber} {x.TotalCommission}");

            return result;
        }
    }
}
