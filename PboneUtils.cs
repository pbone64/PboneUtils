using log4net;
using Microsoft.Xna.Framework;
using PboneLib.Core.CrossMod;
using PboneLib.Core.Net;
using PboneLib.Core.UI;
using PboneUtils.CrossMod.Call;
using PboneUtils.CrossMod.Ref.Content;
using PboneUtils.DataStructures.MysteriousTrader;
using PboneUtils.Helpers;
using PboneUtils.ID;
using PboneUtils.Packets;
using PboneUtils.UI.States;
using PboneUtils.UI.States.EndlessBuffToggler;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace PboneUtils
{
    public partial class PboneUtils : Mod
    {
        public override uint ExtraPlayerBuffSlots => (uint)PboneUtilsConfig.Instance.ExtraBuffSlots;

        public static PboneUtils Instance;
        public static ILog Log => Instance.Logger;

        public static UIManager UI => Instance.ui;
        public static ModTextureManager Textures => Instance.textures;
        public static ModRecipeManager Recipes => Instance.recipes;
        public static TreasureBagValueCalculator BagValues => Instance.bagValues;
        public static CrossModManager CrossMod => Instance.crossModManager;
        public static PacketManager PacketManager => Instance.packetManager;

        private UIManager ui;
        private ModTextureManager textures;
        private ModRecipeManager recipes;
        private TreasureBagValueCalculator bagValues;
        private CrossModManager crossModManager;
        private PacketManager packetManager;

        public Guid RadialMenuInterface;
        public Guid BuffTogglerInterface;
        public Guid BuffTogglerButtonInterface;

        public override void Load()
        {
            base.Load();
            LoadingHelper.Load();

            Instance = ModContent.GetInstance<PboneUtils>();

            // Instantiate managers
            ui = new UIManager(this);
            textures = new ModTextureManager();
            recipes = new ModRecipeManager();
            bagValues = new TreasureBagValueCalculator();

            packetManager = new PacketManager(this);
            packetManager.RegisterPacketHandler<SyncMysteriousTraderShop>(PacketID.SyncMysteriousTraderShop);

            crossModManager = new CrossModManager(this);
            crossModManager.CallManager.RegisterHandler<MysteriousTraderShopInterface>();
            crossModManager.CallManager.MapModCallHandlersToMessages();

            crossModManager.RefManager.RegisterCompatibility<FargowiltasCompatibility>();
            crossModManager.RefManager.RegisterCompatibility<FargowiltasSoulsCompatibility>();
            crossModManager.RefManager.RegisterCompatibility<ExtensibleInventoryCompatibility>();
            crossModManager.RefManager.RegisterCompatibility<CensusCompatability>();
            crossModManager.RefManager.RegisterCompatibility<MagicStorageCompatability>();

            // Load MonoMod hooks
            Load_IL();
            Load_On();

            // Load managers that need it
            textures.Initialize();
            if (!Main.dedServ)
            {
                RadialMenuInterface = ui.QuickCreateInterface("Vanilla: Cursor");
                ui.RegisterUI<RadialMenuContainer>(RadialMenuInterface);

                BuffTogglerInterface = ui.QuickCreateInterface("Vanilla: Inventory");
                ui.RegisterUI<BuffTogglerUI>(BuffTogglerInterface);

                BuffTogglerButtonInterface = ui.QuickCreateInterface("Vanilla: Mouse Text");
                ui.RegisterUI<BuffTogglerInventoryButtonUI>(BuffTogglerButtonInterface);

                if (PboneUtilsConfig.Instance.EndlessPotions)
                    ui.OpenUI<BuffTogglerInventoryButtonUI>();
            }

            if (crossModManager.IsModLoaded("Census"))
                crossModManager.GetModCompatibility<CensusCompatability>("Census").Load();
        }

        public override object Call(params object[] args) => crossModManager.CallManager.HandleCall(args);

        public override void HandlePacket(BinaryReader reader, int whoAmI) => PacketManager.HandlePacket(reader);

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
            packetManager = null;

            if (MysteriousTraderShopManager.Instance != null)
                MysteriousTraderShopManager.Unload();
        }
    }
}