using RecAndRep.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecAndRep.BOL.Model
{
    public class Client: IIdInterface
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
