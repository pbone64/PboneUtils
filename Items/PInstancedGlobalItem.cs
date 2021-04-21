using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace PboneUtils.Items
{
    public class PInstancedGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public bool AutoswingOrig;
        public bool ChangedAutoswing = false;

        public override void SetDefaults(Item item)
        {
            base.SetDefaults(item);
            AutoswingOrig = item.autoReuse;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (PboneUtilsConfig.Instance.AutoswingOnEverything)
            {
                // Don't autoswing blacklisted items
                if (PboneUtilsConfig.Instance.AutoswingOnEverythingBlacklist != null)
                {
                    foreach (ItemDefinition itemDefinition in PboneUtilsConfig.Instance.AutoswingOnEverythingBlacklist)
                    {
                        if (itemDefinition.Type == item.type)
                        {
                            item.autoReuse = AutoswingOrig;
                            return base.UseItem(item, player);
                        }
                    }
                }

                item.autoReuse = true;
                ChangedAutoswing = true;

                return true;
            }
            else if (!PboneUtilsConfig.Instance.AutoswingOnEverything && ChangedAutoswing)
            {
                item.autoReuse = AutoswingOrig;
                ChangedAutoswing = false;

                return true;
            }
            return base.UseItem(item, player);
        }

        public override GlobalItem NewInstance(Item item) =>
            new PInstancedGlobalItem() {
                AutoswingOrig = item.autoReuse,
                ChangedAutoswing = false
            };
    }
}
