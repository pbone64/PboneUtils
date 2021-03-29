using Terraria;
using Terraria.ID;

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

		public static bool BetterPlaceObject(int x, int y, int type, bool mute = false, int style = 0, int alternate = 0, int random = -1, int direction = -1)
		{
			TileObject objectData;
			if (!TileObject.CanPlace(x, y, type, style, direction, out objectData, false, false))
				return false;

			objectData.random = random;
			if (TileObject.Place(objectData))
			{
				WorldGen.SquareTileFrame(x, y, true);

				if (!mute)
					Main.PlaySound(SoundID.Dig, x * 16, y * 16, 1, 1f, 0f);

				return true;
			}
			return false;
		}
	}
}
