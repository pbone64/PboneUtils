using Microsoft.Xna.Framework;
using PboneUtils.Projectiles.Selection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Terraforming
{
    public class Gigaxe : PItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Yellow;
            item.value = Item.sellPrice(0, 1, 0, 0);

            item.useTime = 10;
            item.useAnimation = 10;
            item.channel = true;
            item.useStyle = ItemUseStyleID.SwingThrow;

            item.shoot = ModContent.ProjectileType<GigaxePro>();
        }
    }
}
