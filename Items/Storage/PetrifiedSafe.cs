using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Storage
{
    public class PetrifiedSafe : PItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.useStyle = ItemUseStyleID.SwingThrow;
            UseTime = 28;
            item.shoot = 1;
            item.shootSpeed = 4;
            item.rare = ItemRarityID.Orange;
            item.value = Item.sellPrice(0, 2, 0, 0);
        }
    }
}
