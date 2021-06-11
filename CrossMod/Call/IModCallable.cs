using System.Collections.Generic;

namespace PboneUtils.CrossMod.Call
{
    internal interface IModCallable
    {
        string[] GetMessagesICanHandle();
        
        object Call(string message, List<object> args);
    }
}
