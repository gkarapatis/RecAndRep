using RecAndRep.Client.Business.ActionResolver.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecAndRep.Client.Business.ActionResolver.Abstractions
{
    public interface IActionResolver
    {
        ActionResponse Parse(string input);
    }
}
