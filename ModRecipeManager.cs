using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace PboneUtils
{
    public class ModRecipeManager
    {
        public string AnyShadowScale = "PboneUtils:AnyShadowScale";

        public void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => $"{Language.GetTextValue("ItemName.ShadowScale")}/{Language.GetTextValue("ItemName.TissueSample")}", new int[2] {
                ItemID.ShadowScale,
                ItemID.TissueSample
            });
            RecipeGroup.RegisterGroup(AnyShadowScale, group);
        }
    }
}
