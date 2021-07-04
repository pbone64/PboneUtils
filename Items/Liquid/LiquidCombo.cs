using Microsoft.Xna.Framework;
using PboneUtils.Projectiles.Selection;
using PboneUtils.UI;
using PboneUtils.UI.States;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Liquid
{
    public class LiquidCombo : PItem
    {
        public override bool AutoloadCondition => PboneUtilsConfig.Instance.LiquidItemsToggle;
        public override bool ShowItemIconWhenInRange => true;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 10;
            item.useTime = 10;
            item.shoot = ModContent.ProjectileType<LiquidComboPro>();
            item.channel = true;
            item.rare = ItemRarityID.Yellow;
            item.value = Item.sellPrice(0, 60, 0, 0);
            item.tileBoost += 20;
            item.UseSound = SoundID.Item64;
        }

        public override bool AltFunctionUse(Player player) => true;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                RadialMenu.SetInfo("Liquid", item.type);
                PboneUtils.UI.OpenUI<RadialMenuContainer>();
                return false;
            }

            return true;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottomlessBucket);
            recipe.AddIngredient(ModContent.ItemType<BottomlessLavaBucket>());
            recipe.AddIngredient(ModContent.ItemType<BottomlessHoneyBucket>());
            recipe.AddIngredient(ItemID.SuperAbsorbantSponge);
            recipe.AddIngredient(ModContent.ItemType<HeatAbsorbantSponge>());
            recipe.AddIngredient(ModContent.ItemType<SuperSweetSponge>());
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
