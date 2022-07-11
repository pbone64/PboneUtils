using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.WorldControl
{
    public class AmuletOfRain : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.WorldControlItemsToggle;
        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<PboneUtilsConfig>().WorldControlItemsToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.useStyle = ItemUseStyleID.HoldUp;
            UseTime = 15;
            Item.autoReuse = false;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 7, 50, 0);
        }

        public override bool? UseItem(Player player)
        {
            // Call it on the server and every client - this updates visuals immediatly and shouldn't cause any issues
            if (Main.raining)
                Main.StopRain();
            else
                Main.StartRain();

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.FrostCore, 1).AddRecipeGroup(ModRecipeManager.Recipes.AnyAdamantite, 3).AddTile(TileID.MythrilAnvil).Register();
        }
    }
}