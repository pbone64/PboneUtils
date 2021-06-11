using PboneUtils.DataStructures;
using PboneUtils.Helpers;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace PboneUtils.CrossMod.Content
{
    public class MysteriousTraderShopManager : SimpleModCallHandler
    {
        internal Dictionary<MysteriousTraderItem, MysteriousTraderItemRarity> Items = new Dictionary<MysteriousTraderItem, MysteriousTraderItemRarity>();
        internal bool AnyCall = false;

        internal MysteriousTraderShopManager() : base()
        {
            CallFunctions.Add("MysteriousTraderItem", RegisterItem);
        }

        #region Mod.Call Methods
        internal object RegisterItem(List<object> args)
        {
            ModCallHelper.AssertArgs(args, typeof(Mod), typeof(int), typeof(byte), typeof(Func<bool>));
            Mod mod = (Mod)args[0];
            int item = (int)args[1];
            byte rare = (byte)args[2];
            Func<bool> condition = (Func<bool>)args[3];

            return Inner_RegisterItem(mod, item, (MysteriousTraderItemRarity)rare, condition);
        }

        internal object Inner_RegisterItem(Mod mod, int item, MysteriousTraderItemRarity rare, Func<bool> condition)
        {
            MysteriousTraderItemSource source = mod.Name == PboneUtils.Instance.Name ? MysteriousTraderItemSource.Base : MysteriousTraderItemSource.Call;
            Items.Add(new MysteriousTraderItem(item, source, condition), (MysteriousTraderItemRarity)rare);

            if (source == MysteriousTraderItemSource.Call)
                AnyCall = true;

            return null;
        }
        #endregion
    }
}
