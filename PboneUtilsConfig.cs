using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace PboneUtils
{
    public class PboneUtilsConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static PboneUtilsConfig Instance => ModContent.GetInstance<PboneUtilsConfig>();

        [Header("$Mods.PboneUtils.Config.Header.ItemToggles")]
        [Label("$Mods.PboneUtils.Config.Label.StorageItemsToggle")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool StorageItemsToggle;

        [Label("$Mods.PboneUtils.Config.Label.LiquidItemsToggle")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool LiquidItemsToggle;

        [Label("$Mods.PboneUtils.Config.Label.MagnetItemsToggle")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool MagnetItemsToggle;

        [Header("$Mods.PboneUtils.Config.Header.MiscItemToggles")]
        [Label("$Mods.PboneUtils.Config.Label.VoidPiggyToggle")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool VoidPiggyToggle;

        [Header("$Mods.PboneUtils.Config.Label.PhilosophersStone")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool PhilosophersStone;
    }
}
