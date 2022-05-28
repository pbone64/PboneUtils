﻿using Terraria.ID;

namespace PboneUtils.Items.Ammo
{
    public abstract class EndlessAmmoItem : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.AmmoItemsToggle;

        public abstract int BaseAmmo { get; }

        public override void SetDefaults()
        {
            Item.CloneDefaults(BaseAmmo);
            Item.maxStack = 1;
            Item.consumable = false;
            Item.rare += 2;

            base.SetDefaults();
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(BaseAmmo, 3996).AddTile(TileID.CrystalBall).Register();
        }
    }
}