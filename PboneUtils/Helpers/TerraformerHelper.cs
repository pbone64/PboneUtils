using Terraria.GameInput;
using PboneUtils.Items.Building.Terraforming;
using Terraria;
using Terraria.ModLoader;
using PboneUtils.DataStructures;
using Microsoft.Xna.Framework;
using PboneUtils.Projectiles.Terraforming;
using PboneUtils.Enums;
using Terraria.ID;

namespace PboneUtils.Helpers
{
    public static class TerraformerHelper
    {
        public static void DoHeldTerraformerLogic(Player player, ModItem item, ref TerraformingBrush brush)
        {
            TerraformingPlayer modPlayer = player.GetModPlayer<TerraformingPlayer>();
            modPlayer.HoldingTerraformer = true;

            if (PboneUtilsHotkeys.TerraformingScrollModeKey.Current)
            {
                if (PlayerInput.ScrollWheelDelta != 0)
                {
                    if (PlayerInput.ScrollWheelDelta > 0)
                        brush.IncreaseSize();
                    else
                        brush.DecreaseSize();
                }
            }

            if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[ModContent.ProjectileType<TerraformerBrushPreview>()] < 1)
                Projectile.NewProjectile(
                    player.GetProjectileSource_Item(item.Item), Main.MouseWorld, Vector2.Zero,
                    ModContent.ProjectileType<TerraformerBrushPreview>(), 0, 0, player.whoAmI
                    );
        }

        public static void DoTerraformerUseLogic(TerraformerType type, ref TerraformingBrush brush)
        {
            Point pos = brush.Position;
            pos -= new Point(brush.Size / 2, brush.Size / 2);

            for (int i = pos.X; i < pos.X + brush.Size; i++)
            {
                for (int j = pos.Y; j < pos.Y + brush.Size; j++)
                {
                    Tile t = Framing.GetTileSafely(i, j);

                    switch (type)
                    {
                        case TerraformerType.Block:
                            WorldGen.KillTile(i, j);
                            break;
                        case TerraformerType.Wall:
                            WorldGen.KillWall(i, j);
                            break;
                        case TerraformerType.Tree:
                            if (t.type == TileID.Trees || TileID.Sets.CountsAsGemTree[t.type])
                            {
                                if (TileID.Sets.IsShakeable[t.type])
                                    WorldGen.KillTile(i, j, true); // Shake the tree

                                WorldGen.KillTile(i, j); // Kill the tree
                            }
                            break;
                    }
                }
            }
        }
    }
}
