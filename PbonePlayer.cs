using Terraria;
using Terraria.ModLoader;
using PboneUtils.Projectiles.Storage;
using PboneUtils.ID;
using System.Collections.Generic;
using PboneUtils.DataStructures;
using Terraria.ModLoader.IO;
using Terraria.Audio;

namespace PboneUtils
{
    public class PbonePlayer : ModPlayer
    {
        #region Fields
        // Storage
        public bool VoidPig;
        public int SafeGargoyleChest = -1;
        public bool SafeGargoyleOpen = false;
        public int DefendersCrystalChest = -1;
        public bool DefendersCrystalOpen = false;

        // Magnets
        public bool DeluxeTreasureMagnet;
        public bool HallowedTreasureMagnet;
        public bool SpectreTreasureMagnet;
        public bool RunicTreasureMagnet;
        public int SuperGrabCooldown;

        // Tools
        public bool PhilosophersStone;
        public bool InfiniteMana;

        public float SpawnRateMultiplier;
        public float MaxSpawnsMultiplier;

        // Item Config
        public Dictionary<string, ItemConfig> ItemConfigs;
        #endregion

        public override void Initialize()
        {
            base.Initialize();

            SafeGargoyleChest = -1;
            SafeGargoyleOpen = false;
            DefendersCrystalChest = -1;
            DefendersCrystalOpen = false;

            SuperGrabCooldown = 0;

            ResetVariables();
            ItemConfigs = ItemConfig.DefaultConfigs();
        }
        

        public override void ResetEffects()
        {
            base.ResetEffects();

            ResetVariables();
        }

        public void ResetVariables()
        {
            VoidPig = false;

            DeluxeTreasureMagnet = false;
            HallowedTreasureMagnet = false;
            SpectreTreasureMagnet = false;
            RunicTreasureMagnet = false;

            PhilosophersStone = false;
            InfiniteMana = false;

            SpawnRateMultiplier = 1f;
            MaxSpawnsMultiplier = 1f;
        }

        #region I/O
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
        #endregion

        public override void PostUpdateEquips()
        {
            base.PostUpdateEquips();

            if (InfiniteMana)
            {
                player.maxMinions = 1;
            }
        }

        public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
        {
            base.ModifyManaCost(item, ref reduce, ref mult);

            if (InfiniteMana)
            {
                reduce -= item.mana;
            }
        }

        public override void UpdateDead()
        {
            base.UpdateDead();
            if (PboneUtilsConfig.Instance.FastRespawn)
            {
                player.respawnTimer -= 2;
            }
        }

        public override void PreUpdateBuffs()
        {
            base.PreUpdateBuffs();

            if (SafeGargoyleChest >= 0)
                DoPortableChest<PetrifiedSafeProjectile>(ref SafeGargoyleChest, ref DefendersCrystalOpen);
            if (DefendersCrystalChest >= 0)
                DoPortableChest<DefendersCrystalProjectile>(ref DefendersCrystalChest, ref DefendersCrystalOpen);
        }

        public void DoPortableChest<T>(ref int whoAmI, ref bool toggle) where T : StorageProjectile, new()
        {
            int projectileType = ModContent.ProjectileType<T>();
            T instance = new T();
            int bankID = instance.ChestType;
            LegacySoundStyle useSound = instance.UseSound;

            if (Main.projectile[whoAmI].active && Main.projectile[whoAmI].type == projectileType)
            {
                int oldChest = player.chest;
                player.chest = bankID;
                toggle = true;

                int num17 = (int)((player.position.X + player.width * 0.5) / 16.0);
                int num18 = (int)((player.position.Y + player.height * 0.5) / 16.0);
                player.chestX = (int)Main.projectile[whoAmI].Center.X / 16;
                player.chestY = (int)Main.projectile[whoAmI].Center.Y / 16;
                if ((oldChest != bankID && oldChest != -1) || num17 < player.chestX - Player.tileRangeX || num17 > player.chestX + Player.tileRangeX + 1 || num18 < player.chestY - Player.tileRangeY || num18 > player.chestY + Player.tileRangeY + 1)
                {
                    whoAmI = -1;
                    if (player.chest != -1)
                    {
                        Main.PlaySound(useSound);
                    }

                    if (oldChest != bankID)
                        player.chest = oldChest;
                    else
                        player.chest = -1;

                    Recipe.FindRecipes();
                }
            }
            else
            {
                Main.PlaySound(useSound);

                whoAmI = -1;
                player.chest = BankID.None;
                Recipe.FindRecipes();
            }
        }
    }
}
