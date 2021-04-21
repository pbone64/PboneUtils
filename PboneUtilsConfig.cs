using System.ComponentModel;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace PboneUtils
{
    public class PboneUtilsConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static PboneUtilsConfig Instance => ModContent.GetInstance<PboneUtilsConfig>();

        [Header("$Mods.PboneUtils.Config.Header.MiscFeatures")]
        [Label("$Mods.PboneUtils.Config.Label.AutoswingOnEverything")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.AutoswingOnEverything")]
        [DefaultValue(false)]
        public bool AutoswingOnEverything;

        [Label("$Mods.PboneUtils.Config.Label.AutoswingOnEverythingBlacklist")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.AutoswingOnEverythingBlacklist")]
        public List<ItemDefinition> AutoswingOnEverythingBlacklist;

        [Header("$Mods.PboneUtils.Config.Header.ItemToggles")]
        [Label("$Mods.PboneUtils.Config.Label.StorageItemsToggle")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.StorageItemsToggle")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool StorageItemsToggle;

        [Label("$Mods.PboneUtils.Config.Label.LiquidItemsToggle")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.LiquidItemsToggle")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool LiquidItemsToggle;

        [Label("$Mods.PboneUtils.Config.Label.MagnetItemsToggle")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.MagnetItemsToggle")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool MagnetItemsToggle;

        [Header("$Mods.PboneUtils.Config.Header.MiscItemToggles")]
        [Label("$Mods.PboneUtils.Config.Label.VoidPiggyToggle")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.VoidPiggyToggle")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool VoidPiggyToggle;

        [Header("$Mods.PboneUtils.Config.Header.PhilosophersStone")]
        [Label("$Mods.PboneUtils.Config.Label.PhilosophersStone")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.PhilosophersStone")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool PhilosophersStone;
    }
}
