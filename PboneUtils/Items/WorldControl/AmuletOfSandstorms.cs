using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;

namespace PboneUtils.Items.WorldControl
{
    public class AmuletOfSandstorms : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.WorldControlItemsToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.autoReuse = false;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 7, 50, 0);
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (Sandstorm.Happening)
                    PboneWorld.StopStandstorm();
                else
                    PboneWorld.StartSandstorm();

                // Same case as amulet of rain syncing, just hope it works
                if (Main.netMode == NetmodeID.MultiplayerClient)
                    NetMessage.SendData(MessageID.WorldData);

                return true;
            }

            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.AncientBattleArmorMaterial, 1).AddRecipeGroup(PboneUtils.Recipes.AnyAdamantite, 3).AddTile(TileID.MythrilAnvil).Register();
        }
    }
}
