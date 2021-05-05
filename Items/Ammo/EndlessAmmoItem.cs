using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Ammo
{
    public abstract class EndlessAmmoItem : PItem
    {
        public override bool AutoloadCondition => PboneUtilsConfig.Instance.AmmoItemsToggle;

        public abstract int BaseAmmo { get; }

        public override void SetDefaults()
        {
            item.CloneDefaults(BaseAmmo);
            item.maxStack = 1;
            item.consumable = false;
            item.rare += 2;

            base.SetDefaults();
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(BaseAmmo, 3996);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
