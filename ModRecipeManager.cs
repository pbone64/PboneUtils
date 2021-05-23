using Terraria;
using Terraria.ID;

namespace PboneUtils
{
    public class ModRecipeManager
    {
        public string AnyShadowScale = "PboneUtils:AnyShadowScale";
        public string AnyDemoniteBar = "PboneUtils:AnyDemoniteBar";

        public void AddRecipes()
        {

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
        }
    }
}
