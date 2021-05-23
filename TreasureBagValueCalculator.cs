using PboneUtils.Helpers;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils
{
    public class TreasureBagValueCalculator
    {
        public struct TreasureBagOpeningInfo
        {
            public List<int> RealValues;

            public TreasureBagOpeningInfo(int whyCantStructsHaveParamlessCtors = 0)
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

                value /= Attempts;

                return value;
            }
        }

        public const int Attempts = 100;

        public static HashSet<int> VanillaBossBags = new HashSet<int>() {
            ItemID.KingSlimeBossBag, ItemID.EyeOfCthulhuBossBag, ItemID.EaterOfWorldsBossBag, ItemID.BrainOfCthulhuBossBag, ItemID.QueenBeeBossBag, ItemID.SkeletronBossBag, ItemID.WallOfFleshBossBag,
            ItemID.DestroyerBossBag, ItemID.TwinsBossBag, ItemID.SkeletronPrimeBossBag, ItemID.PlanteraBossBag, ItemID.GolemBossBag, ItemID.FishronBossBag, ItemID.MoonLordBossBag, ItemID.BossBagBetsy,
            ItemID.CultistBossBag, ItemID.BossBagDarkMage, ItemID.BossBagOgre
        };

        public static bool Loading;
        public static bool Loaded;
        public static TreasureBagOpeningInfo TempInfo;

        public Dictionary<int, int> AveragedValues;

        public static void HandleQuickSpawnItem(Player self, int item, int stack)
        {
            Item instance = new Item();
            instance.SetDefaults(item);

            int realValue = instance.value * stack;

            TempInfo.RealValues.Add(realValue);
        }

        public void Load()
        {
            Loaded = false;
            Loading = true;

            AveragedValues = new Dictionary<int, int>();

            string origLoadStage = LoadingHelper.GetLoadStage();
            LoadingHelper.SetLoadStage("Averaging treasure bag prices...");

            for (int i = 0; i < ItemLoader.ItemCount; i++)
            {
                Item item = new Item();
                item.SetDefaults(i);

                if ((item.IsVanilla() && !VanillaBossBags.Contains(item.type))
                || (item.modItem != null && item.modItem.BossBagNPC == 0)) // 0 is the default
                    continue;

                Player dummy = new Player(false);

                LoadingHelper.SetSubText(Lang.GetItemName(i).Value);

                TempInfo = new TreasureBagOpeningInfo(1); // I need this one because otherwise it calls the default paramless ctor
                for (int j = 0; j < Attempts; j++)
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
                            item.modItem.OpenBossBag(dummy);
                        }
                    }
                    catch (Exception e)
                    {
                        PboneUtils.Log.Debug($"Non-fatal error '{e}' encountered while averaging treasure bag (ID: {item.type}, Name: {item.Name})");
                        PboneUtils.Log.Debug("Skipping bag...");
                    }
                }

                // TODO values are still 0
                AveragedValues.Add(item.type, (int)TempInfo.GetAverageValue());
            }

            LoadingHelper.SetLoadStage(origLoadStage);
            LoadingHelper.SetSubText("");
            LoadingHelper.SetProgress(0f);

            Loading = false;
            Loaded = true;
        }

        public void Unload()
        {
            if (AveragedValues != null)
                AveragedValues.Clear();
            AveragedValues = null;
            Loaded = false;
        }
    }
}
