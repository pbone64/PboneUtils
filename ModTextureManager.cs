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
        public MountTextures Mounts;
        public UITextures UI;

        #region Texture Containers
        public class ExtraTextures : IDisposable
        {
            public Texture2D PetrifiedSafeOutline = ModContent.GetTexture("PboneUtils/Textures/Extras/PetrifiedSafeOutline");
            public Texture2D DefendersCrystalOutline = ModContent.GetTexture("PboneUtils/Textures/Extras/DefendersCrystalOutline");
            public Texture2D DefendersCrystalGlowyThing = ModContent.GetTexture("PboneUtils/Textures/Extras/DefendersCrystalProjectileGlowyThing");

            public void Dispose()
            {
                PetrifiedSafeOutline.Dispose();
                DefendersCrystalOutline.Dispose();
                DefendersCrystalGlowyThing.Dispose();
            }
        }

        public class ItemTextures : IDisposable
        {
            public void Dispose()
            {
            }
        }

        public class MountTextures : IDisposable
        {
            public Texture2D SuperDrillMountBackTexture = ModContent.GetTexture("PboneUtils/Mounts/SuperDrillMount_Back");
            public Texture2D SuperDrillMountFrontTexture = ModContent.GetTexture("PboneUtils/Mounts/SuperDrillMount_Front");

            public void Dispose()
            {
                SuperDrillMountBackTexture.Dispose();
                SuperDrillMountFrontTexture.Dispose();
            }
        }

        public class UITextures : IDisposable
        {
            public Texture2D RadialButton = ModContent.GetTexture("PboneUtils/Textures/UI/Radial/Button");
            public Texture2D RadialButtonRed = ModContent.GetTexture("PboneUtils/Textures/UI/Radial/ButtonRed");
            public Texture2D RadialButtonHover = ModContent.GetTexture("PboneUtils/Textures/UI/Radial/ButtonHover");
            public Texture2D RadialButtonRedHover = ModContent.GetTexture("PboneUtils/Textures/UI/Radial/ButtonRedHover");
            public Dictionary<string, Texture2D> RadialMenuIcons = new Dictionary<string, Texture2D>() {
                { "Liquid", getRadialIcon("Liquid") },
                { "LiquidRed", getRadialIcon("LiquidRed") },
                { "Water", getRadialIcon("Water") },
                { "Lava", getRadialIcon("Lava") },
                { "Honey", getRadialIcon("Honey") }
            };

            public Texture2D BuffTogglerInventoryButton = ModContent.GetTexture("PboneUtils/Textures/UI/BuffToggler/InventoryButton");
            public Texture2D BuffTogglerInventoryButton_MouseOver = ModContent.GetTexture("PboneUtils/Textures/UI/BuffToggler/InventoryButton_MouseOver");

            private static Texture2D getRadialIcon(string name) => ModContent.GetTexture($"PboneUtils/Textures/UI/Radial/Icon{name}");

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

                BuffTogglerInventoryButton.Dispose();
                BuffTogglerInventoryButton_MouseOver.Dispose();
            }
        }
        #endregion

        public void Initialize()
        {
            Extras = new ExtraTextures();
            Items = new ItemTextures();
            Mounts = new MountTextures();
            UI = new UITextures();
        }

        public void Dispose()
        {
            Extras.Dispose();
            Items.Dispose();
            Mounts.Dispose();
            UI.Dispose();
        }
    }
}
