using Terraria.ModLoader;

namespace PboneUtils.Items.Magnets
{
    public class MagnetPlayer : ModPlayer
    {
        public bool DeluxeTreasureMagnet;
        public bool HallowedTreasureMagnet;
        public bool SpectreTreasureMagnet;
        public bool RunicTreasureMagnet;
        public int SuperGrabCooldown;

        public override void Initialize()
        {
            base.Initialize();
            DeluxeTreasureMagnet = false;
            HallowedTreasureMagnet = false;
            SpectreTreasureMagnet = false;
            RunicTreasureMagnet = false;

            SuperGrabCooldown = 0;
        }

        public override void ResetEffects()
        {
            base.ResetEffects();
            DeluxeTreasureMagnet = false;
            HallowedTreasureMagnet = false;
            SpectreTreasureMagnet = false;
            RunicTreasureMagnet = false;
        }
    }
}
