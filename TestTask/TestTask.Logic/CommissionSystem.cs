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
            var membersMap = new Dictionary<int, Member>();

            // O(n)
            foreach (var dto in memberDtos)
            {
                var member = membersMap[dto.Id] = new(dto.Id);

                if (dto.SupervisorId is not null)
                {
                    member.Supervisor = membersMap[(int)dto.SupervisorId];
                    member.Level = member.Supervisor.Level + 1;
                }
            }
            
            // O(n)
            foreach (var dto in memberDtos.Reverse())
            {
                if (dto.SupervisorId is null)
                    continue;

                var mem = membersMap[dto.Id];
                var subNum = mem.SubordinatesNumber;
                mem.Supervisor.SubordinatesNumber += subNum == 0 ? 1 : subNum;
            }

            // O(n * m)
            foreach (var dto in transferDtos)
                membersMap[dto.From].ProcessTransfer(dto.Amount);

            // O(nlogn)
            var result = membersMap.Values.OrderBy(x => x.Id)
                .Select(x => $"{x.Id} {x.Level} {x.SubordinatesNumber} {x.TotalCommission}");

            return result;
        }
    }
}
