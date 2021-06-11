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
            MyRegisterItem(ItemID.RodofDiscord, MysteriousTraderItemRarity.Legendary, new Func<bool>(() => NPC.downedMechBossAny));
            MyRegisterItem(ItemID.SittingDucksFishingRod, MysteriousTraderItemRarity.Rare, new Func<bool>(() => NPC.downedBoss3));
            MyRegisterItem(ItemID.BandofRegeneration, MysteriousTraderItemRarity.Common, new Func<bool>(() => true));
            MyRegisterItem(ItemID.LesserHealingPotion, MysteriousTraderItemRarity.NonUnique, new Func<bool>(() => true));
        }

        internal Dictionary<MysteriousTraderItem, MysteriousTraderItemRarity> Items = new Dictionary<MysteriousTraderItem, MysteriousTraderItemRarity>();
        internal bool AnyCall = false;

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
