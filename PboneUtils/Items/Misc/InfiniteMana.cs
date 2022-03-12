using PboneLib.CustomLoading.Content.Implementations.Misc;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Misc
{
    public class InfiniteMana : PboneUtilsItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.InfiniteManaToggle;
        public override bool DrawGlowmask => true;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.value = Item.buyPrice(0, 15, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetModPlayer<InfiniteManaPlayer>().InfiniteMana = true;
            player.GetDamage(DamageClass.Generic) -= 0.35f;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.ManaCrystal, 10).AddIngredient(ItemID.Ectoplasm, 7).AddTile(TileID.MythrilAnvil).Register();
        }
    }

    public class InfiniteManaPlayer : PPlayer
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
                Player.statMana += neededMana;
                Player.ManaEffect(neededMana);
            }
        }

        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            base.OnConsumeMana(item, manaConsumed);

            if (InfiniteMana && manaConsumed > 0)
            {
                Player.statMana += manaConsumed;
                Player.ManaEffect(manaConsumed);
            }
        }


        public override void PostUpdateEquips()
        {
            base.PostUpdateEquips();

            if (InfiniteMana)
            {
                Player.maxMinions = 1;
            }
        }
    }
}
