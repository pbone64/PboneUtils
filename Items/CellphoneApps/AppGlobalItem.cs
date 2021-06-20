using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace PboneUtils.Items.CellphoneApps
{
    public partial class AppGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public HashSet<string> Apps;

        public override TagCompound Save(Item item)
        {
            if (item.type == ItemID.CellPhone)
            {
                TagCompound tag = new TagCompound();
                tag.Add("AppCount", Apps.Count);

                foreach (string s in Apps)
                {
                    tag.Add("App0", s);
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
                int count = tag.GetAsInt("AppCount");

                for (int i = 0; i < count; i++)
                {
                    Apps.Add(tag.Get<string>("App" + i));
                }
            }
        }
    }
}
