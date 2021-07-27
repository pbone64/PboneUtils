using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace PboneUtils.Tiles
{
    public class PGlobalTile : GlobalTile
    {
        public Dictionary<int, Func<bool>> TileBreakPredicates = new Dictionary<int, Func<bool>>();

        public override bool Autoload(ref string name)
        {
            TileBreakPredicates = new Dictionary<int, Func<bool>>();
            TileBreakPredicates.Add(ModContent.TileType<PetrifiedSafeTile>(), PetrifiedSafeTile.Predicate);

            return base.Autoload(ref name);
        }

        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
            => CheckPredicate(Framing.GetTileSafely(i, j - 1), base.CanKillTile(i, j, type, ref blockDamaged));

        public override bool CanExplode(int i, int j, int type)
            => CheckPredicate(Framing.GetTileSafely(i, j - 1), base.CanExplode(i, j, type));

        private bool CheckPredicate(Tile tile, bool baseValue)
        {
            if (TileBreakPredicates.TryGetValue(tile.type, out Func<bool> predicate))
                return predicate();

            return baseValue;
        }
    }
}
