using PboneLib.CustomLoading;
using Terraria;
using Terraria.ModLoader;

namespace PboneUtils.Projectiles
{
    public abstract class PProjectile : ModProjectile, IBetterLoadable
    {
        public Player Owner => Main.player[Projectile.owner];

        public bool AutoloadCondition => true;
        public bool LoadCondition() => AutoloadCondition;
    }
}
