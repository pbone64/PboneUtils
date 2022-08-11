using Terraria;
using Terraria.ID;

namespace PboneUtils.Buffs
{
    public class BuffBrazierBuff : PboneUtilsBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);

            // Campfire
            Main.SceneMetrics.HasCampfire = true;

            // Heart Lantern
            Main.SceneMetrics.HasHeartLantern = true;

            // Star in a Bottle
            Main.SceneMetrics.HasStarInBottle = true;

            // Sunflower
            Main.SceneMetrics.HasSunflower = true;

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
