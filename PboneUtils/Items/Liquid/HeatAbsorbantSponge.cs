using PboneUtils.Helpers;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace PboneUtils.Items.Liquid
{
    public class HeatAbsorbantSponge : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.LiquidItemsToggle;
        public override bool ShowItemIconWhenInRange => true;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 12;
            Item.useTime = 5;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.tileBoost += 2;
        }

        public override bool? UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                if (player.IsTargetTileInItemRange(Item))
                {
                    if (LiquidHelper.DrainLiquid(Player.tileTargetX, Player.tileTargetY, LiquidID.Lava))
                    {
                        SoundEngine.PlaySound(SoundID.Splash, (int)player.position.X, (int)player.position.Y);
                        return true;
                    }
                }
            }

            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.EmptyBucket, 5);
            recipe.AddIngredient(ItemID.SoulofNight, 2);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
