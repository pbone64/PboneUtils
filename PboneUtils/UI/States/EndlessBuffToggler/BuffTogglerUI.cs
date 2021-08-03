using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneUtils.DataStructures;
using PboneUtils.UI.Elements;
using PboneUtils.UI.Elements.EndlessBuffToggler;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace PboneUtils.UI.States.EndlessBuffToggler
{
    public class BuffTogglerUI : UIState, IEndlessBuffGridHolder
    {
        public const int BackWidth = 400 + BackPanelContentsPadding * 2;
        public const int BackHeight = 475 + 28 + BackPanelContentsPadding * 4; // Height + Searchbar height + padding * 4 (4 because there's for instances of vertical padding between elements)
        public const int BackPanelContentsPadding = 4;
        public const int DescriptionPanelHeight = 220;

        public UIDragablePanel BackPanel;
        public UIMySearchBar SearchBar;
        public UIPanel BuffListPanel;
        public UIPanel BuffDescriptionPanel;
        public UIEndlessBuffGrid BuffGrid;

        public (int buff, EndlessBuffSource source) SelectedBuffInfo = (-1, default);

        public override void OnInitialize()
        {
            // todo panel is in a weird position
            base.OnInitialize();

            // BACK PANEL
            BackPanel = new UIDragablePanel();
            BackPanel.Top.Set(Main.screenHeight / 2f - BackHeight / 2f, 0f);
            BackPanel.Left.Set(Main.screenHeight / 2f - BackHeight / 2f, 0f);
            BackPanel.Width.Set(BackWidth, 0f);
            BackPanel.Height.Set(BackHeight, 0f);

            BackPanel.SetPadding(0f);
            BackPanel.BackgroundColor = new Color(49, 53, 127) * 0.7f;
            Append(BackPanel);

            // SEARCH BAR
            SearchBar = new UIMySearchBar(BackWidth - BackPanelContentsPadding * 2, 28);
            SearchBar.Top.Set(BackPanelContentsPadding, 0f);
            SearchBar.Left.Set(BackPanelContentsPadding, 0f);
            BackPanel.Append(SearchBar);

            // BUFF LIST PANEL
            BuffListPanel = new UIPanel();
            BuffListPanel.Top.Set(BackPanelContentsPadding * 2 + SearchBar.Height.Pixels, 0f);
            BuffListPanel.Left.Set(BackPanelContentsPadding, 0f);
            BuffListPanel.Width.Set(BackWidth - BackPanelContentsPadding * 2f, 0f);
            BuffListPanel.Height.Set(BackHeight - BackPanelContentsPadding * 3f - DescriptionPanelHeight - SearchBar.Height.Pixels, 0f);

            BuffListPanel.SetPadding(0f);
            BuffListPanel.BackgroundColor = new Color(73, 94, 171) * 0.95f;
            BackPanel.Append(BuffListPanel);

            // BUFF GRID
            BuffGrid = new UIEndlessBuffGrid(this);
            BuffListPanel.Append(BuffGrid);

            // BUFF DESCRIPTION PANEL
            BuffDescriptionPanel = new UIPanel();
            BuffDescriptionPanel.Top.Set(BuffListPanel.Top.Pixels + BuffListPanel.Height.Pixels + BackPanelContentsPadding, 0f);
            BuffDescriptionPanel.Left.Set(BackPanelContentsPadding, 0f);
            BuffDescriptionPanel.Width.Set(BackWidth - BackPanelContentsPadding * 2, 0f);
            BuffDescriptionPanel.Height.Set(BackHeight - BackPanelContentsPadding * 5f - BuffListPanel.Height.Pixels - SearchBar.Height.Pixels, 0f);

            BuffDescriptionPanel.PaddingLeft = BackPanel.PaddingRight = BackPanel.PaddingTop = BackPanel.PaddingBottom = 4;
            BuffDescriptionPanel.BackgroundColor = new Color(63, 65, 151) * 0.95f;
            BackPanel.Append(BuffDescriptionPanel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Main.GameUpdateCount % 60 == 0)
            {
                RebuildGrid();
            }

            if (!Main.playerInventory)
                PboneUtils.UI.CloseUI<BuffTogglerUI>();
        }

        public void RebuildGrid()
        {
            BuffGrid.Rebuild(SearchBar, (int)BuffListPanel.Width.Pixels / 40);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Vector2 position = BuffDescriptionPanel.GetDimensions().Position() + new Vector2(4);

            string text = Language.GetTextValue("Mods.PboneUtils.UI.EndlessBuffToggler.DescriptionPlaceholder");
            Color color = Color.Gray;

            if (SelectedBuffInfo.buff != -1)
            {
                // This code is dangerous to your health and you risk cancer reading it
                text =
             $"{Language.GetTextValue("Mods.PboneUtils.UI.EndlessBuffToggler.DescriptionName")}: {Lang.GetBuffName(SelectedBuffInfo.buff)}\n" +
             $"{Language.GetTextValue("Mods.PboneUtils.UI.EndlessBuffToggler.DescriptionSource")}: {Language.GetTextValue($"Mods.PboneUtils.UI.EndlessBuffSource.{SelectedBuffInfo.source.Key}")}\n" +
             $"{Language.GetTextValue("Mods.PboneUtils.UI.EndlessBuffToggler.DescriptionDescription")}: {Lang.GetBuffDescription(SelectedBuffInfo.buff)}\n" +
             $"{(Main.LocalPlayer.GetModPlayer<EndlessPotionPlayer>().DisabledBuffs.Contains(SelectedBuffInfo.buff) ? $"[c/ff0000:{Language.GetTextValue("Mods.PboneUtils.Common.Enabled.False")}]" : $"[c/00ff00:{Language.GetTextValue("Mods.PboneUtils.Common.Enabled.True")}]")} {Language.GetTextValue("Mods.PboneUtils.UI.EndlessBuffToggler.DescriptionHowToDisable")}";

                color = Color.White;
            }

            Utils.DrawBorderString(spriteBatch, text, position, color, 0.85f);
        }

        public bool HandleBuffEntryClick(UIEndlessBuffEntry entry)
        {
            bool alreadySelected = SelectedBuffInfo.buff == entry.Buff;
            SelectedBuffInfo = (entry.Buff, entry.Source);

            if (alreadySelected)
            {
                SelectedBuffInfo = (-1, default);
            }

            return alreadySelected;
        }

        public UIEndlessBuffEntry GetSelectedBuffHolder() => new UIEndlessBuffEntry(SelectedBuffInfo.buff, SelectedBuffInfo.source);
    }
}
