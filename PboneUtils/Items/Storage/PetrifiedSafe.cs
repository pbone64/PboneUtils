using Microsoft.Xna.Framework;
using PboneUtils.Projectiles.Storage;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Storage
{
    public class PetrifiedSafe : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.StorageItemsToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.useStyle = ItemUseStyleID.Swing;
            UseTime = 28;
            Item.UseSound = SoundID.Item37;
            Item.shoot = ModContent.ProjectileType<PetrifiedSafeProjectile>();
            Item.shootSpeed = 4;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 2, 0, 0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position -= new Vector2(0, 12);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            if (PboneUtilsConfig.Instance.RecipePetrifiedSafe)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.Safe);
                recipe.AddIngredient(ItemID.StoneBlock, 99);
                recipe.AddIngredient(ItemID.Bone, 10);
                recipe.AddTile(TileID.Hellforge);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
