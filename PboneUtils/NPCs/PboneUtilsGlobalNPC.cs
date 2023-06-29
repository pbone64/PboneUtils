using PboneUtils.Items.Storage;
using PboneUtils.Items.Misc;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using PboneUtils.MiscModsPlayers;
using Terraria.GameContent.Bestiary;
using PboneUtils.Items.Clovers;
using PboneLib.CustomLoading.Content.Implementations.Globals;
using Terraria.Localization;

namespace PboneUtils.NPCs
{
    public class PboneUtilsGlobalNPC : PGlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            base.ModifyNPCLoot(npc, npcLoot);

            switch (npc.type)
            {
                case NPCID.DD2Betsy:
                    if (PboneUtilsConfig.Instance.StorageItemsToggle)
                        npcLoot.Add(new DropPerPlayerOnThePlayer(ModContent.ItemType<DefendersCrystal>(), 1, 1, 1, null));
                    break;
            }
        }

		public override void OnKill(NPC npc)
		{
            if (Main.player[npc.FindClosestPlayer()].GetModPlayer<PbonePlayer>().GreedyChest)
            {
                npc.extraValue = int.MinValue;
            }
        }

		public override void ModifyShop(NPCShop shop)
		{
            if (shop.NpcType == NPCID.ArmsDealer)
            {
                if (PboneUtilsConfig.Instance.PhilosophersStoneToggle)
                {
                    shop.Add(ModContent.ItemType<PhilosophersStone>());
                }
            }
			if (shop.NpcType == NPCID.BestiaryGirl)
			{
				if (PboneUtilsConfig.Instance.CloversToggle)
				{
					shop.Add(ModContent.ItemType<FourLeafClover>(), new Condition(Language.GetText("Conditions.BestiaryPercentage").WithFormatArgs(7),
					    () => Main.GetBestiaryProgressReport().CompletionPercent >= 0.07f));
				}
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
