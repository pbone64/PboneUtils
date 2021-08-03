using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Tools
{
    public class InfiniteMana : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.InfiniteManaToggle;
        public override bool DrawGlowmask => true;

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Purple;
            item.accessory = true;
            item.value = Item.buyPrice(0, 15, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetModPlayer<InfiniteManaPlayer>().InfiniteMana = true;
            player.allDamage -= 0.35f;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ManaCrystal, 10);
            recipe.AddIngredient(ItemID.Ectoplasm, 7);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class InfiniteManaPlayer : ModPlayer
    {
        public bool InfiniteMana;

        public override void Initialize()
        {
            base.Initialize();
            InfiniteMana = false;
        }

        public override void ResetEffects()
        {
            base.ResetEffects();
            InfiniteMana = false;
        }

        public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
        {
            base.ModifyManaCost(item, ref reduce, ref mult);

            if (InfiniteMana)
            {
                reduce -= item.mana;
            }
        }

        public override void OnMissingMana(Item item, int neededMana)
        {
            base.OnMissingMana(item, neededMana);

            if (InfiniteMana && neededMana > 0)
            {
                player.statMana += neededMana;
                player.ManaEffect(neededMana);
            }
        }

        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            base.OnConsumeMana(item, manaConsumed);

            if (InfiniteMana && manaConsumed > 0)
            {
                player.statMana += manaConsumed;
                player.ManaEffect(manaConsumed);
            }
        }


        public override void PostUpdateEquips()
        {
            base.PostUpdateEquips();

            if (InfiniteMana)
            {
                player.maxMinions = 1;
            }
        }
    }
}
