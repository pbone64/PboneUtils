using Terraria;
using PboneUtils.Projectiles.Storage;
using Terraria.Audio;
using Terraria.ModLoader;
using PboneLib.ID;

namespace PboneUtils.Items.Storage
{
    public class PortableStoragePlayer : ModPlayer
    {
        public int SafeGargoyleChest = -1;
        public bool SafeGargoyleOpen = false;
        public int DefendersCrystalChest = -1;
        public bool DefendersCrystalOpen = false;

        public override void Initialize()
        {
            base.Initialize();

            SafeGargoyleChest = -1;
            SafeGargoyleOpen = false;
            DefendersCrystalChest = -1;
            DefendersCrystalOpen = false;
        }

        public override void PreUpdateBuffs()
        {
            base.PreUpdateBuffs();

            if (SafeGargoyleChest >= 0)
                DoPortableChest<PetrifiedSafeProjectile>(ref SafeGargoyleChest, ref DefendersCrystalOpen);
            if (DefendersCrystalChest >= 0)
                DoPortableChest<DefendersCrystalProjectile>(ref DefendersCrystalChest, ref DefendersCrystalOpen);
        }

        public void DoPortableChest<T>(ref int whoAmI, ref bool toggle) where T : StorageProjectile, new()
        {
            int projectileType = ModContent.ProjectileType<T>();
            T instance = new T();
            int bankID = instance.ChestType;
            LegacySoundStyle useSound = instance.UseSound;

            if (Main.projectile[whoAmI].active && Main.projectile[whoAmI].type == projectileType)
            {
                int oldChest = Player.chest;
                Player.chest = bankID;
                toggle = true;

                int num17 = (int)((Player.position.X + Player.width * 0.5) / 16.0);
                int num18 = (int)((Player.position.Y + Player.height * 0.5) / 16.0);
                Player.chestX = (int)Main.projectile[whoAmI].Center.X / 16;
                Player.chestY = (int)Main.projectile[whoAmI].Center.Y / 16;
                if ((oldChest != bankID && oldChest != -1) || num17 < Player.chestX - Player.tileRangeX || num17 > Player.chestX + Player.tileRangeX + 1 || num18 < Player.chestY - Player.tileRangeY || num18 > Player.chestY + Player.tileRangeY + 1)
                {
                    whoAmI = -1;
                    if (Player.chest != -1)
                    {
                        SoundEngine.PlaySound(useSound);
                    }

                    if (oldChest != bankID)
                        Player.chest = oldChest;
                    else
                        Player.chest = -1;

                    Recipe.FindRecipes();
                }
            }
            else
            {
                SoundEngine.PlaySound(useSound);

                whoAmI = -1;
                Player.chest = BankID.None;
                Recipe.FindRecipes();
            }
        }

    }
}
