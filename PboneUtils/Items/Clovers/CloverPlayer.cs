using PboneLib.CustomLoading.Content.Implementations.Misc;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;

namespace PboneUtils.Items.Clovers
{
    public class CloverPlayer : PPlayer
    {
        public const int FourLeafClover = 1;
        public const int CorruptedClover = 2;
        public const int GoldenClover = 3;

        public int CloverMode;

        public override void Initialize()
        {
            base.Initialize();
            CloverMode = -1;
        }

        public override void ResetEffects()
        {
            base.ResetEffects();
            CloverMode = -1;
        }

        public override bool PreModifyLuck(ref float luck)
        {
            if (CloverMode == CorruptedClover)
                return false;

            return base.PreModifyLuck(ref luck);
        }

        public override void ModifyLuck(ref float luck)
        {
            if (CloverMode == CorruptedClover)
                luck = 0;

            if (CloverMode == GoldenClover)
                luck += PboneUtilsConfig.Instance.GoldenCloverLuckSlider;

            base.ModifyLuck(ref luck);
        }

        public float CalculateLuck()
        {
            float luck = 0f;
            if (PlayerLoader.PreModifyLuck(Player, ref luck))
            {
                luck = GetLadyBugLuckRecreation() * 0.2f + Player.torchLuck * 0.2f;
                luck += (float)(int)Player.luckPotion * 0.1f;
                if (LanternNight.LanternsUp)
                {
                    luck += 0.3f;
                }
                if (Player.HasGardenGnomeNearby)
                {
                    luck += 0.2f;
                }
            }
            PlayerLoader.ModifyLuck(Player, ref luck);
            return luck;
        }

        public float GetLadyBugLuckRecreation()
        {
            if (Player.ladyBugLuckTimeLeft > 0.0)
            {
                return (float)Player.ladyBugLuckTimeLeft / (float)NPC.ladyBugGoodLuckTime;
            }
            if (Player.ladyBugLuckTimeLeft < 0.0)
            {
                return (0f - (float)Player.ladyBugLuckTimeLeft) / (float)NPC.ladyBugBadLuckTime;
            }
            return 0f;
        }

        public void TryChangeCloverMode(int newMode)
        {
            if (newMode > CloverMode)
                CloverMode = newMode;
        }
    }
}
