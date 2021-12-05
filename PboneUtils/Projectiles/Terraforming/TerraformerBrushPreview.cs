using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneUtils.DataStructures;
using PboneUtils.Items.Building.Terraforming;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace PboneUtils.Projectiles.Terraforming
{
    public class TerraformerBrushPreview : PboneUtilsProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }

        public ref TerraformingBrush GetCurrentBrush()
        {
            int held = Owner.HeldItem.type;

            if (held == ModContent.ItemType<Gigaxe>())
                return ref Owner.GetModPlayer<TerraformingPlayer>().AxeBrush;

            return ref Owner.GetModPlayer<TerraformingPlayer>().AxeBrush;
        }

        public override void AI()
        {
            if (!Owner.GetModPlayer<TerraformingPlayer>().HoldingTerraformer)
                Projectile.Kill();

            if (Owner.whoAmI != Main.myPlayer)
                return;

            Projectile.Center = Main.MouseWorld;
            GetCurrentBrush().Position = Projectile.Center.ToTileCoordinates();
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
            overWiresUI.Add(Projectile.whoAmI);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Owner.whoAmI != Main.myPlayer)
                return false;

            Rectangle transparentPixel = new Rectangle(0, 0, 1, 1);
            Rectangle whitePixel = new Rectangle(1, 0, 1, 1);
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            ref TerraformingBrush brush = ref GetCurrentBrush();

            (int width, int height) = brush.GetDimensions();

            width *= 16;
            height *= 16;

            Vector2 tileAlignedCenter = Projectile.Center.ToTileCoordinates().ToVector2();
            Vector2 anchor = tileAlignedCenter * 16 - new Vector2(width / 2f, height / 2f);

            if (!brush.IsEven()) anchor += new Vector2(8, 8);
            anchor -= Main.screenPosition;

            // BASE COLOR
            Color color = new Color(0.24f, 0.8f, 0.9f, 1f);
            float a = Main.playerInventory ? 0.5f : 1f;

            // DRAW FIRST RECTANGLE
            Main.spriteBatch.Draw(texture, new Rectangle((int)anchor.X, (int)anchor.Y, width, height), transparentPixel, color * a);

            // DRAW LINES
            for (int i = 0; i < width + 1; i += 16)
            {
                Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)anchor.X + i, (int)anchor.Y, 2, height + 2), whitePixel, color);
            }

            for (int j = 0; j < width + 1; j += 16)
            {
                Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)anchor.X, (int)anchor.Y + j, width + 2, 2), whitePixel, color);
            }

            return false;
        }
    }
}
