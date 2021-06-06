using log4net;
using Microsoft.Xna.Framework;
using PboneUtils.Helpers;
using System.Collections.Generic;
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

            Load_IL();
            Load_On();
            textures.Initialize();
            ui.Initialize();
        }

        public override void PostSetupContent()
        {
            base.PostSetupContent();

            if (PboneUtilsConfig.Instance.AverageBossBags)
                bagValues.Load();
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

        public override void PostAddRecipes()
        {
            base.PostAddRecipes();
            textures.Initialize();
        }

        public override void AddRecipeGroups()
        {
            base.AddRecipeGroups();
            recipes.AddRecipeGroups();
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
        }
    }
}