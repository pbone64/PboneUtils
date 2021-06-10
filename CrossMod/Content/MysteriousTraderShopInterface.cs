using PboneUtils.DataStructures;
using PboneUtils.Helpers;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace PboneUtils.CrossMod.Content
{
    public class MysteriousTraderShopInterface : SimpleModCallHandler
    {
        internal Dictionary<MysteriousTraderItem, MysteriousTraderItemRarity> Items = new Dictionary<MysteriousTraderItem, MysteriousTraderItemRarity>();
        internal bool AnyCall = false;

        internal MysteriousTraderShopInterface()
        {
            CallFunctions.Add("MysteriousTraderItem", RegisterItem);
        }

        #region Mod.Call Methods
        internal object RegisterItem(List<object> args)
        {
            ModCallHelper.AssertArgs(args, typeof(Mod), typeof(int), typeof(int));
            Mod mod = (Mod)args[0];
            int item = (int)args[1];
            int rare = (int)args[2];

            MysteriousTraderItemSource source = mod.Name == PboneUtils.Instance.Name ? MysteriousTraderItemSource.Base : MysteriousTraderItemSource.Call;
            Items.Add(new MysteriousTraderItem(item, source), (MysteriousTraderItemRarity)rare);

            if (source == MysteriousTraderItemSource.Call)
                AnyCall = true;

            return null;
        }
        #endregion
    }
}
