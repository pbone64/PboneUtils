using Terraria;
using Terraria.ModLoader;

namespace PboneUtils.Projectiles
{
    public abstract class PProjectile : ModProjectile
    {
        public Player Owner => Main.player[projectile.owner];
    }
}
