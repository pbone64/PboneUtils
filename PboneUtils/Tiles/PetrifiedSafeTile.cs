using Terraria.ObjectData;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.ID;
using PboneUtils.Items.Storage;
using Terraria.Localization;
using System;

namespace PboneUtils.Tiles
{
    public class PetrifiedSafeTile : ModTile
    {
        public static Func<bool> Predicate = () => NPC.downedBoss3;

        public static bool MessageSent = false;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Origin = new Point16(1, 3);
            TileObjectData.newTile.CoordinateHeights = new int[4] {
                16,
                16,
                16,
                16
            };
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            AddMapEntry(Color.DarkGray, name);
            DustType = DustID.Stone;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged) => Predicate();
        public override bool CanExplode(int i, int j) => Predicate();

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            if (PboneUtilsConfig.Instance.StorageItemsToggle)
                Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<PetrifiedSafe>());
            else if (!MessageSent)
            {
                Main.NewText(Language.GetTextValue("Mods.PboneUtils.Message.PetrifiedSafeDisabled1"), Main.OurFavoriteColor);
                Main.NewText(Language.GetTextValue("Mods.PboneUtils.Message.PetrifiedSafeDisabled2"), Main.OurFavoriteColor);
                MessageSent = true;
            }
        }
    }
}
