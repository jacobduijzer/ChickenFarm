using System.Collections.Generic;

namespace ChickenFarm.Shared
{
    public record FarmDto(int FarmId, List<ShedDto> Sheds);
}