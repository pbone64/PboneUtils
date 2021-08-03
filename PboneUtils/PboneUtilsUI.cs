using PboneLib.Services.UI;
using PboneUtils.UI.States;
using PboneUtils.UI.States.EndlessBuffToggler;
using System;

namespace PboneUtils
{
    public class PboneUtilsUI : UIManager
    {
        public Guid RadialMenuInterface;
        public Guid BuffTogglerInterface;
        public Guid BuffTogglerButtonInterface;

        public override void RegisterUI()
        {
            RadialMenuInterface = QuickCreateInterface("Vanilla: Cursor");
            RegisterUI<RadialMenuContainer>(RadialMenuInterface);

            BuffTogglerInterface = QuickCreateInterface("Vanilla: Inventory");
            RegisterUI<BuffTogglerUI>(BuffTogglerInterface);

            BuffTogglerButtonInterface = QuickCreateInterface("Vanilla: Mouse Text");
            RegisterUI<BuffTogglerInventoryButtonUI>(BuffTogglerButtonInterface);

            if (PboneUtilsConfig.Instance.EndlessPotions)
                OpenUI<BuffTogglerInventoryButtonUI>();
        }
    }
}
