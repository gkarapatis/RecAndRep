using RecAndRep.BOL.Model;
using RecAndRep.DAL;
using System.Collections.Generic;
using System.Linq;

namespace RecAndRep.BOL
{
    public class ClientRepository : Repository<Client>
    {
        public ClientRepository() : base("Client")
        {
        }

    }
}
