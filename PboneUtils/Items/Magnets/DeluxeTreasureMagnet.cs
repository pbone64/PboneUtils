using Terraria;
using Terraria.ID;

namespace PboneUtils.Items.Magnets
{
    public class DeluxeTreasureMagnet : RightClickToggleItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.MagnetItemsToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(0, 15, 0, 0);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<MagnetPlayer>().DeluxeTreasureMagnet = Enabled;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.GoldCoin, 15).AddTile(TileID.MythrilAnvil).Register();
        }
    }
}
