using Microsoft.Xna.Framework;
using PboneLib.Utils;
using PboneUtils.DataStructures;
using PboneUtils.MiscModsPlayers;
using PboneUtils.UI.States;
using System;
using Terraria.Audio;
using Terraria.ID;

namespace PboneUtils.Projectiles.Selection
{
    public class LiquidComboPro : SelectionProjectile
    {
        int LiquidType = -1;

        public override Func<Rectangle, bool> PreAction => (rect) => {
            PbonePlayer mPlayer = Owner.GetModPlayer<PbonePlayer>();
            ItemConfig config = mPlayer.ItemConfigs["Liquid"];

            LiquidType =
                (config.Data["Water"] ? LiquidID.Water :
                (config.Data["Lava"] ? LiquidID.Lava :
                (config.Data["Honey"] ? LiquidID.Honey :
                -1)));

            return LiquidType > -1 && !PboneUtils.UI.GetUIState<RadialMenuContainer>().Internal.IsHovered();
        };

        public override Action<int, int> TileAction => (i, j) => {
            PbonePlayer mPlayer = Owner.GetModPlayer<PbonePlayer>();
            ItemConfig config = mPlayer.ItemConfigs["Liquid"];

            if (!config.RedMode)
            {
                if (LiquidHelper.PlaceLiquid(i, j, (byte)LiquidType))
                {
                    SoundEngine.PlaySound(SoundID.Splash, (int)Owner.position.X, (int)Owner.position.Y);
                    return;
                }
            }
            else
            {
                if (LiquidHelper.DrainLiquid(i, j, (byte)LiquidType))
                {
                    SoundEngine.PlaySound(SoundID.Splash, (int)Owner.position.X, (int)Owner.position.Y);
                    return;
                }
            }
        };
    }
}
