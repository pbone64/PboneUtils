using System;
using System.Collections.Generic;
using System.Linq;

namespace PboneUtils.CrossMod.Call
{
    public abstract class SimpleModCallHandler : IModCallable
    {
        protected Dictionary<string, Func<List<object>, object>> CallFunctions;

        protected SimpleModCallHandler()
        {
            CallFunctions = new Dictionary<string, Func<List<object>, object>>();
        }

        public string[] GetMessagesICanHandle() => CallFunctions.Keys.ToArray();

        public object Call(string message, List<object> args) => CallFunctions[message](args);
    }
}
