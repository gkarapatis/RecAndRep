using RecAndRep.BOL.Model;
using RecAndRep.DAL;
using System.Collections.Generic;
using System.Linq;

namespace RecAndRep.BOL
{
    public class CommandRepository : Repository<Command>
    {
        public CommandRepository() : base("Command")
        {
        }

    }
}
