using Terraria.ModLoader;

namespace PboneUtils
{
    public class PbonePlayer : ModPlayer
    {
        #region Fields
        // Storage
        public bool VoidPig;
        #endregion

        public override void Initialize()
        {
            base.Initialize();
            VoidPig = false;
        }

        public override void ResetEffects()
        {
            base.ResetEffects();
            VoidPig = false;
        }
    }
}
