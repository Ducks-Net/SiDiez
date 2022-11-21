using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DucksNet.Domain.Model
{
    public class Business
    {
        public Guid ID { get; private set; }
        public String BusinessName { get; private set; }
        public String Surname {get; private set;}
        public String FirstName {get; private set;}
        public String Address {get; private set;}
        public String OwnerPhone {get; private set;}
        public String OwnerEmail {get; private set;}

        private Business(String _BusinessName, String _Surname, String _FirstName, String _Address, String _OwnerPhone, String _OwnerEmail) 
        {
            ID = new Guid();
            BusinessName = _BusinessName;
            Surname = _Surname;
            FirstName = _FirstName;
            Address = _Address;
            OwnerPhone = _OwnerPhone;
            OwnerEmail = _OwnerEmail;
        }
    }
}
