using PboneLib.Utils;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace PboneUtils.NPCs.Town
{
    [AutoloadHead]
    public class MysteriousTrader : PboneUtilsNPC
    {
        public override bool LoadCondition() =>  ModContent.GetInstance<PboneUtilsConfig>().MysteriousTrader;
        public override bool IsLoadingEnabled(Mod mod) => ModContent.GetInstance<PboneUtilsConfig>().MysteriousTrader;

        private readonly List<string> Names = new List<string>() {
            "Verboten", "Thooloo", "Uri", "Sellatron", "Indigo", "Steve"
        };

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Merchant];
            NPCID.Sets.ExtraFramesCount[NPC.type] = NPCID.Sets.ExtraFramesCount[NPCID.Merchant];
            NPCID.Sets.AttackFrameCount[NPC.type] = NPCID.Sets.AttackFrameCount[NPCID.Merchant];
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 2;
            NPCID.Sets.AttackTime[NPC.type] = 45;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;
            NPCID.Sets.HatOffsetY[NPC.type] = 4;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new(0)
            {
                Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
                Direction = -1
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Merchant;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.PboneUtils.Bestiary.MysteriousTrader")
            });
        }

	public override void OnKill()
	{
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.Find<ModGore>(Mod.Name + "/" + Name + "_GoreHead").Type, 1f);
            for (int i = 0; i < 2; i++)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.Find<ModGore>(Mod.Name + "/" + Name + "_GoreArm").Type, 1f);
            }
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.Find<ModGore>(Mod.Name + "/" + Name + "_GoreLeg").Type, 1f);
        }

	public override bool CanTownNPCSpawn(int numTownNPCs, int money) => false;
        public override bool CanGoToStatue(bool toKingStatue) => false;
        public override List<string> SetNPCNameList() => Names;

        public override string GetChat()
        {
            WeightedRandom<string> chats = new WeightedRandom<string>();

            const int count = 9;
            for (int i = 1; i < count + 1; i++)
            {
                chats.Add(Language.GetTextValue("Mods.PboneUtils.TownChat.MysteriousTrader." + i));
            }

            return chats.Get();
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop) => shop = true;
        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            base.SetupShop(shop, ref nextSlot);

            try
            {
                foreach (int i in PboneWorld.MysteriousTraderShop)
                {
                    shop.AddShopItem(i, ref nextSlot);
                }
            }
            catch (NullReferenceException)
            {
                PboneUtils.Log.Warn("NullRef when trying to populate mysterious trader shop!");
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            if (NPC.downedMoonlord)
                damage = 65;
            else if (Main.hardMode)
                damage = 35;
            else
                damage = 15;

            knockback = 6f;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            base.TownNPCAttackProj(ref projType, ref attackDelay);
            attackDelay = 1;

            if (Main.hardMode)
                projType = ProjectileID.NebulaBlaze2;
            else
                projType = ProjectileID.NebulaBlaze1;
        }


        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 10;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 8f;
            randomOffset = 1f;
        }
    }
}
