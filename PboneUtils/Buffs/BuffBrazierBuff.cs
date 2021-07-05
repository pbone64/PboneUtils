using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Buffs
{
    public class BuffBrazierBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            base.SetDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);

            // Campfire
            Main.campfire = true;

            // Heart Lantern
            Main.heartLantern = true;

            // Star in a Bottle
            Main.starInBottle = true;

            // Sunflower
            Main.sunflower = true;

            // Honey
            player.honey = true;

            // Disable other buffs
            player.buffImmune[BuffID.Campfire] = true;
            player.buffImmune[BuffID.HeartLamp] = true;
            player.buffImmune[BuffID.StarInBottle] = true;
            player.buffImmune[BuffID.Sunflower] = true;
            player.buffImmune[BuffID.Honey] = true;
        }
    }
}
