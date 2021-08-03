using log4net;
using PboneLib.CustomLoading;
using PboneLib.Services.CrossMod;
using PboneLib.Services.Net;
using PboneUtils.CrossMod.Call;
using PboneUtils.CrossMod.Ref.Content;
using PboneUtils.DataStructures.MysteriousTrader;
using PboneUtils.Helpers;
using PboneUtils.ID;
using PboneUtils.Packets;
using System.IO;
using Terraria.ModLoader;

namespace PboneUtils
{
    public partial class PboneUtils : Mod
    {
        public override uint ExtraPlayerBuffSlots => (uint)PboneUtilsConfig.Instance.ExtraBuffSlots;

        public static PboneUtils Instance;
        public static ILog Log => Instance.Logger;

        public static PboneUtilsUI UI => Instance.ui;
        public static PboneUtilsTextures Textures => Instance.textures;
        public static ModRecipeManager Recipes => Instance.recipes;
        public static CrossModManager CrossMod => Instance.crossModManager;
        public static PacketManager PacketManager => Instance.packetManager;

        private PboneUtilsUI ui;
        private PboneUtilsTextures textures;
        private ModRecipeManager recipes;
        private CrossModManager crossModManager;
        private PacketManager packetManager;

        public PboneUtils()
        {
            Properties = new ModProperties() {
                Autoload = false
            };
        }

        public override void Load()
        {
            base.Load();
            LoadingHelper.Load();

            Instance = ModContent.GetInstance<PboneUtils>();

            // Instantiate managers
            ui = new PboneUtilsUI();
            textures = new PboneUtilsTextures();
            recipes = new ModRecipeManager();
            packetManager = new PacketManager(this);
            packetManager.RegisterPacketHandler<SyncMysteriousTraderShop>(PacketID.SyncMysteriousTraderShop);

            crossModManager = new CrossModManager();
            crossModManager.CallManager.RegisterHandler<MysteriousTraderShopInterface>();

            crossModManager.RefManager.RegisterCompatibility<FargowiltasCompatibility>();
            crossModManager.RefManager.RegisterCompatibility<FargowiltasSoulsCompatibility>();
            crossModManager.RefManager.RegisterCompatibility<ExtensibleInventoryCompatibility>();
            crossModManager.RefManager.RegisterCompatibility<CensusCompatability>();
            crossModManager.RefManager.RegisterCompatibility<MagicStorageCompatability>();

            // Load MonoMod hooks
            Load_IL();
            Load_On();

            // Adding content
            ContentLoader loader = new ContentLoader((compoundLoadable) => {
                AddContent(compoundLoadable.AsLoadable);
            });

            loader.LoadFromTypes(Code.GetTypes());
        }

        public override object Call(params object[] args) => crossModManager.CallManager.HandleCall(args);

        public override void HandlePacket(BinaryReader reader, int whoAmI) => PacketManager.HandlePacket(reader);

        public override void PostSetupContent()
        {
            base.PostSetupContent();

            // These get assinged to something by their ctors, don't worry
#pragma warning disable CA1806 // Do not ignore method results
            new MysteriousTraderShopManager();
            new CompiledMysteriousTraderShop(MysteriousTraderShopManager.Instance);
#pragma warning restore CA1806 // Do not ignore method results
        }

        #region UI

        #endregion

        public override void AddRecipes()
        {
        }
        public override void AddRecipeGroups()
        {
            base.AddRecipeGroups();
            recipes.AddRecipeGroups();
        }

        public override void Unload()
        {
            base.Unload();

            Instance = null;

            textures = null;
            recipes = null;
            ui = null;
            crossModManager = null;
            packetManager = null;

            if (MysteriousTraderShopManager.Instance != null)
                MysteriousTraderShopManager.Unload();
        }
    }
}