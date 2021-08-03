using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Misc
{
    public class StarMagnet : RightClickToggleItem
    {
        public override bool LoadCondition() => PboneUtilsConfig.Instance.StarMagnetToggle;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            player.GetModPlayer<StarMagnetPlayer>().StarMagnet = Enabled;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.FallenStar, 5).AddRecipeGroup(PboneUtils.Recipes.AnyDemoniteBar, 7).AddRecipeGroup(PboneUtils.Recipes.AnyShadowScale, 10).AddTile(TileID.SkyMill).Register();
        }
    }

    public class StarMagnetPlayer : ModPlayer
    {
        public bool StarMagnet;

        public override void Initialize()
        {
            base.Initialize();
            StarMagnet = false;
        }

        public override void ResetEffects()
        {
            base.ResetEffects();
            StarMagnet = false;
        }

        // This being called for every player with star magnet is indeed an entity
        // Having it in ModWorld would entail hellish netcode
        public override void PostUpdate()
        {
            base.PostUpdate();
            if (StarMagnet && !Main.dayTime)
            {
                float num139 = Main.maxTilesX / 4200;
                if (Main.rand.Next(8000) < 10f * num139)
                {
                    int num140 = Main.rand.Next(Main.maxTilesX - 50) + 100;
                    num140 *= 16;
                    int num141 = Main.rand.Next((int)((double)Main.maxTilesY * 0.05));
                    num141 *= 16;
                    Vector2 vector = new Vector2(num140, num141);
                    float num142 = Main.rand.Next(-100, 101);
                    float num143 = Main.rand.Next(200) + 100;
                    float num144 = (float)Math.Sqrt(num142 * num142 + num143 * num143);
                    num144 = 12f / num144;
                    num142 *= num144;
                    num143 *= num144;
                    Projectile.NewProjectile(Projectile.GetNoneSource(), vector.X, vector.Y, num142, num143, ProjectileID.FallingStar, 1000, 10f, Main.myPlayer);
                }
            }
        }
    }
}