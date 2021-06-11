using log4net;
using Microsoft.Xna.Framework;
using PboneUtils.CrossMod;
using PboneUtils.CrossMod.Content;
using PboneUtils.DataStructures;
using PboneUtils.Helpers;
using PboneUtils.Net;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace PboneUtils
{
    public partial class PboneUtils : Mod
    {
        public override uint ExtraPlayerBuffSlots => (uint)PboneUtilsConfig.Instance.ExtraBuffSlots;

        public static PboneUtils Instance;
        public static ILog Log => Instance.Logger;

        public static ModTextureManager Textures => Instance.textures;
        public static ModRecipeManager Recipes => Instance.recipes;
        public static ModUIManager UI => Instance.ui;
        public static TreasureBagValueCalculator BagValues => Instance.bagValues;

        public static bool FargowiltasLoaded => Instance.fargowiltasLoaded;
        public static bool FargowiltasSoulsLoaded => Instance.fargowiltasSoulsLoaded;

        private ModTextureManager textures;
        private ModRecipeManager recipes;
        private ModUIManager ui;
        private TreasureBagValueCalculator bagValues;
        private ModCallManager modCallManager;
        private ModPacketManager modPacketManager;

        private bool fargowiltasLoaded;
        private bool fargowiltasSoulsLoaded;

        public override void Load()
        {
            base.Load();
            LoadingHelper.Load();

            Instance = ModContent.GetInstance<PboneUtils>();
            textures = new ModTextureManager();
            recipes = new ModRecipeManager();
            ui = new ModUIManager();
            bagValues = new TreasureBagValueCalculator();

            fargowiltasLoaded = ModLoader.GetMod("Fargowiltas") != null;
            fargowiltasSoulsLoaded = ModLoader.GetMod("FargowiltasSouls") != null;

            modPacketManager = new ModPacketManager(this);
            modCallManager = new ModCallManager();
            modCallManager.Load();

            Load_IL();
            Load_On();
            textures.Initialize();
            ui.Initialize();
        }

        public override object Call(params object[] args) => modCallManager.HandleCall(args);

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            base.HandlePacket(reader, whoAmI);
            modPacketManager.ReadPacket(reader, whoAmI);
        }

        public override void PostSetupContent()
        {
            base.PostSetupContent();

            if (PboneUtilsConfig.Instance.AverageBossBags)
                bagValues.Load();

            // Mod.Call Key, mod, itemID, rarity, condition
            MysteriousTraderShopManager manager = modCallManager.GetCallHandlerFromType(typeof(MysteriousTraderShopManager)) as MysteriousTraderShopManager;
            manager.Inner_RegisterItem(this, ItemID.RodofDiscord, MysteriousTraderItemRarity.Legendary, new Func<bool>(() => NPC.downedMechBossAny));
            manager.Inner_RegisterItem(this, ItemID.SittingDucksFishingRod, MysteriousTraderItemRarity.Rare, new Func<bool>(() => NPC.downedBoss3));
            manager.Inner_RegisterItem(this, ItemID.BandofRegeneration, MysteriousTraderItemRarity.Common, new Func<bool>(() => true));
            manager.Inner_RegisterItem(this, ItemID.LesserHealingPotion, MysteriousTraderItemRarity.NonUnique, new Func<bool>(() => true));

            // This gets assinged to something by the ctor, don't worry
            new CompiledMysteriousTraderShop(modCallManager.GetCallHandlerFromType(typeof(MysteriousTraderShopManager)) as MysteriousTraderShopManager);
        }

        #region UI
        public override void UpdateUI(GameTime gameTime) => ui.UpdateUI(gameTime);
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) => ui.ModifyInterfaceLayers(layers);
        #endregion

        public override void AddRecipes()
        {
            base.AddRecipes();
            recipes.AddRecipes(this);
        }
        public override void AddRecipeGroups()
        {
            base.AddRecipeGroups();
            recipes.AddRecipeGroups();
        }
        public override void PostAddRecipes()
        {
            base.PostAddRecipes();
            textures.Initialize();
        }

        public override void Unload()
        {
            base.Unload();
            if (textures != null)
                textures.Dispose();
            if (bagValues != null)
                bagValues.Unload();

            Instance = null;

            textures = null;
            recipes = null;
            ui = null;
            bagValues = null;
            modCallManager = null;
            modPacketManager = null;
        }
    }
}