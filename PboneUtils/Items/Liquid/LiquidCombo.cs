using Microsoft.Xna.Framework;
using PboneUtils.Projectiles.Selection;
using PboneUtils.UI;
using PboneUtils.UI.States;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Liquid
{
    public class LiquidCombo : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.LiquidItemsToggle;
        public override bool ShowItemIconWhenInRange => true;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.shoot = ModContent.ProjectileType<LiquidComboPro>();
            Item.channel = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 60, 0, 0);
            Item.tileBoost += 20;
            Item.UseSound = SoundID.Item64;
        }

        public override bool AltFunctionUse(Player player) => true;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                RadialMenu.SetInfo("Liquid", Item.type);
                PboneUtils.UI.ToggleUI<RadialMenuContainer>();
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
