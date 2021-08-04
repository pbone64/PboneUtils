using PboneLib.CustomLoading.Content.Implementations;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace PboneUtils.MiscModsPlayers
{
    public class VisualPlayer : PPlayer
    {
        #region Fields
        public bool AmIFluxCapacitoring;
        #endregion

        public override void Initialize()
        {
            base.Initialize();
            AmIFluxCapacitoring = false;
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
            if (AmIFluxCapacitoring && drawInfo.shadow == 0f && Main.rand.NextBool(8))
            {
                Dust d = Dust.NewDustDirect(drawInfo.Position, Player.width, Player.height, DustID.Electric);
                d.noGravity = true;
            }
        }
    }
}
