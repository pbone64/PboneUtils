using PboneUtils.Items.Storage;
using PboneUtils.Items.Tools;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.NPCs
{
    public class PGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            switch (npc.type)
            {
                case NPCID.DD2Betsy:
                    if (PboneUtilsConfig.Instance.StorageItemsToggle)
                        Item.NewItem(npc.getRect(), ModContent.ItemType<DefendersCrystal>());
                    break;
            }
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            switch (type)
            {
                case NPCID.Wizard:
                    if (PboneUtilsConfig.Instance.PhilosophersStone)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<PhilosophersStone>());
                        nextSlot++;
                    }
                    break;
            }
        }

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            base.EditSpawnRate(player, ref spawnRate, ref maxSpawns);
            PbonePlayer modPlayer = player.GetModPlayer<PbonePlayer>();
            spawnRate = (int)(spawnRate * modPlayer.SpawnRateMultiplier);
            maxSpawns = (int)(maxSpawns * modPlayer.MaxSpawnsMultiplier);
        }
    }
}
