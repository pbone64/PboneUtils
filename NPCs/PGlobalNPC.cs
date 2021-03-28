using PboneUtils.Items.Liquid;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.NPCs
{
    public class PGlobalNPC : GlobalNPC
    {
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.PartyGirl)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<SuperSweetSponge>());
                nextSlot++;
            }
        }
    }
}
