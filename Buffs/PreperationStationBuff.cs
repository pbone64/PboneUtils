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

            player.armorPenetration += 4;

            player.ammoBox = true;

            player.magicCrit += 2;
            player.magicDamage += 0.05f;
            player.statManaMax2 += 20;
            player.manaCost -= 0.02f;

            player.maxMinions += 1;

            player.buffImmune[BuffID.Sharpened] = true;
            player.buffImmune[BuffID.AmmoBox] = true;
            player.buffImmune[BuffID.Clairvoyance] = true;
            player.buffImmune[BuffID.Bewitched] = true;
        }
    }
}
