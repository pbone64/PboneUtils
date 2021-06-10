using System.Collections.Generic;

namespace PboneUtils.CrossMod
{
    internal interface IModCallable
    {
        string[] GetMessagesICanHandle();
        
        object Call(string message, List<object> args);
    }
}
