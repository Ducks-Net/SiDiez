using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DucksNet.Domain.Model
{
    public class Business
    {
        public Guid ID { get; private set; }
        public string BusinessName { get; private set; }
        public string Surname {get; private set;}
        public string FirstName {get; private set;}
        public string Address {get; private set;}
        public string OwnerPhone {get; private set;}
        public string OwnerEmail {get; private set;}

        public Business(string _BusinessName, string _Surname, string _FirstName, string _Address, string _OwnerPhone, string _OwnerEmail) 
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
