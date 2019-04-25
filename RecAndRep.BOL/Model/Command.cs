using RecAndRep.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecAndRep.BOL.Model
{
    public class Command: IIdInterface
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
