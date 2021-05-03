using Terraria;
using Terraria.ModLoader;
using PboneUtils.Projectiles.Storage;
using PboneUtils.ID;
using System.Collections.Generic;
using PboneUtils.DataStructures;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using PboneUtils.Items.Liquid;
using Terraria.Audio;

namespace PboneUtils
{
    public class PbonePlayer : ModPlayer
    {
        #region Fields
        // Storage
        public bool VoidPig;
        public bool PhilosophersStone;
        public Ref<int> SafeGargoyleChest = new Ref<int>();
        public Ref<bool> SafeGargoyleOpen = new Ref<bool>(false);
        public Ref<int> DefendersCrystalChest = new Ref<int>();
        public Ref<bool> DefendersCrystalOpen = new Ref<bool>(false);

        // Tools
        public bool DeluxeTreasureMagnet;
        public bool TerraTreasureMagnet;
        public bool RunicTreasureMagnet;

        // Item Config
        public Dictionary<string, ItemConfig> ItemConfigs;
        #endregion

        public override void Initialize()
        {
            base.Initialize();
            VoidPig = false;
            PhilosophersStone = false;

            SafeGargoyleChest.Value = -1;
            SafeGargoyleOpen.Value = false;
            DefendersCrystalChest.Value = -1;
            DefendersCrystalOpen.Value = false;

            PhilosophersStone = false;
            DeluxeTreasureMagnet = false;
            TerraTreasureMagnet = false;
            RunicTreasureMagnet = false;

            ItemConfigs = ItemConfig.DefaultConfigs();
        }

        public override TagCompound Save()
        {
            base.Save();
            TagCompound tag = new TagCompound();
            foreach (KeyValuePair<string, ItemConfig> kvp in ItemConfigs)
            {
                tag.Add(kvp.Key, kvp.Value.Save());
            }

            return tag;
        }

        public override void Load(TagCompound tag)
        {
            base.Load(tag);
            foreach (string s in ItemConfigs.Keys)
            {
                ItemConfigs[s].Load(tag, s);
            }
        }

        public override void ResetEffects()
        {
            base.ResetEffects();
            VoidPig = false;

            PhilosophersStone = false;
            DeluxeTreasureMagnet = false;
            TerraTreasureMagnet = false;
            RunicTreasureMagnet = false;
        }

        public override void PreUpdateBuffs()
        {
            base.PreUpdateBuffs();
            if (SafeGargoyleChest.Value >= 0)
                DoPortableChest<PetrifiedSafeProjectile>(ref SafeGargoyleChest, ref SafeGargoyleOpen);
            if (DefendersCrystalChest.Value >= 0)
                DoPortableChest<DefendersCrystalProjectile>(ref DefendersCrystalChest, ref DefendersCrystalOpen);
        }

        public void DoPortableChest<T>(ref Ref<int> whoAmI, ref Ref<bool> toggle) where T : StorageProjectile, new()
        {
            int projectileType = ModContent.ProjectileType<T>();
            T instance = new T();
            int bankID = instance.ChestType;
            LegacySoundStyle useSound = instance.UseSound;

            if (Main.projectile[whoAmI.Value].active && Main.projectile[whoAmI.Value].type == projectileType)
            {
                int oldChest = player.chest;
                player.chest = bankID;
                toggle.Value = true;

                int num17 = (int)((player.position.X + player.width * 0.5) / 16.0);
                int num18 = (int)((player.position.Y + player.height * 0.5) / 16.0);
                player.chestX = (int)Main.projectile[whoAmI.Value].Center.X / 16;
                player.chestY = (int)Main.projectile[whoAmI.Value].Center.Y / 16;
                if ((oldChest != -3 && oldChest != -1) || num17 < player.chestX - Player.tileRangeX || num17 > player.chestX + Player.tileRangeX + 1 || num18 < player.chestY - Player.tileRangeY || num18 > player.chestY + Player.tileRangeY + 1)
                {
                    whoAmI.Value = -1;
                    if (player.chest != -1)
                    {
                        Main.PlaySound(useSound);
                    }

                    if (oldChest != -3)
                        player.chest = oldChest;
                    else
                        player.chest = -1;

                    Recipe.FindRecipes();
                }
            }
            else
            {
                Main.PlaySound(useSound);

                whoAmI.Value = -1;
                player.chest = BankID.None;
                Recipe.FindRecipes();
            }
        }

        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            base.CatchFish(fishingRod, bait, power, liquidType, poolSize, worldLayer, questFish, ref caughtType, ref junk);

            if (junk)
                return;

            FishingChances chances = new FishingChances(player);
            WeightedRandom<int> random = new WeightedRandom<int>();
            random.Add(caughtType);

            switch (liquidType)
            {
                case LiquidID.Water:
                    break;
                case LiquidID.Lava:
                    if (FishingChances.Check(chances.ExtremelyRare))
                    {
                        random.Add(ModContent.ItemType<BottomlessLavaBucket>());
                        random.Add(ModContent.ItemType<HeatAbsorbantSponge>());
                    }
                    break;
                case LiquidID.Honey:
                    if (FishingChances.Check(chances.ExtremelyRare))
                    {
                        random.Add(ModContent.ItemType<BottomlessHoneyBucket>());
                        random.Add(ModContent.ItemType<SuperSweetSponge>());
                    }
                    break;
            }

            caughtType = random.Get();
        }
    }
}
