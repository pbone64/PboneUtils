using System;
using Terraria;
using Terraria.ID;

namespace PboneUtils.Projectiles.Selection
{
    public class GigaxePro : SelectionProjectile
    {
        public override Action<int, int> TileAction => (i, j) => {
            Tile tile = Framing.GetTileSafely(i, j);
            if (tile.type == TileID.Trees)
            {
                WorldGen.KillTile(i, j);
            }
        };
    }
}
