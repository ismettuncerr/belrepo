using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bel.DataLayer.Interfaces
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        void Edit(Contact contact);
    }
}
