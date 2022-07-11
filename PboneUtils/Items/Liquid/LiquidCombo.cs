using Microsoft.Xna.Framework;
using PboneUtils.DataStructures;
using PboneUtils.MiscModsPlayers;
using PboneUtils.Projectiles.Selection;
using PboneUtils.UI;
using PboneUtils.UI.States;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Liquid
{
    public class LiquidCombo : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.LiquidItemsToggle;
        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<PboneUtilsConfig>().LiquidItemsToggle;
        public override bool ShowItemIconWhenInRange => true;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.useStyle = ItemUseStyleID.HoldUp;
            UseTime = 10;
            Item.shoot = ModContent.ProjectileType<LiquidComboPro>();
            Item.channel = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 60, 0, 0);
            Item.tileBoost += 20;
            Item.UseSound = SoundID.Item64;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            PbonePlayer mPlayer = player.GetModPlayer<PbonePlayer>();
            ItemConfig config = mPlayer.ItemConfigs["Liquid"];

            // Don't run if a liquid type hasn't been chosen and the player isn't right clicking
            if (config.OnlyOneValue == default && player.altFunctionUse != 2)
                return false;

            if (player.altFunctionUse == 2)
            {
                RadialMenu.SetInfo("Liquid", Item.type);
                PboneUtils.UI.ToggleUI<RadialMenuContainer>();
                return false;
            }
            // Don't place anything if the menu is open
            if (PboneUtils.UI.GetUIState<RadialMenuContainer>().Internal.Active)
			{
                return false;
			}
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.BottomlessBucket).AddIngredient(ItemID.BottomlessLavaBucket).AddIngredient(ModContent.ItemType<BottomlessHoneyBucket>()).AddIngredient(ItemID.SuperAbsorbantSponge).AddIngredient(ItemID.LavaAbsorbantSponge).AddIngredient(ModContent.ItemType<SuperSweetSponge>()).AddTile(TileID.AlchemyTable).Register();
        }
    }
}
