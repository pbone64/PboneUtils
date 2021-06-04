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

        // MISC FEATURES

        [Header("$Mods.PboneUtils.Config.Header.MiscFeatures")]
        [Label("$Mods.PboneUtils.Config.Label.ExtraBuffSlots")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.ExtraBuffSlots")]
        [Slider]
        [Range(0, 66)]
        [Increment(11)]
        [DefaultValue(22)]
        [DrawTicks]
        [ReloadRequired]
        public int ExtraBuffSlots;

        [Label("$Mods.PboneUtils.Config.Label.EndlessPotions")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.EndlessPotions")]
        [DefaultValue(true)]
        public bool EndlessPotions;

        [Label("$Mods.PboneUtils.Config.Label.EndlessPotionsSlider")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.EndlessPotionsSlider")]
        [Slider]
        [Range(1, 100)]
        [Increment(1)]
        [DefaultValue(30)]
        public int EndlessPotionsSlider;

        [Label("$Mods.PboneUtils.Config.Label.AutoswingOnEverything")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.AutoswingOnEverything")]
        [DefaultValue(true)]
        public bool AutoswingOnEverything;

        [Label("$Mods.PboneUtils.Config.Label.AutoswingOnEverythingBlacklist")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.AutoswingOnEverythingBlacklist")]
        public List<ItemDefinition> AutoswingOnEverythingBlacklist;

        [Label("$Mods.PboneUtils.Config.Label.AverageBossBags")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.AverageBossBags")]
        [DefaultValue(true)]
        public bool AverageBossBags;

        [Label("$Mods.PboneUtils.Config.Label.MaxStackIncrease")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.MaxStackIncrease")]
        [DefaultValue(true)]
        public bool MaxStackIncrease;


        // ITEMS

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

        [Label("$Mods.PboneUtils.Config.Label.AmmoItemsToggle")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.AmmoItemsToggle")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool AmmoItemsToggle;

        [Label("$Mods.PboneUtils.Config.Label.ArenaItemsToggle")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.ArenaItemsToggle")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool ArenaItemsToggle;

        [Label("$Mods.PboneUtils.Config.Label.MagnetItemsToggle")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.MagnetItemsToggle")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool MagnetItemsToggle;


        // MISC SINGLE ITEMS

        [Header("$Mods.PboneUtils.Config.Header.MiscItemToggles")]
        [Label("$Mods.PboneUtils.Config.Label.VoidPiggyToggle")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.VoidPiggyToggle")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool VoidPiggyToggle;

        [Label("$Mods.PboneUtils.Config.Label.PhilosophersStoneToggle")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.PhilosophersStoneToggle")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool PhilosophersStoneToggle;

        [Label("$Mods.PboneUtils.Config.Label.SpawnRateItemsToggle")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.SpawnRateItemsToggle")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool SpawnRateItemsToggle;

        [Label("$Mods.PboneUtils.Config.Label.GreaterStaffOfRegrowthToggle")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.GreaterStaffOfRegrowthToggle")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool GreaterStaffOfRegrowthToggle;

        [Label("$Mods.PboneUtils.Config.Label.ShadowPearlToggle")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.ShadowPearlToggle")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool ShadowPearlToggle;
    }
}
