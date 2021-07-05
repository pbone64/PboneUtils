using Microsoft.Xna.Framework;
using PboneUtils.DataStructures;
using PboneUtils.Helpers;
using PboneUtils.ID;
using PboneUtils.UI.States;
using System;
using Terraria;
using Terraria.ID;

namespace PboneUtils.Projectiles.Selection
{
    public class LiquidComboPro : SelectionProjectile
    {
        int Type = -1;

        public override Func<Rectangle, bool> PreAction => (rect) => {
            PbonePlayer mPlayer = Owner.GetModPlayer<PbonePlayer>();
            ItemConfig config = mPlayer.ItemConfigs["Liquid"];

            Type =
                (config.Data["Water"] ? LiquidID.Water :
                (config.Data["Lava"] ? LiquidID.Lava :
                (config.Data["Honey"] ? LiquidID.Honey :
                -1)));

            return Type > -1 && !PboneUtils.UI.GetUIState<RadialMenuContainer>().Internal.IsHovered();
        };

        public override Action<int, int> TileAction => (i, j) => {
            PbonePlayer mPlayer = Owner.GetModPlayer<PbonePlayer>();
            ItemConfig config = mPlayer.ItemConfigs["Liquid"];

            if (!config.RedMode)
            {
                if (LiquidHelper.PlaceLiquid(i, j, (byte)Type))
                {
                    Main.PlaySound(SoundID.Splash, (int)Owner.position.X, (int)Owner.position.Y);
                    return;
                }
            }
            else
            {
                if (LiquidHelper.DrainLiquid(i, j, (byte)Type))
                {
                    Main.PlaySound(SoundID.Splash, (int)Owner.position.X, (int)Owner.position.Y);
                    return;
                }
            }
        };
    }
}
