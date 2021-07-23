using Microsoft.Xna.Framework;

namespace PboneUtils.Tiles.Lights
{
    public class WhiteLight : BaseLight
    {
        public override Color LightColor => Color.White;
    }

    public class RedLight : BaseLight
    {
        public override Color LightColor => Color.Red;
    }

    public class GreenLight : BaseLight
    {
        public override Color LightColor => Color.Green;
    }

    public class BlueLight : BaseLight
    {
        public override Color LightColor => Color.Blue;
    }

    public class YellowLight : BaseLight
    {
        public override Color LightColor => Color.Yellow;
    }

    public class OrangeLight : BaseLight
    {
        public override Color LightColor => Color.Orange;
    }

    public class PurpleLight : BaseLight
    {
        public override Color LightColor => Color.Purple;
    }
}
