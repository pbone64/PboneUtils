using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Buffs
{
    public class PreperationStationBuff : PboneUtilsBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);

            // Sharpening Station
            player.GetArmorPenetration(DamageClass.Melee) += 12;

            // Ammobox
            player.ammoBox = true;

            // Magic Ball
            player.GetCritChance(DamageClass.Magic) += 2;
            player.GetDamage(DamageClass.Magic) += 0.05f;
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
