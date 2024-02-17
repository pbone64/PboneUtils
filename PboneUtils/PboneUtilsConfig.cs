using System.ComponentModel;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using PboneLib.CustomLoading.Content.Implementations.Misc;

namespace PboneUtils
{
    [ExtendsFromMod("PboneLib")]
    public class PboneUtilsConfig : PConfig
    {
        public override string GetName() => "Feature Config";
        public override ConfigScope Mode => ConfigScope.ServerSide;

        public static PboneUtilsConfig Instance => ModContent.GetInstance<PboneUtilsConfig>();

        // MISC FEATURES

        [Header("MiscFeatures")]
        /*[Label("$Mods.PboneUtils.Config.Label.ExtraBuffSlots")]
        [Tooltip("$Mods.PboneUtils.Config.Tooltip.ExtraBuffSlots")]
        [Slider]
        [Range(0, 66)]
        [Increment(11)]
        [DefaultValue(22)]
        [DrawTicks]
        [ReloadRequired]
        public int ExtraBuffSlots;
        */

        [DefaultValue(true)]
        public bool EndlessPotions;

        [Slider]
        [Range(1, 100)]
        [Increment(1)]
        [DefaultValue(30)]
        public int EndlessPotionsSlider;

        [DefaultValue(false)]
        public bool AutoswingOnEverything;

        public List<ItemDefinition> AutoswingOnEverythingBlacklist;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool AverageBossBags;

        [Slider]
        [Range(100, 500)]
        [Increment(50)]
        [DefaultValue(100)]
        [DrawTicks]
        [ReloadRequired]
        public int AverageBossBagsSlider;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool MaxStackIncrease;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool NoMoreGraves;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool FastRespawn;

        [DefaultValue(false)]
        [ReloadRequired]
        public bool FastRespawnDuringBoss;


        // ITEMS

        [Header("ItemToggles")]

        [DefaultValue(true)]
        [ReloadRequired]
        public bool StorageItemsToggle;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool LiquidItemsToggle;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool AmmoItemsToggle;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool ArenaItemsToggle;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool MagnetItemsToggle;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool WorldControlItemsToggle;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool CellPhoneAppsToggle;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool EndlessBaitToggle;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool CloversToggle;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool BuildingItemToggle;


        // ITEM SETTINGS

        [Header("ItemSettings")]

        [Slider]
        [Range(0f, 1f)]
        [Increment(0.05f)]
        [DefaultValue(1f)]
        [DrawTicks]
        [ReloadRequired]
        public float GoldenCloverLuckSlider;


        // MISC SINGLE ITEMS

        [Header("MiscItemToggles")]

        [DefaultValue(true)]
        [ReloadRequired]
        public bool VoidPiggyToggle;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool PhilosophersStoneToggle;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool SpawnRateItemsToggle;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool GreaterStaffOfRegrowthToggle;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool ShadowPearlToggle;

        [DefaultValue(false)]
        [ReloadRequired]
        public bool InfiniteManaToggle;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool StarMagnetToggle;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool GreedyChestToggle;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool MagicLightToggle;

        // NPCS

        [Header("NPCs")]

        [DefaultValue(true)]
        [ReloadRequired]
        public bool MysteriousTrader;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool Miner;


        // QOL RECIPES

        [Header("QolRecipes")]

        [DefaultValue(true)]
        [ReloadRequired]
        public bool RecipeEndlessWater;

        [DefaultValue(false)]
        [ReloadRequired]
        public bool RecipePetrifiedSafe;
    }
}
