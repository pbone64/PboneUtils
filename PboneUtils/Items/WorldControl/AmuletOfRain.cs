using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.WorldControl
{
    public class AmuletOfRain : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.WorldControlItemsToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 15;
            item.useTime = 15;
            item.autoReuse = false;
            item.rare = ItemRarityID.Pink;
            item.value = Item.sellPrice(0, 7, 50, 0);
        }

        public override bool UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (Main.raining)
                    PboneWorld.StopRain();
                else
                    PboneWorld.StartRain();

                // Vanilla does some wacky syncing. I'm just calling SendData and hoping for the best
                /*if (Main.maxRaining != Main.oldMaxRaining)
                {
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendData(MessageID.WorldData);
                    }
                    Main.oldMaxRaining = Main.maxRaining;
                }*/
                if (Main.netMode == NetmodeID.MultiplayerClient)
                    NetMessage.SendData(MessageID.WorldData);

                return true;
            }

            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddRecipeGroup(PboneUtils.Recipes.AnyAdamantite, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}