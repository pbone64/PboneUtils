using Microsoft.Xna.Framework;

namespace PboneUtils.Projectiles.Terraforming
{
    public class GigaxeHoldout : HoldoutBase
    {
        public override string Texture => "PboneUtils/Items/Building/Terraforming/Gigaxe";

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.Size = new Vector2(32, 36);
        }
    }
}
