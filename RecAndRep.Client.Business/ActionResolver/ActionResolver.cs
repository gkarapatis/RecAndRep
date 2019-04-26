using log4net;
using RecAndRep.Client.Business.ActionResolver.Abstractions;
using RecAndRep.Extensible.Model.Model;
using RecAndRep.Extensible.Model.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RecAndRep.Client.Business.ActionResolver
{
    public class ActionResolver : IActionResolver
    {
        public static ActionResolver Instance = new ActionResolver();

        private readonly ILog log = LogManager.GetLogger($"Interpreter");
        private Dictionary<string, Executor> executorDict = new Dictionary<string, Executor>();


        public ActionResolver()
        {
            LookForOperatorsIn(Assembly.GetExecutingAssembly());
            LookForOperatorsIn(Path.Combine(Environment.CurrentDirectory, "../../Extensions"), watch: true);
        }

        public ActionResponse Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                log.Warn($"Operator did not defined.");
                return new ActionResponse()
                {
                    Succeeded = false,
                    ErrorMessage = "Operator did not defined"
                };
            }
            var oper_action = input?.Split(new[] { '-' }, 2);
            var operatorName = oper_action[0].Trim().ToLower();

            if (executorDict.ContainsKey(operatorName) && oper_action.Length > 1)
            {                
                var action_params = oper_action[1].Split(new[] { ' ' }, 2);
                var actionName = action_params[0].Trim().ToLower();
                var parameters = action_params.Length > 1 ? action_params[1].Split(new[] { ';' }) : null;
                var response = executorDict[operatorName].Execute(actionName, parameters);
                return response;

            }
            log.Warn($"Operator not found {input}");
            return new ActionResponse()
            {
                Succeeded = false,
                ErrorMessage = $"Operator not found ({input})"
            };
        }



        private void LookForOperatorsIn(string path, bool watch)
        {
            foreach (var dll in Directory.EnumerateFiles(path, "*.dll"))
            {
                LookForOperatorsIn(Assembly.LoadFrom(dll));
            }
        }

        private void LookForOperatorsIn(Assembly asm)
        {
            foreach (Type t in asm.GetTypes())
            {
                if (t.IsDefined(typeof(OperatorAttribute)))
                {
                    try
                    {
                        OperatorAttribute attr = t.GetCustomAttribute<OperatorAttribute>();

                        var instance = Activator.CreateInstance(t);

                        var infoMethods = t.GetMethods().
                            Where(y => y.GetCustomAttributes().OfType<ActionAttribute>().Any() && y.ReturnType == typeof(ActionResponse)).
                            ToDictionary(x => x.GetCustomAttribute<ActionAttribute>().Name.ToLower(), x => x);
                        
                        executorDict.Add(attr.Name.ToLower(), new Executor(instance, infoMethods));
                    }
                    catch (Exception e)
                    {
                        log.Error($"Exceptio while initializing the Interpreter Instance{e.Message}");
                    }
                }
            }
        }

    }
}
