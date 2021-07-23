using Terraria.ModLoader;
using System.Collections.Generic;
using PboneUtils.DataStructures;
using Terraria.ModLoader.IO;
using PboneUtils.Helpers;

namespace PboneUtils
{
    public class PbonePlayer : ModPlayer
    {
        #region Fields
        // Tools
        public bool VoidPig;
        public bool PhilosophersStone;
        public bool GreedyChest;
        public bool MagicLight;

        public float SpawnRateMultiplier;
        public float MaxSpawnsMultiplier;

        // Item Config
        public Dictionary<string, ItemConfig> ItemConfigs;
        #endregion

        public override void Initialize()
        {
            base.Initialize();

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
            PhilosophersStone = false;
            GreedyChest = false;
            MagicLight = false;

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

        public override void UpdateDead()
        {
            base.UpdateDead();
            if (PboneUtilsConfig.Instance.FastRespawn)
            {
                if (PboneUtilsConfig.Instance.FastRespawnDuringBoss || !MiscVanillaMethods.AnyBoss())
                    player.respawnTimer -= 2;
            }
        }
    }
}
