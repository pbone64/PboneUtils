using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils
{
    public class ModRecipeManager
    {
        public string AnyShadowScale = "PboneUtils:AnyShadowScale";
        public string AnyDemoniteBar = "PboneUtils:AnyDemoniteBar";
        public string AnyAdamantite = "PboneUtils:AnyAdamantite";
        public string AnyCopperBar = "PboneUtils:AnyCopperBar";
        public string AnySilverBar = "PboneUtils:AnySilverBar";
        public string AnyGoldBar = "PboneUtils:AnyGoldBar";

        public void AddRecipes(Mod mod)
        {
            if (PboneUtilsConfig.Instance.RecipeEndlessWater)
            {
                mod.CreateRecipe(ItemID.BottomlessBucket)
                    .AddIngredient(ItemID.WaterBucket, 5)
                    .AddIngredient(ItemID.SoulofNight, 1)
                    .AddIngredient(ItemID.SoulofLight, 1)
                    .AddTile(TileID.AlchemyTable)
                    .Register();

                mod.CreateRecipe(ItemID.BottomlessBucket)
                    .AddIngredient(ItemID.EmptyBucket, 5)
                    .AddIngredient(ItemID.SoulofNight, 1)
                    .AddIngredient(ItemID.SoulofLight, 1)
                    .AddTile(TileID.AlchemyTable)
                    .Register();
            }
        }

        public void AddRecipeGroups()
        {
            RegisterTwoItemGroup(AnyShadowScale, ItemID.ShadowScale, ItemID.TissueSample);
            RegisterTwoItemGroup(AnyDemoniteBar, ItemID.DemoniteBar, ItemID.CrimtaneBar);
            RegisterTwoItemGroup(AnyAdamantite, ItemID.AdamantiteBar, ItemID.TitaniumBar);
            RegisterTwoItemGroup(AnyCopperBar, ItemID.CopperBar, ItemID.TinBar);
            RegisterTwoItemGroup(AnySilverBar, ItemID.SilverBar, ItemID.TungstenBar);
            RegisterTwoItemGroup(AnyGoldBar, ItemID.GoldBar, ItemID.PlatinumBar);
        }

        private void RegisterTwoItemGroup(string key, int item1, int item2)
        {
            RecipeGroup group = new RecipeGroup(() => $"{Lang.GetItemName(item1)}/{Lang.GetItemName(item2)}", new int[2] {
                item1,
                item2
            });
            RecipeGroup.RegisterGroup(key, group);
        }
    }
}
