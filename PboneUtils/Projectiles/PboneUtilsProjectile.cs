using PboneLib.CustomLoading.Content.Implementations;
using Terraria;

namespace PboneUtils.Projectiles
{
    public abstract class PboneUtilsProjectile : PProjectile
    {
        public Player Owner => Main.player[Projectile.owner];
    }
}
