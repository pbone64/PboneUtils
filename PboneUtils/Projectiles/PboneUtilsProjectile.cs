using PboneLib.CustomLoading.Implementations;
using Terraria;

namespace PboneUtils.Projectiles
{
    public abstract class PboneUtilsProjectile : PProjectile
    {
        public Player Owner => Main.player[Projectile.owner];

        public bool AutoloadCondition => true;
        public bool LoadCondition() => AutoloadCondition;
    }
}
