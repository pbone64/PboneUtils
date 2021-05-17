using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Buffs
{
    public class PreperationStationBuff : ModBuff
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);

            // Sharpening Station
            player.armorPenetration += 4;

            // Ammobox
            player.ammoBox = true;

            // Magix Ball
            player.magicCrit += 2;
            player.magicDamage += 0.05f;
            player.statManaMax2 += 20;
            player.manaCost -= 0.02f;

            // Bewitching Table
            player.maxMinions += 1;

            // Disable other buffs
            player.buffImmune[BuffID.Sharpened] = true;
            player.buffImmune[BuffID.AmmoBox] = true;
            player.buffImmune[BuffID.Clairvoyance] = true;
            player.buffImmune[BuffID.Bewitched] = true;
        }
    }
}
