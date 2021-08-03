using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace PboneUtils.Tiles
{
    public class ShadowPearlTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            AddMapEntry(Color.DarkViolet, name);
            DustType = DustID.Shadowflame;
            AdjTiles = new int[] { TileID.DemonAltar };
        }
    }
}
