using log4net;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using PboneUtils.Items;
using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace PboneUtils
{
    public partial class PboneUtils : Mod
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
            IL.Terraria.Player.Update += Player_Update;
            Instance = ModContent.GetInstance<PboneUtils>();
            recipes = new ModRecipeManager();
        }

        private void Player_Update(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (!c.TryGotoNext(instr => instr.MatchLdcR4(1f) && instr.Next.Next.Next.Next.Next.Next.MatchStfld(typeof(Player).GetField("chest"))))
            {
                throw new Exception("Unable to match IL to patch Terraria.Player::Update");
            }

            c.FindNext(out ILCursor[] cursors, instr => instr.MatchLdcR4(1f));
            c = cursors[0];

            c.Index++;
            c.EmitDelegate<Func<float, float>>((volume) => {
                if (Main.LocalPlayer.GetModPlayer<PbonePlayer>().SafeGargoyleOpen)
                {
                    return 0f;
                }

                return volume;
            });
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