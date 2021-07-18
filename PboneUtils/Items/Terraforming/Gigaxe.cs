using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Terraforming
{
    public class Gigaxe : PItem
    {
        public override string Texture => "Terraria/Item_" + ItemID.CopperAxe;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(0, 1, 0, 0);

            item.useTime = 10;
            item.useAnimation = 40;
            item.useStyle = ItemUseStyleID.SwingThrow;
        }

        public override void UseStyle(Player player)
        {
            base.UseStyle(player);
            if (player.itemAnimation == 1)
            {
                Vector2 screenBottom = Main.screenPosition + new Vector2(Main.screenWidth, Main.screenHeight);
                for (int i = (int)Main.screenPosition.X / 16; i < (int)screenBottom.X / 16; i++)
                {
                    for (int j = (int)Main.screenPosition.Y / 16; j < (int)screenBottom.Y / 16; j++)
                    {
                        if (!WorldGen.InWorld(i, j))
                            continue;

                        Tile tile = Framing.GetTileSafely(i, j);
                        
                        if (tile.type == TileID.Trees)
                        {
                            WorldGen.KillTile(i, j);
                        }
                    }
                }
            }
        }
    }
}
