using Terraria;

namespace PboneUtils.Helpers
{
    public static class MiscVanillaMethods
    {
        public static bool IsTargetTileInItemRange(this Player player, Item item)
        {
            if (player.position.X / 16f - Player.tileRangeX - item.tileBoost <= Player.tileTargetX && (player.position.X + player.width) / 16f + Player.tileRangeX + item.tileBoost - 1f >= Player.tileTargetX && player.position.Y / 16f - Player.tileRangeY - item.tileBoost <= Player.tileTargetY)
                return (player.position.Y + player.height) / 16f + Player.tileRangeY + item.tileBoost - 2f >= Player.tileTargetY;

            return false;
        }
    }
}
