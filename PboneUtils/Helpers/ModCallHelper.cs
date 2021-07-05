using System;
using System.Collections.Generic;

namespace PboneUtils.Helpers
{
    public static class ModCallHelper
    {
        public static void AssertArgs(List<object> args, params Type[] types)
        {
            for (int i = 0; i < args.Count; i++)
            {
                Type t = args[i].GetType();
                if (!types[i].IsAssignableFrom(t))
                {
                    throw new ArgumentException($"Incorrect arg typed passed into Mod.Call: Expected {types[i].Name}, got {t.Name}.");
                }
            }
        }
    }
}
