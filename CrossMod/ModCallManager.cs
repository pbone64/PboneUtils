using PboneUtils.CrossMod.Content;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PboneUtils.CrossMod
{
    internal class ModCallManager
    {
        private Dictionary<Type, IModCallable> ModCallHandlersByType;
        private Dictionary<string, Type> ModCallHandlerTypesByMessage;

        internal void Load()
        {
            ModCallHandlersByType = new Dictionary<Type, IModCallable>();
            LoadHandlers();

            ModCallHandlerTypesByMessage = new Dictionary<string, Type>();
            LoadHandlersByMessage();
        }

        private void LoadHandlers()
        {
            ModCallHandlersByType.Add(typeof(MysteriousTraderShopInterface), new MysteriousTraderShopInterface());
        }

        private void LoadHandlersByMessage()
        {
            string[] messages;

            foreach (KeyValuePair<Type, IModCallable> handler in ModCallHandlersByType)
            {
                messages = handler.Value.GetMessagesICanHandle();

                foreach (string s in messages)
                {
                    ModCallHandlerTypesByMessage.Add(s, handler.Key);
                }
            }
        }

        internal object HandleCall(object[] args)
        {
            ParseArgs(args, out string message, out List<object> parsedArgs);

            IModCallable handler = ModCallHandlersByType[ModCallHandlerTypesByMessage[message]];

            return handler.Call(message, parsedArgs);
        }

        internal void ParseArgs(object[] args, out string message, out List<object> parsedArgs)
        {
            if (!(args[0] is string s))
            {
                throw new ArgumentException("The first parameter of Mod.Call must be a string for all PboneUtils calls.");
            }
            else
            {
                message = s;
                parsedArgs = args.ToList();
                parsedArgs.RemoveAt(0);
            }
        }

        public IModCallable GetCallHandlerFromType(Type t) => ModCallHandlersByType[t];
    }
}
