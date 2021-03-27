using log4net;
using PboneUtils.Items;
using Terraria.ModLoader;

namespace PboneUtils
{
    public class PboneUtils : Mod
    {
        public static PboneUtils Instance;
        public static bool TexturesLoaded = false;
        public static ModTextureManager Textures => Instance.textures;
        public static ModRecipeManager Recipes => Instance.recipes;
        public static ILog Log => Instance.Logger;

        public ModTextureManager textures;
        public ModRecipeManager recipes;

        public override void Load()
        {
            base.Load();
            Instance = ModContent.GetInstance<PboneUtils>();
            recipes = new ModRecipeManager();
        }

        public override void PostAddRecipes()
        {
            base.PostAddRecipes();
            TexturesLoaded = true;
            textures = new ModTextureManager();
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
            textures.Dispose();
            textures = null;
            Instance = null;
        }
    }
}