using PboneLib.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace PboneUtils.Content.Liquid.Items
{
    public sealed class BottomlessHoneyBucket : PboneUtilsItem
    {
        public sealed override bool LoadCondition() => FeatureConfig.Instance.ItemToggle_EndlessLiquidItems;

        public sealed override void SetDefaults()
        {
            base.SetDefaults();

            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.tileBoost += 2;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 12;
            Item.useTime = 5;
            Item.useTurn = true;
            Item.autoReuse = true;

            ShowItemIconWhenInRange = true;
        }

        public sealed override bool? UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI || !player.IsTargetTileInItemRange(Item))
                return base.UseItem(player);

            if (LiquidHelper.PlaceLiquid(Player.tileTargetX, Player.tileTargetY, LiquidID.Honey))
            {
                SoundEngine.PlaySound(SoundID.Splash, (int)player.position.X, (int)player.position.Y);
                return true;
            }

            return base.UseItem(player);
        }

        public sealed override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ItemID.HoneyBucket, 5)
                .AddIngredient(ItemID.SoulofLight, 2)
                .AddTile(TileID.AlchemyTable)
                .Register();
        }
    }
}
