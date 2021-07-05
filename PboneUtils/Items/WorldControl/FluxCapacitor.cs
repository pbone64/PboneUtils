using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.WorldControl
{
    public class FluxCapacitor : PItem
    {
        public override bool AutoloadCondition => PboneUtilsConfig.Instance.WorldControlItemsToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 15;
            item.useTime = 15;
            item.autoReuse = false;
            item.rare = ItemRarityID.Pink;
            item.value = Item.sellPrice(0, 7, 50, 0);
        }

        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 0)
                item.channel = false;
            else
                item.channel = true;
            return base.CanUseItem(player);
        }

        public override bool UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.altFunctionUse == 0)
                {
                    Main.dayTime = !Main.dayTime;
                    Main.time = 0;
                    if (++Main.moonPhase >= 8)
                    {
                        Main.moonPhase = 0;
                    }

                    if (Main.netMode == NetmodeID.MultiplayerClient)
                        NetMessage.SendData(MessageID.WorldData);

                    return true;
                }
                else if (player.altFunctionUse == 2)
                {
                    if (PboneWorld.ForceFastForwardTime)
                    {
                        PboneWorld.ForceStopTimeFastForward();
                        player.GetModPlayer<VisualPlayer>().AmIFluxCapacitoring = false;
                    }
                    else
                    {
                        PboneWorld.ForceFastForwardTime = true;
                        player.GetModPlayer<VisualPlayer>().AmIFluxCapacitoring = true;
                    }

                    return true;
                }
            }

            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddIngredient(ItemID.Wire, 50);
            recipe.AddIngredient(ItemID.SoulofLight, 3);
            recipe.AddIngredient(ItemID.SoulofNight, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
