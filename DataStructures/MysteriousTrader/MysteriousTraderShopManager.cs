using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.DataStructures.MysteriousTrader
{
    public class MysteriousTraderShopManager
    {
        public static MysteriousTraderShopManager Instance;

        static MysteriousTraderShopManager()
        {
            Instance = new MysteriousTraderShopManager();

            MyRegisterItem(ItemID.TruffleWorm, MysteriousTraderItemRarity.Legendary, () => NPC.downedPlantBoss && !NPC.downedFishron);
            MyRegisterItem(ItemID.RodofDiscord, MysteriousTraderItemRarity.Legendary, () => NPC.downedMechBossAny);
            MyRegisterItem(ItemID.Arkhalis, MysteriousTraderItemRarity.Legendary, () => NPC.downedBoss1);

            MyRegisterItem(ItemID.SittingDucksFishingRod, MysteriousTraderItemRarity.Rare, () => NPC.downedBoss3);
            MyRegisterItem(ItemID.PulseBow, MysteriousTraderItemRarity.Rare, () => NPC.downedPlantBoss);
            MyRegisterItem(ItemID.AnkletoftheWind, MysteriousTraderItemRarity.Rare);
            MyRegisterItem(ItemID.AmmoBox, MysteriousTraderItemRarity.Rare);
            MyRegisterItem(ItemID.CelestialMagnet, MysteriousTraderItemRarity.Rare);
            MyRegisterItem(ItemID.MagicHat, MysteriousTraderItemRarity.Rare);
            MyRegisterItem(ItemID.StaffofRegrowth, MysteriousTraderItemRarity.Rare);
            MyRegisterItem(ItemID.BlizzardinaBalloon, MysteriousTraderItemRarity.Rare);
            MyRegisterItem(ItemID.SandstorminaBalloon, MysteriousTraderItemRarity.Rare);
            MyRegisterItem(ItemID.CloudinaBalloon, MysteriousTraderItemRarity.Rare);
            MyRegisterItem(ItemID.WhoopieCushion, MysteriousTraderItemRarity.Rare);

            MyRegisterItem(ItemID.BandofRegeneration, MysteriousTraderItemRarity.Common);
            MyRegisterItem(ItemID.Aglet, MysteriousTraderItemRarity.Common);

            MyRegisterItem(ItemID.DPSMeter, MysteriousTraderItemRarity.Common);
            MyRegisterItem(ItemID.LifeformAnalyzer, MysteriousTraderItemRarity.Common);
            MyRegisterItem(ItemID.Stopwatch, MysteriousTraderItemRarity.Common);

            MyRegisterItem(ItemID.ActuationAccessory, MysteriousTraderItemRarity.Common);
            MyRegisterItem(ItemID.PortableCementMixer, MysteriousTraderItemRarity.Common);
            MyRegisterItem(ItemID.ExtendoGrip, MysteriousTraderItemRarity.Common);
            MyRegisterItem(ItemID.BrickLayer, MysteriousTraderItemRarity.Common);

            MyRegisterItem(ItemID.MagicMirror, MysteriousTraderItemRarity.Common);

            MyRegisterItem(ItemID.ShinyRedBalloon, MysteriousTraderItemRarity.Common);
            MyRegisterItem(ItemID.Aglet, MysteriousTraderItemRarity.Common);

            //MyRegisterItem(ItemID.LesserHealingPotion, MysteriousTraderItemRarity.NonUnique, new Func<bool>(() => !Main.hardMode));
            //MyRegisterItem(ItemID.HealingPotion, MysteriousTraderItemRarity.NonUnique, new Func<bool>(() => Main.hardMode));
        }

        internal Dictionary<MysteriousTraderItem, MysteriousTraderItemRarity> Items = new Dictionary<MysteriousTraderItem, MysteriousTraderItemRarity>();
        internal bool AnyCall = false;

        private static object MyRegisterItem(int item, MysteriousTraderItemRarity rare)
            => Instance.RegisterItem(PboneUtils.Instance, item, rare, new Func<bool>(() => true));

        private static object MyRegisterItem(int item, MysteriousTraderItemRarity rare, Func<bool> condition)
            => Instance.RegisterItem(PboneUtils.Instance, item, rare, condition);

        internal object RegisterItem(Mod mod, int item, MysteriousTraderItemRarity rare, Func<bool> condition)
        {
            try
            {
                MysteriousTraderItemSource source = mod.Name == PboneUtils.Instance.Name ? MysteriousTraderItemSource.Base : MysteriousTraderItemSource.Call;
                Items.Add(new MysteriousTraderItem(item, source, condition), rare);

                if (source == MysteriousTraderItemSource.Call)
                    AnyCall = true;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
