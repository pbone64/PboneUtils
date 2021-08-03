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
            CreateRecipe(1).AddIngredient(BaseID, CraftAmount).AddTile(TileID.Bottles).Register();
        }
    }
}
