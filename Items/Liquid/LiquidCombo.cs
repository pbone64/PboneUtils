using PboneUtils.DataStructures;
using PboneUtils.Helpers;
using PboneUtils.ID;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Liquid
{
    public class LiquidCombo : PItem
    {
        public override bool ShowItemIconWhenInRange => true;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 10;
            item.useTime = 10;
            item.useTurn = true;
            item.autoReuse = true;
            item.rare = ItemRarityID.Yellow;
            item.value = Item.sellPrice(0, 60, 0, 0);
            item.tileBoost += 4;
        }

        public override bool AltFunctionUse(Player player) => true;
        public override bool UseItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                if (player.altFunctionUse == 2)
                {
                    PboneUtils.UI.ItemConfigurer.Toggle("Liquid", item.type);
                    return true;
                }
                else if (!PboneUtils.UI.ItemConfigurer.IsHovered())
                {
                    if (player.IsTargetTileInItemRange(item))
                    {
                        PbonePlayer mPlayer = player.GetModPlayer<PbonePlayer>();
                        ItemConfig config = mPlayer.ItemConfigs["Liquid"];
                        int type = config.Data["Water"] ? LiquidID.Water :
                            (config.Data["Lava"] ? LiquidID.Lava :
                            (config.Data["Honey"] ? LiquidID.Honey : -1));

                        if (type == -1)
                            return false;

                        if (!config.RedMode)
                        {
                            if (LiquidHelper.PlaceLiquid(Player.tileTargetX, Player.tileTargetY, (byte)type))
                            {
                                Main.PlaySound(SoundID.Splash, (int)player.position.X, (int)player.position.Y);
                                return true;
                            }
                        }
                        else
                        {
                            if (LiquidHelper.DrainLiquid(Player.tileTargetX, Player.tileTargetY, (byte)type))
                            {
                                Main.PlaySound(SoundID.Splash, (int)player.position.X, (int)player.position.Y);
                                return true;
                            }
                        }
                    }
                }
            }

            return base.UseItem(player);
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
