using System.Collections.Generic;
using PboneUtils.DataStructures;
using Terraria.ModLoader.IO;
using PboneLib.Utils;
using PboneLib.CustomLoading.Content.Implementations;

namespace PboneUtils.MiscModsPlayers
{
    public class PbonePlayer : PPlayer
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
        public override void SaveData(TagCompound tag)
        {
            foreach (KeyValuePair<string, ItemConfig> kvp in ItemConfigs)
            {
                tag.Add(kvp.Key, kvp.Value.Save());
            }

            base.SaveData(tag);
        }

        public override void LoadData(TagCompound tag)
        {
            foreach (string s in ItemConfigs.Keys)
            {
                ItemConfigs[s].Load(tag, s);
            }

            base.LoadData(tag);
        }
        #endregion

        public override void UpdateDead()
        {
            base.UpdateDead();
            if (PboneUtilsConfig.Instance.FastRespawn)
            {
                if (PboneUtilsConfig.Instance.FastRespawnDuringBoss || !MiscUtils.AnyBoss())
                    Player.respawnTimer -= 2;
            }
        }
    }
}
