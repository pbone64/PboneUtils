using PboneLib.CustomLoading.Content.Implementations.Content;
using Terraria;

namespace PboneUtils.Content
{
    public abstract class PboneUtilsItem : PItem
    {
        public bool ShowItemIconWhenInRange = false;

        public override void HoldItem(Player player)
        {
            base.HoldItem(player);

            if (ShowItemIconWhenInRange && player.IsTargetTileInItemRange(Item))
            {
                player.cursorItemIconEnabled = true;
                player.cursorItemIconID = Item.type;
            }
        }
    }
}
