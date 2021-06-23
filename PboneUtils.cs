using log4net;
using Microsoft.Xna.Framework;
using PboneUtils.CrossMod;
using PboneUtils.DataStructures.MysteriousTrader;
using PboneUtils.Helpers;
using PboneUtils.Net;
using System.Collections.Generic;
using System.IO;
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
        public static CrossModManager CrossMod => Instance.crossModManager;
        public static ModPacketManager ModPacket => Instance.modPacketManager;

        private ModTextureManager textures;
        private ModRecipeManager recipes;
        private ModUIManager ui;
        private TreasureBagValueCalculator bagValues;
        private CrossModManager crossModManager;
        private ModPacketManager modPacketManager;

        public override void Load()
        {
            base.Load();
            LoadingHelper.Load();

            Instance = ModContent.GetInstance<PboneUtils>();

            // Instantiate managers
            textures = new ModTextureManager();
            recipes = new ModRecipeManager();
            ui = new ModUIManager();
            bagValues = new TreasureBagValueCalculator();

            modPacketManager = new ModPacketManager(this);
            crossModManager = new CrossModManager();
            crossModManager.Load();

            // Load MonoMod hooks
            Load_IL();
            Load_On();

            // Load managers that need it
            textures.Initialize();
            ui.Initialize();
        }

        public override object Call(params object[] args) => crossModManager.CallManager.HandleCall(args);

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

            // These get assinged to something by their ctors, don't worry
            new MysteriousTraderShopManager();
            new CompiledMysteriousTraderShop(MysteriousTraderShopManager.Instance);
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
            crossModManager = null;
            modPacketManager = null;

            MysteriousTraderShopManager.Unload();
        }
    }
}