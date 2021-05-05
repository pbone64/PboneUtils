using Microsoft.Xna.Framework;
using PboneUtils.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace PboneUtils.Tiles
{
    public class PreperationStationTile : ModTile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Main.tileFrameImportant[Type] = true;

            TileID.Sets.HasOutlines[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);

            AddMapEntry(Color.DarkGreen);
        }

        public override bool HasSmartInteract() => true;

        public override bool NewRightClick(int i, int j)
        {
            Main.LocalPlayer.AddBuff(ModContent.BuffType<PreperationStationBuff>(), 1800 * 60); // 30 minutes
            return true;
        }
    }
}
