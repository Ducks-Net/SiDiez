using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DucksNet.Domain.Model
{
    public class Office
    {
        public Guid ID { get; private set; }
        public Guid BusinessId { get; set; }
        public String Address {get; private set;}
        public int AnimalCapacity {get; set;}

        private Office(Guid _BusinessId, String _Address,  int _AnimalCapacity) 
        {
            ID = new Guid();
            BusinessId = _BusinessId;
            Address = _Address;
            AnimalCapacity = _AnimalCapacity;
        }
    }
}
