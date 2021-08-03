using Terraria.ID;

namespace PboneUtils.Items.Test
{
    public class TileTester : PboneUtilsItem
    {
        public override string Texture => "PboneUtils/Items/Test/TestItem";

        public override void SetDefaults()
        {
            base.SetDefaults();
            UseTime = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.createTile = TileID.BloomingHerbs;
        }
    }
}
