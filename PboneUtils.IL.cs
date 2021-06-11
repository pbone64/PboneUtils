using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.ModLoader;

namespace PboneUtils
{
    public partial class PboneUtils : Mod
    {
        public void Load_IL()
        {
            IL.Terraria.Player.Update += Player_Update;
            IL.Terraria.Main.DrawBuffIcon += Main_DrawBuffIcon;
        }

        private void Player_Update(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (!c.TryGotoNext(instr =>
                instr.MatchLdcR4(1f) &&
                instr.Next.Next.Next.Next.Next.Next.MatchStfld(typeof(Player).GetField("chest"))))
            {
                /*throw new Exception*/
                Log.Warn("Unable to patch Terraria.Player.Update: couldn't match IL");
                return;
            }

            c.FindNext(out ILCursor[] cursors, instr => instr.MatchLdcR4(1f));
            c = cursors[0];

            c.Index++;
            c.EmitDelegate<Func<float, float>>((volume) =>
            {
                if (Main.LocalPlayer.GetModPlayer<PbonePlayer>().SafeGargoyleOpen
                    || Main.LocalPlayer.GetModPlayer<PbonePlayer>().DefendersCrystalOpen)
                {
                    return 0f;
                }

                return volume;
            });
        }

        private void Main_DrawBuffIcon(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (!c.TryGotoNext(MoveType.Before, instr => instr.MatchLdcI4(2)))
            {
                /*throw new Exception*/
                Log.Warn("Unable to patch Terraria.Main.DrawBuffIcon: couldn't match IL");
                return;
            }

            c.Remove();
            c.Emit(OpCodes.Ldc_I4, 20);
        }
    }
}