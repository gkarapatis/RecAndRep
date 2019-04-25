using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using RecAndRep.Client.Business.ActionResolver.Model;

namespace RecAndRep.Client.Business.ActionResolver
{
    public class Executor
    {
        private readonly ILog log;
        private object result;

        private object Instance { get; }

        private Dictionary<string, MethodInfo> InfoMethods { get; }

        public Executor(object instance, Dictionary<string, MethodInfo> infoMethods)
        {
            Instance = instance;
            InfoMethods = infoMethods;
            log = LogManager.GetLogger($"Executor: {Instance.GetType()}");
        }

        public ActionResponse Execute(string actionName, string[] parameters)
        {
            if (!InfoMethods.ContainsKey(actionName))
            {
                log.Warn($"Invalid Action {actionName}");
                return new ActionResponse()
                {
                    Succeeded=false,
                    ErrorMessage = "Invalid Action"
                };
            }

            var m = InfoMethods[actionName];

            if (m.GetParameters().Length != parameters.Length)
            {
                log.Warn($"Invalid number of parameters for Action :{actionName} ({m.GetParameters().Length} requered)");
                return new ActionResponse()
                {
                    Succeeded = false,
                    ErrorMessage = "Invalid number of parameters"
                };
            }

            object[] methodParameters = new object[parameters.Length];

            var i = 0;
            foreach (var parameterInfo in m.GetParameters())
            {
                try
                {
                    methodParameters[i] = Convert.ChangeType(parameters[i], parameterInfo.ParameterType);
                }
                catch(InvalidCastException e)
                {
                    log.Error($"An error occured trying to cast {parameters[i]} to {parameterInfo.ParameterType} for Action {actionName} ({e.Message})");
                    return new ActionResponse()
                    {
                        Succeeded = false,
                        ErrorMessage = "Invalid Cast Exception"
                    };
                }
                catch(Exception e)
                {
                    log.Error($"An error occured trying to cast {parameters[i]} to {parameterInfo.ParameterType} for Action {actionName} ({e.Message})");
                    return new ActionResponse()
                    {
                        Succeeded = false,
                        ErrorMessage = "Runtime Error"
                    };
                }
                i++;
            }
            return (ActionResponse)m.Invoke(Instance, methodParameters);           
        }






    }
}
