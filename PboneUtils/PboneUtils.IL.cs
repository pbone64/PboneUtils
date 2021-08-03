using Mono.Cecil.Cil;
using MonoMod.Cil;
using PboneUtils.Items.Storage;
using System;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils
{
    // FIXME
    // GetNearbyContainerProjectileList?
    public partial class PboneUtils : Mod
    {
        public FieldInfo Item59Field = typeof(SoundID).GetField("Item59");

        public void Load_IL()
        {
            IL.Terraria.Player.HandleBeingInChestRange += Player_HandleBeingInChestRange;
            IL.Terraria.Main.DrawBuffIcon += Main_DrawBuffIcon;
        }

        // Make the chest close sound not play if a portable storage is open
        private void Player_HandleBeingInChestRange(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (!c.TryGotoNext(intsr => 
                intsr.Previous.Previous.MatchLdsfld(Item59Field) &&
                intsr.Previous.MatchLdcI4(-1) &&
                intsr.MatchLdcI4(-1))
            )
                throw new Exception("Unable to path Terraria.Player.HandleBeingInChestRange: couldn't match IL");
            
            // On the [Load -1] line before playsound
            c.Remove(); // Remove the next intsr

            c.EmitDelegate<Action>(() => {
                if (!( // If no PboneUtils portable storages are open
                Main.LocalPlayer.GetModPlayer<PortableStoragePlayer>().SafeGargoyleOpen || Main.LocalPlayer.GetModPlayer<PortableStoragePlayer>().DefendersCrystalOpen
                    ))
                {
                    SoundEngine.PlaySound(SoundID.Item59, -1, -1);
                }
            });
        }

        // Make the buff time counter disappear under 20 ticks instead of 2 ticks
        private void Main_DrawBuffIcon(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (!c.TryGotoNext(MoveType.Before, instr => instr.MatchLdcI4(2)))
            {
                throw new Exception("Unable to patch Terraria.Main.DrawBuffIcon: couldn't match IL");
            }

            c.Remove();
            c.Emit(OpCodes.Ldc_I4, 20);
        }

        // [OLD] Make the chest close sound not play if a portable storage is open
        [Obsolete("1.3")]
        private void Player_Update(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (!c.TryGotoNext(instr => instr.MatchLdcR4(1f) && instr.Next.Next.Next.Next.Next.Next.MatchStfld(typeof(Player).GetField("chest"))))
            {
                throw new Exception("Unable to patch Terraria.Player.Update: couldn't match IL");
            }

            c.FindNext(out ILCursor[] cursors, instr => instr.MatchLdcR4(1f));
            c = cursors[0];

            c.Index++;
            c.EmitDelegate<Func<float, float>>((volume) => {
                if (Main.LocalPlayer.GetModPlayer<PortableStoragePlayer>().SafeGargoyleOpen
                || Main.LocalPlayer.GetModPlayer<PortableStoragePlayer>().DefendersCrystalOpen)
                {
                    // Return 0 volume if one is open so the sound is silent
                    return 0f;
                }

                return volume;
            });
        }
    }
}
