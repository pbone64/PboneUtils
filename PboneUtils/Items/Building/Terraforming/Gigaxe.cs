using PboneUtils.Projectiles.Selection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Building.Terraforming
{
    public class Gigaxe : PboneUtilsItem
    {
        // UNFINISHED ITEM, STOP BEING LAZY PBONE

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 1, 0, 0);

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.shoot = ModContent.ProjectileType<GigaxePro>();
        }
    }
}
