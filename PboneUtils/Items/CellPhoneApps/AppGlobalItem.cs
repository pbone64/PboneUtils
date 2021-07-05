using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace PboneUtils.Items.CellPhoneApps
{
    public partial class AppGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;
        public override bool NeedsSaving(Item item)
        {
            if (item.type == ItemID.CellPhone)
                return true;

            return base.NeedsSaving(item);
        }

        public List<(int item, string appId)> Apps = new List<(int, string)>();

        public override GlobalItem Clone(Item item, Item itemClone)
        {
            AppGlobalItem gItem = base.Clone(item, itemClone) as AppGlobalItem;
            gItem.Apps = Apps;

            return gItem;
        }

        public override GlobalItem NewInstance(Item item)
        {
            AppGlobalItem gItem = base.NewInstance(item) as AppGlobalItem;
            gItem.Apps = new List<(int item, string appId)>();

            return gItem;
        }

        public override TagCompound Save(Item item)
        {
            if (item.type == ItemID.CellPhone)
            {
#pragma warning disable IDE0028 // Simplify collection initialization
                TagCompound tag = new TagCompound();
#pragma warning restore IDE0028 // Simplify collection initialization
                tag.Add("AppCount", Apps.Count);

                for (int i = 0; i < Apps.Count; i++)
                {
                    tag.Add("AppId" + i, Apps[i].appId);
                    tag.Add("AppItem" + i, Apps[i].item);
                }

                return tag;
            }

            return base.Save(item);
        }

        public override void Load(Item item, TagCompound tag)
        {
            base.Load(item, tag);

            if (item.type == ItemID.CellPhone)
            {
                Apps.Clear();

                int count = tag.GetAsInt("AppCount");

                for (int i = 0; i < count; i++)
                {
                    Apps.Add(
                        (tag.Get<int>("AppItem" + i),
                        tag.Get<string>("AppId" + i))
                        );
                }
            }
        }

        public override bool CanRightClick(Item item)
        {
            if (item.type == ItemID.CellPhone)
                return Main.mouseItem.modItem is AppItem app && !Apps.Contains((app.BaseID, app.AppName));

            return base.CanRightClick(item);
        }

        public override void RightClick(Item item, Player player)
        {
            base.RightClick(item, player);

            if (item.type == ItemID.CellPhone)
            {
                if (Main.mouseItem.modItem is AppItem app)
                {
                    if (Apps.Contains((app.BaseID, app.AppName)))
                        return;

                    Apps.Add((app.BaseID, app.AppName));
                    Main.mouseItem.TurnToAir();
                }
            }
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            if (item.type == ItemID.CellPhone)
                return false;

            return base.ConsumeItem(item, player);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(item, tooltips);
            if (item.type == ItemID.CellPhone)
            {
                tooltips.Add(new TooltipLine(mod, "PboneUtils:CellPhoneInfo", Language.GetTextValue("Mods.PboneUtils.Common.CellPhoneInfo")));

                foreach ((int item, string appId) tuple in Apps)
                {
                    tooltips.Add(
                        new TooltipLine(mod, "PboneUtils:CellPhoneAppDescription-" + tuple.appId.GetHashCode(),
                        Language.GetTextValue("Mods.PboneUtils.Common.CellPhone." + tuple.appId)));
                }
            }
        }

        public override bool AltFunctionUse(Item item, Player player)
        {
            if (item.type == ItemID.CellPhone)
                return Apps.Contains((ItemID.TeleportationPotion, "Teleportation"));

            return base.AltFunctionUse(item, player);
        }

        public override bool UseItem(Item item, Player player)
        {
            if (item.type == ItemID.CellPhone && player.altFunctionUse == 2 && Apps.Contains((ItemID.TeleportationPotion, "Teleportation")))
            {
                player.TeleportationPotion();
                return true;
            }

            return base.UseItem(item, player);
        }
    }
}
