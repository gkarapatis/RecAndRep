using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecAndRep.BOL;
using RecAndRep.BOL.Model;

namespace RecAndRep.Server
{
    class InitializeCommands
    {
        private CommandRepository _repository;

        public InitializeCommands(CommandRepository repository)
        {
            _repository = repository;
        }

        public void Initialize()
        {
            if (!_repository.Get().Any())
            {
                _repository.Add(new Command { Description = "window-select notepad" });
                _repository.Add(new Command { Description = "mouse-move 150;150" });
                _repository.Add(new Command { Description = "mouse-click Single" });
                _repository.Add(new Command { Description = "keyboard-sendkeys Hello World!!!" });
            }
        }
    }
}
