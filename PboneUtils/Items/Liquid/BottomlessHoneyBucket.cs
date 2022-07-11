using PboneLib.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Liquid
{
    public class BottomlessHoneyBucket : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.LiquidItemsToggle;
        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<PboneUtilsConfig>().LiquidItemsToggle;
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
                    if (LiquidHelper.PlaceLiquid(Player.tileTargetX, Player.tileTargetY, LiquidID.Honey))
                    {
                        SoundEngine.PlaySound(SoundID.Splash, player.position);
                        return true;
                    }
                }
            }

            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.HoneyBucket, 5).AddIngredient(ItemID.SoulofLight, 2).AddTile(TileID.AlchemyTable).Register();
        }
    }
}
