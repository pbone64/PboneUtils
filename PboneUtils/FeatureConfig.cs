using PboneLib.CustomLoading.Content.Implementations.Misc;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace PboneUtils
{
    public sealed class FeatureConfig : PConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static FeatureConfig Instance => ModContent.GetInstance<FeatureConfig>();

        // ------------------------------------------------------------------------------------------
        [Header("$Mods.PboneUtils.Config.Header.ItemToggles")]

        [Label("$Mods.PboneUtils.Config.Option.ItemToggle_LiquidItems.Label")]
        [Tooltip("$Mods.PboneUtils.Config.Option.ItemToggle_LiquidItems.Tooltip")]
        [DefaultValue(true)]
        public bool ItemToggle_LiquidItems;
    }
}
