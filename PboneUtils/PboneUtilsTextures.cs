using System.Collections.Generic;
using PboneLib.Services.Textures;
using ReLogic.Content;

namespace PboneUtils
{
    public class PboneUtilsTextures : ModTextureManager
    {
        public override Dictionary<string, string> GetImmediateTextures() => new Dictionary<string, string> {
            // UI
            // Radial
            ["RadialButton"] = "PboneUtils/Textures/UI/Radial/Button",
            ["RadialButtonHovered"] = "PboneUtils/Textures/UI/Radial/ButtonRed",
            ["RadialButtonRed"] = "PboneUtils/Textures/UI/Radial/ButtonHover",
            ["RadialButtonRedHovered"] = "PboneUtils/Textures/UI/Radial/ButtonRedHover",
            ["IconLiquid"] = GetRadialPath("Liquid"),
            ["IconLiquidRed"] = GetRadialPath("LiquidRed"),
            ["IconWater"] = GetRadialPath("Water"),
            ["IconLava"] = GetRadialPath("Lava"),
            ["IconHoney"] = GetRadialPath("Honey"),
            ["IconLight"] = GetRadialPath("Light"),
            ["IconWhite"] = GetRadialPath("White"),
            ["IconRed"] = GetRadialPath("Red"),
            ["IconGreen"] = GetRadialPath("Green"),
            ["IconBlue"] = GetRadialPath("Blue"),
            ["IconYellow"] = GetRadialPath("Yellow"),
            ["IconOrange"] = GetRadialPath("Orange"),
            ["IconPurple"] = GetRadialPath("Purple"),

            // Buff Togglers
            ["BuffTogglerInventoryButton"] = "PboneUtils/Textures/UI/BuffToggler/InventoryButton",
            ["BuffTogglerInventoryButton_MouseOver"] = "PboneUtils/Textures/UI/BuffToggler/InventoryButton_MouseOver"
        };

        public override Dictionary<string, string> GetOtherTextures() => new Dictionary<string, string> {
            // EXTRAS
            ["PetrifiedSafeOutline"] = "PboneUtils/Textures/Extras/PetrifiedSafeOutline",
            ["DefendersCrystalOutline"] = "PboneUtils/Textures/Extras/DefendersCrystalOutline",
            ["DefendersCrystalGlowyThing"] = "PboneUtils/Textures/Extras/DefendersCrystalProjectileGlowyThing",

            // MOUNTS
            ["SuperDrillMountBackTexture"] = "PboneUtils/Mounts/SuperDrillMount_Back",
            ["SuperDrillMountFrontTexture"] = "PboneUtils/Mounts/SuperDrillMount_Front",

            // NPCS
            ["MinerAttack"] = "PboneUtils/NPCs/Town/Miner_Attack",

            // TILES
            ["BuffBrazierFlame"] = "PboneUtils/Tiles/BuffBrazierTile_Flame"
        };

        public string GetRadialPath(string name) => $"PboneUtils/Textures/UI/Radial/Icon{name}";
        public IAsset GetRadialButton(bool hover, bool red)
        {
            if (!hover)
                return !red ? CachedAssets["RadialButton"] : CachedAssets["RadialButton"];
            else
                return !red ? CachedAssets["RadialButtonHovered"] : CachedAssets["RadialButtonRedHovered"];
        }
    }
}
