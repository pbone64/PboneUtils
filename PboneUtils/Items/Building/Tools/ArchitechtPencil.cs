namespace PboneUtils.Items.Building.Tools
{
    public class ArchitechtPencil : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.BuildingItemToggle;
    }
}
