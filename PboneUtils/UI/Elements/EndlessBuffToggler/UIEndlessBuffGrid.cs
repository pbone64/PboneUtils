using Terraria;
using System.Collections.Generic;
using Terraria.UI;
using PboneUtils.DataStructures;
using System.Linq;
using Microsoft.Xna.Framework;
using System;

namespace PboneUtils.UI.Elements.EndlessBuffToggler
{
    public class UIEndlessBuffGrid : UIElement, IEndlessBuffGridHolder
    {
        public IEndlessBuffGridHolder ParentEndlessBuffGridHolder;

        public UIEndlessBuffGrid(IEndlessBuffGridHolder uiState)
        {
            Width.Set(0, 1f);
            Height.Set(0, 1f);
            SetPadding(0f);

            ParentEndlessBuffGridHolder = uiState;
        }

        public void Rebuild(UIMySearchBar mySearchBar, int rowAmount)
        {
            RemoveAllChildren();

            EndlessPotionPlayer player = Main.LocalPlayer.GetModPlayer<EndlessPotionPlayer>();
            string searchText = mySearchBar.IsEmpty ? "" : mySearchBar.Text;
            int counter = 0;
            Vector2 nextPosition = new Vector2(8);

            foreach (KeyValuePair<int, EndlessBuffSource> kvp in player.EndlessBuffSources)
            {
                string buffName = buffName = Lang.GetBuffName(kvp.Value.Item.buffType);

                string[] words = buffName.Split(' ');

                if (words.Any((s) => mySearchBar.IsEmpty || s.StartsWith(searchText, StringComparison.OrdinalIgnoreCase)))
                {
                    UIEndlessBuffEntry element = new UIEndlessBuffEntry(kvp.Key, kvp.Value);
                    element.Left.Set(nextPosition.X, 0f);
                    element.Top.Set(nextPosition.Y, 0f);

                    if (player.DisabledBuffs.Contains(kvp.Key))
                        element.Disabled = true;

                    nextPosition.X += 2 + 32; // 2 = padding, 32 = buff texture size
                    if (counter > 0 && counter % rowAmount == 0)
                    {
                        nextPosition.X = 8;
                        nextPosition.Y += 2 + 32;
                    }

                    Append(element);
                    counter++;
                }
            }
        }

        public bool HandleBuffEntryClick(UIEndlessBuffEntry entry) => ParentEndlessBuffGridHolder.HandleBuffEntryClick(entry);
        public UIEndlessBuffEntry GetSelectedBuffHolder() => ParentEndlessBuffGridHolder.GetSelectedBuffHolder();
        public void RebuildGrid() => ParentEndlessBuffGridHolder.RebuildGrid();
    }
}
