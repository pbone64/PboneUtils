using PboneLib.Core.CrossMod.Call;
using PboneUtils.DataStructures.MysteriousTrader;
using PboneUtils.Helpers;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace PboneUtils.CrossMod.Call
{
    public class MysteriousTraderShopInterface : SimpleModCallHandler
    {
        public MysteriousTraderShopInterface() : base()
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

            return MysteriousTraderShopManager.Instance.RegisterItem(mod, item, (MysteriousTraderItemRarity)rare, condition);
        }
        #endregion
    }
}
