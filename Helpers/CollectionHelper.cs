using System.Collections.Generic;
using System.Linq;

namespace PboneUtils.Helpers
{
    public static class CollectionHelper
    {
        public static List<(T, string)> FromArray<T>(T[] items, string key) => items.Select(t => (t, key)).ToList();
    }
}
