using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.CellPhoneApps
{
    public abstract class AppItem : PboneUtilsItem
    {
        public abstract string AppName { get; }
        public abstract int BaseID { get; }
        public virtual int CraftAmount { get => 30; }

        public override bool LoadCondition() => PboneUtilsConfig.Instance.CellPhoneAppsToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(BaseID, CraftAmount);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
