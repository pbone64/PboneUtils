using PboneUtils.DataStructures;
using PboneUtils.UI;
using PboneUtils.UI.States;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Tools
{
    public class MagicLight : RightClickToggleItem
    {
        public override bool AutoloadCondition => PboneUtilsConfig.Instance.MagicLightToggle;

        public override bool DrawGlowmask => true;

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            UseTime = 10;
            item.consumable = false;
            item.rare = ItemRarityID.Yellow;
            item.maxStack = 1;
        }

        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            PbonePlayer mPlayer = player.GetModPlayer<PbonePlayer>();
            ItemConfig config = mPlayer.ItemConfigs["Light"];

            // Don't run if a light color hasn't been chosen and the player isn't right clicking
            if (config.OnlyOneValue == default && player.altFunctionUse != 2)
                return false;

            RadialMenu menu = PboneUtils.UI.GetUIState<RadialMenuContainer>().Internal;
            // If there's a radial menu open, the player isn't right clicking, and the menu is hovered then don't do nothin
            if (menu != null && player.altFunctionUse != 2 && menu.IsHovered())
                return false;

            if (player.altFunctionUse == 2)
            {
                // Open config
                RadialMenu.SetInfo("Light", item.type);
                PboneUtils.UI.ToggleUI<RadialMenuContainer>();
                item.createTile = -1; // Don't place a tile when opening the menu
            }
            else
            {
                // Change tile based on config
                item.createTile = mod.TileType(config.OnlyOneValue + "Light");
            }

            return base.CanUseItem(player);
        }

        public override bool UseItem(Player player)
        {
            if (player.itemAnimation == 1)
            {
                PbonePlayer mPlayer = player.GetModPlayer<PbonePlayer>();
                ItemConfig config = mPlayer.ItemConfigs["Light"];
                // Set it back to the selected tile, because it's set to -1 when the menu is opened
                item.createTile = mod.TileType(config.OnlyOneValue + "Light");
            }

            return base.UseItem(player);
        }

        public override void HoldItem(Player player)
        {
            base.HoldItem(player);
            player.GetModPlayer<PbonePlayer>().MagicLight = true;
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<PbonePlayer>().MagicLight = Enabled;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Torch, 99);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
