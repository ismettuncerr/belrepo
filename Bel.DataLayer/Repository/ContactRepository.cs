using Bel.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Repository
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        private readonly beldatabaseEntities context;
        public ContactRepository(beldatabaseEntities context)
        {
            this.context = context;
        }
        public void Edit(Contact contact)
        {
            var result = context.Set<Contact>().FirstOrDefault();
            result.Address = contact.Address;
            result.CityState = contact.CityState;
            result.Email = contact.Email;
            result.EMailDescription = contact.EMailDescription;
            result.Phone = contact.Phone;
            result.PhoneDescription = contact.PhoneDescription;
            context.SaveChanges();
        }
    }
}
