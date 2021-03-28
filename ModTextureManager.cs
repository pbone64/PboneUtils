using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace PboneUtils
{
    public class ModTextureManager : IDisposable
    {
        public ExtraTextures Extras;
        public ItemTextures Items;
        public UITextures UI;

        #region Texture Containers
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
            public void Dispose()
            {
            }
        }

        public class UITextures : IDisposable
        {
            public Texture2D RadialButton = ModContent.GetTexture("PboneUtils/Textures/UI/RadialButton");
            public Texture2D RadialButtonRed = ModContent.GetTexture("PboneUtils/Textures/UI/RadialButtonRed");
            public Texture2D RadialButtonHover = ModContent.GetTexture("PboneUtils/Textures/UI/RadialButtonHover");
            public Texture2D RadialButtonRedHover = ModContent.GetTexture("PboneUtils/Textures/UI/RadialButtonRedHover");

            public Dictionary<string, Texture2D> RadialMenuIcons = new Dictionary<string, Texture2D>() {
                { "Water", getRadialIcon("Water") },
                { "Lava", getRadialIcon("Lava") },
                { "Honey", getRadialIcon("Honey") }
            };

            private static Texture2D getRadialIcon(string name) => ModContent.GetTexture($"PboneUtils/Textures/UI/Radial{name}");

            public Texture2D GetRadialButton(bool hover, bool red)
            {
                if (!hover)
                    return !red ? RadialButton : RadialButtonRed;
                else
                    return !red ? RadialButtonHover : RadialButtonRedHover;
            }

            public void Dispose()
            {
                RadialButton.Dispose();
                RadialButtonRed.Dispose();
                RadialButtonHover.Dispose();
                RadialButtonRedHover.Dispose();

                foreach (KeyValuePair<string, Texture2D> kvp in RadialMenuIcons)
                {
                    kvp.Value.Dispose();
                }
            }
        }
        #endregion

        public void Initialize()
        {
            Extras = new ExtraTextures();
            Items = new ItemTextures();
            UI = new UITextures();
        }

        public void Dispose()
        {
            Extras.Dispose();
            Items.Dispose();
            UI.Dispose();
        }
    }
}
