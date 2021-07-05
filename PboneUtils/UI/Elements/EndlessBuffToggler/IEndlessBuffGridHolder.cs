namespace PboneUtils.UI.Elements.EndlessBuffToggler
{
    public interface IEndlessBuffGridHolder
    {
        bool HandleBuffEntryClick(UIEndlessBuffEntry entry);
        UIEndlessBuffEntry GetSelectedBuffHolder();
        void RebuildGrid();
    }
}
