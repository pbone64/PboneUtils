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

        public void AddRecipes(Mod mod)
        {
            ModRecipe recipe;

            if (PboneUtilsConfig.Instance.RecipeEndlessWater)
            {
                recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.EmptyBucket, 5);
                recipe.AddIngredient(ItemID.SoulofLight, 1);
                recipe.AddIngredient(ItemID.SoulofNight, 1);
                recipe.AddTile(TileID.AlchemyTable);
                recipe.SetResult(ItemID.SuperAbsorbantSponge);
                recipe.AddRecipe();

                recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.WaterBucket, 5);
                recipe.AddIngredient(ItemID.SoulofLight, 1);
                recipe.AddIngredient(ItemID.SoulofNight, 1);
                recipe.AddTile(TileID.AlchemyTable);
                recipe.SetResult(ItemID.BottomlessBucket);
                recipe.AddRecipe();
            }
        }

        public void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => $"{Lang.GetItemName(ItemID.ShadowScale)}/{Lang.GetItemName(ItemID.TissueSample)}", new int[2] {
                ItemID.ShadowScale,
                ItemID.TissueSample
            });
            RecipeGroup.RegisterGroup(AnyShadowScale, group);

            group = new RecipeGroup(() => $"{Lang.GetItemName(ItemID.DemoniteBar)}/{Lang.GetItemName(ItemID.CrimtaneBar)}", new int[2] {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup(AnyDemoniteBar, group);

            group = new RecipeGroup(() => $"{Lang.GetItemName(ItemID.AdamantiteBar)}/{Lang.GetItemName(ItemID.TitaniumBar)}", new int[2] {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar
            });
            RecipeGroup.RegisterGroup(AnyAdamantite, group);
        }
    }
}
