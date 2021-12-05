using PboneLib.CustomLoading.Content.Implementations;
using PboneUtils.DataStructures;

namespace PboneUtils.Items.Building.Terraforming
{
    public class TerraformingPlayer : PPlayer
    {
        public bool HoldingTerraformer;
        public TerraformingBrush AxeBrush;

        public override void Initialize()
        {
            HoldingTerraformer = false;

            AxeBrush = new TerraformingBrush(1);
        }

        public override void ResetEffects()
        {
            HoldingTerraformer = false;
        }
    }
}
