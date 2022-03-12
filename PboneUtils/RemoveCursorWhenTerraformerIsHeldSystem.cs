using PboneLib.CustomLoading.Content.Implementations.Misc;
using PboneUtils.Items.Building.Terraforming;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;

namespace PboneUtils
{
    public class RemoveCursorWhenTerraformerIsHeldSystem : PSystem
    {
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            base.ModifyInterfaceLayers(layers);
            if (!Main.playerInventory && Main.LocalPlayer.GetModPlayer<TerraformingPlayer>().HoldingTerraformer)
            {
                int cursorIndex = layers.FindIndex((layer) => layer.Name == "Vanilla: Cursor");
                if (cursorIndex != -1)
                    layers.RemoveAt(cursorIndex);
            }
        }
    }
}
