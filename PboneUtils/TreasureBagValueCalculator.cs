using PboneLib.CustomLoading.Content.Implementations;
using PboneLib.Utils;
using PboneUtils.Helpers;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils
{
    public class TreasureBagValueCalculator : PSystem
    {
        public struct TreasureBagOpeningInfo
        {
            public List<int> RealValues;

            public TreasureBagOpeningInfo()
            {
                RealValues = new List<int>();
            }

            public float GetAverageValue()
            {
                float value = 0;

                foreach (int v in RealValues)
                {
                    value += v;
                }

                value /= PboneUtilsConfig.Instance.AverageBossBagsSlider;

                return value;
            }
        }

        public static HashSet<int> VanillaBossBags = new HashSet<int>() {
            ItemID.KingSlimeBossBag, ItemID.EyeOfCthulhuBossBag, ItemID.DeerclopsBossBag, ItemID.EaterOfWorldsBossBag, ItemID.BrainOfCthulhuBossBag,
            ItemID.QueenBeeBossBag, ItemID.SkeletronBossBag, ItemID.WallOfFleshBossBag,

            ItemID.QueenSlimeBossBag, ItemID.DestroyerBossBag, ItemID.TwinsBossBag, ItemID.SkeletronPrimeBossBag,
            ItemID.PlanteraBossBag, ItemID.GolemBossBag, ItemID.FairyQueenBossBag, ItemID.FishronBossBag,
            ItemID.MoonLordBossBag, ItemID.BossBagBetsy,

            ItemID.CultistBossBag, ItemID.BossBagDarkMage, ItemID.BossBagOgre
        };

        public static bool Loading;
        public static bool Loaded;
        public static TreasureBagOpeningInfo TempInfo;

        public Dictionary<int, int> AveragedValues;

        public static int HandleQuickSpawnItem(int item, int stack)
        {
            Item instance = new Item();
            instance.SetDefaults(item);
            Main.item[0] = instance; // I think this is required for mods that use the Main.item whoAmI to change instanced fields

            int realValue = instance.value * stack;

            TempInfo.RealValues.Add(realValue);

            return 0;
        }

        public override void PostSetupContent()
        {
            if (PboneUtilsConfig.Instance.AverageBossBags)
            {
                Loaded = false;
                Loading = true;

                AveragedValues = new Dictionary<int, int>();


                for (int i = 0; i < ItemLoader.ItemCount; i++)
                {
                    Item item = new Item();
                    item.SetDefaults(i);

                    if ((item.IsVanilla() && !VanillaBossBags.Contains(item.type))
                    || (item.ModItem != null && item.ModItem.BossBagNPC == 0)) // 0 is the default
                        continue;

                    Player dummy = new Player();

                    TempInfo = new TreasureBagOpeningInfo();
                    for (int j = 0; j < PboneUtilsConfig.Instance.AverageBossBagsSlider; j++)
                    {
                        try
                        {
                            if (item.IsVanilla())
                            {
                                dummy.OpenBossBag(item.type);
                                ItemLoader.OpenVanillaBag("bossBag", dummy, item.type);
                            }
                            else // Modded
                            {
                                item.ModItem.OpenBossBag(dummy);
                            }
                        }
                        catch (Exception e)
                        {
                            PboneUtils.Log.Debug($"Non-fatal error '{e}' encountered while averaging treasure bag (ID: {item.type}, Name: {item.Name})");
                            PboneUtils.Log.Debug("Skipping bag...");
                        }
                    }

                    AveragedValues.Add(item.type, (int)TempInfo.GetAverageValue());
                }

                Loading = false;
                Loaded = true;
            }
        }

        public override void Unload()
        {
            if (AveragedValues != null)
                AveragedValues.Clear();
            AveragedValues = null;
            Loaded = false;
        }
    }
}
