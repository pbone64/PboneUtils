using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace PboneUtils.DataStructures
{
    public class MysteriousTraderShopManager
    {
        public static MysteriousTraderShopManager Instance;

        static MysteriousTraderShopManager()
        {
            Instance = new MysteriousTraderShopManager();
        }

        internal Dictionary<MysteriousTraderItem, MysteriousTraderItemRarity> Items = new Dictionary<MysteriousTraderItem, MysteriousTraderItemRarity>();
        internal bool AnyCall = false;

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
