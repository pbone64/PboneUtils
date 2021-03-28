using log4net;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.UI;

namespace PboneUtils
{
    public partial class PboneUtils : Mod
    {
        public static PboneUtils Instance;
        public static bool TexturesLoaded = false;
        public static ModTextureManager Textures => Instance.textures;
        public static ModRecipeManager Recipes => Instance.recipes;
        public static ModUIManager UI => Instance.ui;
        public static ILog Log => Instance.Logger;

        public ModTextureManager textures;
        public ModRecipeManager recipes;
        public ModUIManager ui;

        public override void Load()
        {
            base.Load();
            Instance = ModContent.GetInstance<PboneUtils>();
            textures = new ModTextureManager();
            recipes = new ModRecipeManager();
            ui = new ModUIManager();

            Load_IL();
            ui.Initialize();
        }

        #region UI
        public override void UpdateUI(GameTime gameTime) => ui.UpdateUI(gameTime);
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) => ui.ModifyInterfaceLayers(layers);
        #endregion

        public override void PostAddRecipes()
        {
            base.PostAddRecipes();
            textures.Initialize();
            TexturesLoaded = true;
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
            Instance = null;
        }
    }
}