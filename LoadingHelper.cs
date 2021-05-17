using System;
using System.Reflection;
using Terraria.ModLoader;

namespace PboneUtils
{
    public static class LoadingHelper
    {
        public static object UILoadModsInstance;
        public static Type UILoadMods;

        public static FieldInfo StageTextFIeld;
        public static MethodInfo SetLoadStageMethod;
        public static PropertyInfo SubProgressTextProperty;
        public static PropertyInfo ProgressProperty;

        public static void Load()
        {
            Assembly modLoader = typeof(ModLoader).Assembly;

            UILoadModsInstance = modLoader.GetType("Terraria.ModLoader.UI.Interface").GetField("loadMods", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
            UILoadMods = modLoader.GetType("Terraria.ModLoader.UI.UILoadMods");

            StageTextFIeld = UILoadMods.GetField("stageText", BindingFlags.NonPublic | BindingFlags.Instance);
            SetLoadStageMethod = UILoadMods.GetMethod("SetLoadStage", BindingFlags.Public | BindingFlags.Instance);
            SubProgressTextProperty = UILoadMods.GetProperty("SubProgressText", BindingFlags.Public | BindingFlags.Instance);
            ProgressProperty = UILoadMods.GetProperty("Progress", BindingFlags.Public | BindingFlags.Instance);
        }

        public static void SetLoadStage(string text)
        {
            SetLoadStageMethod.Invoke(UILoadModsInstance, new object[] { text, -1 });
        }

        public static void SetSubText(string text)
        {
            SubProgressTextProperty.SetValue(UILoadModsInstance, text);
        }

        public static void SetProgress(float progress)
        {
            ProgressProperty.SetValue(UILoadModsInstance, progress);
        }

        public static string GetLoadStage() => (string)StageTextFIeld.GetValue(UILoadModsInstance);

        public static void Unload()
        {
            UILoadModsInstance = null;
            UILoadMods = null;

            StageTextFIeld = null;
            SetLoadStageMethod = null;
            SubProgressTextProperty = null;
            ProgressProperty = null;
        }
    }
}
