using Microsoft.Xna.Framework;
using PboneUtils.Items.Arena;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace PboneUtils.Tiles
{
    public class AsphaltPlatformTile : ModTile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Main.tileFrameImportant[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;

            TileID.Sets.Platforms[Type] = true;

            TileObjectData.newTile.CoordinateHeights = new[] { 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleMultiplier = 27;
            TileObjectData.newTile.StyleWrapLimit = 27;
            TileObjectData.newTile.UsesCustomCanPlace = false;
            TileObjectData.newTile.LavaDeath = true;
            TileObjectData.addTile(Type);

            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
            AddMapEntry(Color.Black);

            dustType = 109; // DustID.Asphalt, compiling doesn't work with DustID.Asphalt for whatever reason
            drop = ModContent.ItemType<AsphaltPlatform>();
            
            disableSmartCursor = true;
            adjTiles = new int[] { TileID.Platforms };
        }

        public override void FloorVisuals(Player player)
        {
            base.FloorVisuals(player);
            player.powerrun = true;
        }

        public override bool CreateDust(int i, int j, ref int type)
        {
            type = 109; // See SetDefaults
            return base.CreateDust(i, j, ref type);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            base.NumDust(i, j, fail, ref num);
            num = fail ? 1 : 3;
        }
    }
}
