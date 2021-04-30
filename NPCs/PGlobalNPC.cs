using PboneUtils.Items.Tools;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.NPCs
{
    public class PGlobalNPC : GlobalNPC
    {
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Wizard && PboneUtilsConfig.Instance.PhilosophersStone)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<PhilosophersStone>());
                nextSlot++;
            }
        }
    }
}
