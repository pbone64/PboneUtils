using Terraria.GameContent.Bestiary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneLib.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.GameContent;
using ReLogic.Content;
using Terraria.GameContent.Personalities;

namespace PboneUtils.NPCs.Town
{
    [AutoloadHead]
    public class Miner : PboneUtilsNPC
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.Miner;

        private readonly List<string> Names = new List<string>() {
            "Durin", "Armok", "Tarn", "Asmel", "Doren", "Ber", "Datan"
        };

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Merchant];
            NPCID.Sets.ExtraFramesCount[NPC.type] = NPCID.Sets.ExtraFramesCount[NPCID.Merchant];
            NPCID.Sets.AttackFrameCount[NPC.type] = NPCID.Sets.AttackFrameCount[NPCID.Merchant];
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 3;
            NPCID.Sets.AttackTime[NPC.type] = 20;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;
            NPCID.Sets.HatOffsetY[NPC.type] = 4;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new(0)
            {
                Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
                Direction = -1
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.Happiness
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Love)
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Like)
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Hate)
                .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Like)
                //Princess is automatically set
            ; // < Mind the semicolon!
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
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
				new FlavorTextBestiaryInfoElement("Mods.PboneUtils.Bestiary.Miner")
            });
        }

        public override void OnKill()
        {
            if (Terraria.GameContent.Events.BirthdayParty.PartyIsUp)
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.Find<ModGore>(Mod.Name + "/" + Name + "_GoreHeadParty").Type, 1f);
            else
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.Find<ModGore>(Mod.Name + "/" + Name + "_GoreHead").Type, 1f);

            for (int i = 0; i < 2; i++)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.Find<ModGore>(Mod.Name + "/" + Name + "_GoreArm").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.Find<ModGore>(Mod.Name + "/" + Name + "_GoreLeg").Type, 1f);
            }
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money) => NPC.downedBoss2;
        public override bool CanGoToStatue(bool toKingStatue) => toKingStatue;
        public override ITownNPCProfile TownNPCProfile() => new MinerProfile();
        public override List<string> SetNPCNameList() => Names;

        public override string GetChat()
        {
            WeightedRandom<string> chats = new WeightedRandom<string>();

            const int count = 6;
            for (int i = 1; i < count + 1; i++)
            {
                chats.Add(Language.GetTextValue("Mods.PboneUtils.TownChat.Miner." + i));
            }

            if (Main.hardMode)
                chats.Add(Language.GetTextValue("Mods.PboneUtils.TownChat.Miner.HardmodeOnly"));

            return chats;
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop) => shop = true;
        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            base.SetupShop(shop, ref nextSlot);

            shop.AddShopItem(ItemID.CopperOre, ref nextSlot);
            shop.AddShopItem(ItemID.TinOre, ref nextSlot);
            shop.AddShopItem(ItemID.IronOre, ref nextSlot);
            shop.AddShopItem(ItemID.LeadOre, ref nextSlot);
            shop.AddShopItem(ItemID.SilverOre, ref nextSlot);
            shop.AddShopItem(ItemID.TungstenOre, ref nextSlot);
            shop.AddShopItem(ItemID.GoldOre, ref nextSlot);
            shop.AddShopItem(ItemID.PlatinumOre, ref nextSlot);

            if (NPC.downedBoss3)
            {
                shop.AddShopItem(ItemID.DemoniteOre, ref nextSlot);
                shop.AddShopItem(ItemID.CrimtaneOre, ref nextSlot);
            }

            if (Main.hardMode)
            {
                shop.AddShopItem(ItemID.Obsidian, ref nextSlot);
                shop.AddShopItem(ItemID.Hellstone, ref nextSlot);
            }

            if (Main.hardMode)
            {
                int downedMechBosses = 0;
                if (NPC.downedMechBoss1) downedMechBosses++;
                if (NPC.downedMechBoss2) downedMechBosses++;
                if (NPC.downedMechBoss3) downedMechBosses++;

                if (downedMechBosses > -1)
                {
                    shop.AddShopItem(ItemID.CobaltOre, ref nextSlot);
                    shop.AddShopItem(ItemID.PalladiumOre, ref nextSlot);
                }
                if (downedMechBosses > 0)
                {
                    shop.AddShopItem(ItemID.MythrilOre, ref nextSlot);
                    shop.AddShopItem(ItemID.OrichalcumOre, ref nextSlot);
                }
                if (downedMechBosses > 1)
                {
                    shop.AddShopItem(ItemID.AdamantiteOre, ref nextSlot);
                    shop.AddShopItem(ItemID.TitaniumOre, ref nextSlot);
                }
                if (downedMechBosses > 2)
                    shop.AddShopItem(ItemID.HallowedBar, ref nextSlot);

                if (NPC.downedPlantBoss)
                    shop.AddShopItem(ItemID.ChlorophyteOre, ref nextSlot);

                if (NPC.downedGolemBoss)
                {
                    shop.AddShopItem(ItemID.ShroomiteBar, ref nextSlot);
                    shop.AddShopItem(ItemID.SpectreBar, ref nextSlot);
                }
            }

            shop.AddShopItem(ItemID.Amethyst, ref nextSlot);
            shop.AddShopItem(ItemID.Topaz, ref nextSlot);
            shop.AddShopItem(ItemID.Sapphire, ref nextSlot);
            shop.AddShopItem(ItemID.Emerald, ref nextSlot);

            if (NPC.downedBoss3)
            {
                shop.AddShopItem(ItemID.Ruby, ref nextSlot);
                shop.AddShopItem(ItemID.Diamond, ref nextSlot);
            }

            if (Main.hardMode)
                shop.AddShopItem(ItemID.Amber, ref nextSlot);
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            if (NPC.downedMoonlord)
                damage = 55;
            else if (Main.hardMode)
                damage = 45;
            else
                damage = 15;

            knockback = 6f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 10;
            randExtraCooldown = 5;
        }

        public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
        {
            base.TownNPCAttackSwing(ref itemWidth, ref itemHeight);

            itemWidth = itemHeight = 32;
        }

        public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            base.DrawTownAttackSwing(ref item, ref itemSize, ref scale, ref offset);
            item = PboneUtils.Textures["MinerAttack"];
            itemSize = 32;
        }
    }
    public class MinerProfile : ITownNPCProfile
    {
        public int RollVariation() => 0;
        public string GetNameForVariant(NPC npc) => npc.getNewNPCName();

        public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
        {
            if (npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn)
                return ModContent.Request<Texture2D>((GetType().Namespace + "." + npc.TypeName).Replace('.', '/'));

            if (npc.altTexture == 1)
                return ModContent.Request<Texture2D>((GetType().Namespace + "." + npc.TypeName).Replace('.', '/') + "_Party");

            return ModContent.Request<Texture2D>((GetType().Namespace + "." + npc.TypeName).Replace('.', '/'));
        }

        public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot((GetType().Namespace + "." + npc.TypeName).Replace('.', '/') + "_Head");
    }
}
