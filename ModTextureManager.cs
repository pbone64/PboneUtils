using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PboneUtils
{
    public class ModTextureManager : IDisposable
    {
        public ExtraTextures Extras;
        public ItemTextures Items;

        public class ExtraTextures : IDisposable
        {
            public Texture2D PetrifiedSafeOutline = ModContent.GetTexture("PboneUtils/Textures/Extras/PetrifiedSafeOutline");

            public void Dispose()
            {
                PetrifiedSafeOutline.Dispose();
            }
        }

        public class ItemTextures : IDisposable
        {
            public Texture2D VoidPigEnabled = ModContent.GetTexture("PboneUtils/Items/Storage/VoidPiggy");
            public Texture2D VoidPigDisabled = ModContent.GetTexture("PboneUtils/Items/Storage/VoidPiggyOff");

            public void Dispose()
            {
                VoidPigEnabled.Dispose();
                VoidPigDisabled.Dispose();
            }
        }


        public void Initialize()
        {
            Extras = new ExtraTextures();
            Items = new ItemTextures();
        }

        public void Dispose()
        {
            Extras.Dispose();
            Items.Dispose();
        }
    }
}
