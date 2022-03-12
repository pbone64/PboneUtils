using PboneLib.CustomLoading.Content.Implementations.Misc;

namespace PboneUtils.Items.Magnets
{
    public class MagnetPlayer : PPlayer
    {
        public bool DeluxeTreasureMagnet;
        public bool HallowedTreasureMagnet;
        public bool SpectreTreasureMagnet;
        public bool RunicTreasureMagnet;
        public bool MoonLordTreasureMagnet;

        public int SuperGrabCooldown;

        public override void Initialize()
        {
            base.Initialize();
            DeluxeTreasureMagnet = false;
            HallowedTreasureMagnet = false;
            SpectreTreasureMagnet = false;
            RunicTreasureMagnet = false;
            MoonLordTreasureMagnet = false;

            SuperGrabCooldown = 0;
        }

        public override void ResetEffects()
        {
            base.ResetEffects();
            DeluxeTreasureMagnet = false;
            HallowedTreasureMagnet = false;
            SpectreTreasureMagnet = false;
            RunicTreasureMagnet = false;
            MoonLordTreasureMagnet = false;
        }
    }
}
