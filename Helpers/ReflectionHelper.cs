using System;
using System.Reflection;

namespace PboneUtils.Helpers
{
    /// <summary>
    ///     Container of various helpful methods and extensions of reflection-related types.
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        ///     Encompasses access modifiers and static-instance status.
        /// </summary>
        public static BindingFlags AccessFlags =
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;

        /// <summary>
        ///     Attempts to retrieve a method regardless of access-required flags.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static MethodInfo GetMethodForced(this Type type, string method) => type.GetMethod(method, AccessFlags);

        public static TFieldType GetField<TType, TFieldType>(this TType obj, string field) =>
            (TFieldType) typeof(TType).GetField(field, AccessFlags)?.GetValue(obj);

        public static void SetField<TType, TFieldType>(this TType obj, string field, TFieldType value) =>
            typeof(TType).GetField(field, AccessFlags)?.SetValue(obj, value);
    }
}