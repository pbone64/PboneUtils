using Terraria;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.Localization;

namespace PboneUtils.Items.Storage
{
    public class VoidPiggy : PItem
    {
        public override bool Autosize => false;

        public bool Enabled = true;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.Size = new Vector2(24, 16);
            item.rare = ItemRarityID.Orange;
            item.value = Item.sellPrice(0, 1, 0, 0);
        }

        public override bool CanRightClick() => true;
        public override void RightClick(Player player)
        {
            base.RightClick(player);
            Enabled = !Enabled;
        }

        public override bool ConsumeItem(Player player) => false;

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<PbonePlayer>().VoidPig = Enabled;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = Enabled ? PboneUtils.Textures.Items.VoidPigEnabled : PboneUtils.Textures.Items.VoidPigDisabled;
            spriteBatch.Draw(texture, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            return false;
        }

        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound() {
                { "Enabled", Enabled }
            };
            return tag;
        }

        public override void Load(TagCompound tag)
        {
            base.Load(tag);
            Enabled = tag.GetBool("Enabled");
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PiggyBank);
            recipe.AddIngredient(ItemID.Bone, 10);
            recipe.AddIngredient(ItemID.JungleSpores, 5);
            recipe.AddRecipeGroup(PboneUtils.Recipes.AnyShadowScale, 10);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
