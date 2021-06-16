using System.Collections.Generic;

namespace ChickenFarm.Shared
{
    public class Farm : EntityBase
    {
        public ICollection<Shed> Sheds { get; set; }
    }
}