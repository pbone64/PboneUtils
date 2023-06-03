using Microsoft.Xna.Framework;
using PboneLib.CustomLoading.Content.Implementations.Misc;
using Terraria;

namespace PboneUtils.Items.Clovers
{
    public abstract class BaseCloverUI : PInfoDisplay
    {
        public abstract int CloverUIId { get; }

        // A custom method is required, because the vanilla luck value just doesn't work
        public override string DisplayValue(ref Color color) => Main.LocalPlayer.GetModPlayer<CloverPlayer>().CalculateLuck().ToString();
        public override string Texture => "PboneUtils/Items/Clovers/CloverUI_" + CloverUIId;

        public override bool Active() => Main.LocalPlayer.GetModPlayer<CloverPlayer>().CloverMode == CloverUIId;
    }

    public class CloverUI1 : BaseCloverUI
    {
        public override int CloverUIId => CloverPlayer.FourLeafClover;
    }

    public class CloverUI2 : BaseCloverUI
    {
        public override int CloverUIId => CloverPlayer.CorruptedClover;
    }

    public class CloverUI3 : BaseCloverUI
    {
        public override int CloverUIId => CloverPlayer.GoldenClover;
    }
}
