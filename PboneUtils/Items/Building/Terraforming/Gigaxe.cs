using Microsoft.Xna.Framework;
using PboneUtils.Helpers;
using System;
using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Building.Terraforming
{
    public class Gigaxe : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.BuildingItemToggle;
        public override bool DrawGlowmask => true;
        public override Color ModifyGlowmaskColor => Color.White * ((float)(Math.Sin(Main.GameUpdateCount / 12f) + 1f) / 2f);

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.sellPrice(0, 5, 0, 0);

            UseTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override void HoldItem(Player player)
        {
            TerraformerHelper.DoHeldTerraformerLogic(player, this, ref player.GetModPlayer<TerraformingPlayer>().AxeBrush);
        }

        public override bool? UseItem(Player player)
        {
            TerraformerHelper.DoTerraformerUseLogic(Enums.TerraformerType.Tree, ref player.GetModPlayer<TerraformingPlayer>().AxeBrush);
            return null;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
        }
    }
}
