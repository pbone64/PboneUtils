using PboneLib.CustomLoading.Content.Implementations.Misc;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace PboneUtils
{
    public sealed class FeatureConfig : PConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public override string GetName() => "Feature Config";
        public static FeatureConfig Instance => ModContent.GetInstance<FeatureConfig>();

        // ------------------------------------------------------------------------------------------
        [Header("$Mods.PboneUtils.Config.Header.ItemToggles")]

        [Label("$Mods.PboneUtils.Config.Option.ItemToggle_EndlessLiquidItems.Label")]
        [Tooltip("$Mods.PboneUtils.Config.Option.ItemToggle_EndlessLiquidItems.Tooltip")]
        [DefaultValue(true)]
        public bool ItemToggle_EndlessLiquidItems;
    }
}
