using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Tools
{
    public class InfiniteMana : PItem
    {
        public override bool AutoloadCondition => PboneUtilsConfig.Instance.InfiniteManaToggle;
        public override bool DrawGlowmask => true;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Purple;
            item.accessory = true;
            item.value = Item.buyPrice(0, 15, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetModPlayer<PbonePlayer>().InfiniteMana = true;
            player.allDamage -= 0.35f;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ManaCrystal, 10);
            recipe.AddIngredient(ItemID.Ectoplasm, 7);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
