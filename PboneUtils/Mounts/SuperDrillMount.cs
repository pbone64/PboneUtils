/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PboneUtils.Buffs;
using PboneUtils.ID;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Mounts
{
    // WIP
    // Idea: UFO mount?
    public class SuperDrillMount : ModMountData
    {
        public struct DrillBeam
        {
            public Point16 TileTarget;
            public int Cooldown;

            public DrillBeam(byte blehParamBecauseStructsCantHaveImplicitParamlessCtors = 0)
            {
                TileTarget = Point16.NegativeOne;
                Cooldown = 0;
            }
        }

        public class DrillData
        {
            public enum DrillMode
            {
                Normal,
                Selection,
                Nuke
            }

            public const int MaxBeams = 16;

            public DrillBeam[] Beams;
            public DrillMode Mode;
            public float MiningDeviceRotation;
            public float DesiredMiningDeviceRotation;
            public float RingRotation;
            public Vector2 MousePosition;

            public DrillData()
            {
                Beams = new DrillBeam[MaxBeams];
                Mode = DrillMode.Normal;

                MiningDeviceRotation = -1f;
                DesiredMiningDeviceRotation = -1f;
                RingRotation = 0f;

                MousePosition = Vector2.One * -1f;

                for (int i = 0; i < MaxBeams; i++)
                {
                    Beams[i] = new DrillBeam();
                }
            }
        }

        public static Vector2 TextureSize = new Vector2(74f);

        public override void SetDefaults()
        {
            base.SetDefaults();

            mountData.spawnDust = DustID.AmberBolt;
            mountData.buff = ModContent.BuffType<SuperDrillMountBuff>();
            mountData.heightBoost = 16;
            mountData.flightTimeMax = 320;
            mountData.fatigueMax = 320;
            mountData.fallDamage = 1f;
            mountData.usesHover = true;
            mountData.swimSpeed = 4f;
            mountData.runSpeed = 6f;
            mountData.dashSpeed = 4f;
            mountData.acceleration = 0.16f;
            mountData.jumpHeight = 10;
            mountData.jumpSpeed = 4f;
            mountData.blockExtraJumps = true;
            mountData.emitsLight = true;
            mountData.lightColor = new Vector3(1f, 0.9f, 0.1f);
            mountData.totalFrames = 1;

            int[] array = new int[mountData.totalFrames];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 4;
            }

            mountData.playerYOffsets = array;
            mountData.xOffset = 1;
            mountData.bodyFrame = 4;
            mountData.yOffset = 4;
            mountData.playerHeadOffset = 18;

            mountData.standingFrameCount = 1;
            mountData.standingFrameDelay = 12;
            mountData.standingFrameStart = 0;

            mountData.runningFrameCount = 1;
            mountData.runningFrameDelay = 12;
            mountData.runningFrameStart = 0;

            mountData.flyingFrameCount = 1;
            mountData.flyingFrameDelay = 12;
            mountData.flyingFrameStart = 0;

            mountData.inAirFrameCount = 1;
            mountData.inAirFrameDelay = 12;
            mountData.inAirFrameStart = 0;

            mountData.idleFrameCount = 0;
            mountData.idleFrameDelay = 12;
            mountData.idleFrameStart = 8;

            mountData.swimFrameCount = 0;
            mountData.swimFrameDelay = 12;
            mountData.swimFrameStart = 0;

            if (Main.netMode != NetmodeID.Server)
            {
                mountData.backTexture = PboneUtils.Textures.Mounts.SuperDrillMountBackTexture;
                mountData.backTextureGlow = PboneUtils.Textures.Mounts.SuperDrillMountBackTexture;
                mountData.backTextureExtra = null;
                mountData.backTextureExtraGlow = null;

                mountData.frontTexture = PboneUtils.Textures.Mounts.SuperDrillMountFrontTexture;
                mountData.frontTextureGlow = null;
                mountData.frontTextureExtra = null;
                mountData.frontTextureExtraGlow = null;

                mountData.textureWidth = mountData.backTexture.Width;
                mountData.textureHeight = mountData.backTexture.Height;
            }
        }

        public override void SetMount(Player player, ref bool skipDust)
        {
            player.mount._mountSpecificData = new DrillData();
        }

        public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
        {
            Player player = Main.LocalPlayer;
            DrillData data = (DrillData)player.mount._mountSpecificData;

            float ringRotation = data.RingRotation;
            ringRotation += player.velocity.X / 160f;
            if (ringRotation > (float)Math.PI)
            {
                ringRotation -= (float)Math.PI * 2f;
            }
            else if (ringRotation < -(float)Math.PI)
            {
                ringRotation += (float)Math.PI * 2f;
            }
            data.RingRotation = ringRotation;

            switch (drawType)
            {
                case MountDrawTypeID.BackTexture:
                    rotation = data.RingRotation;
                    break;
                case MountDrawTypeID.FrontTexture:
                    rotation = Math.Abs(player.velocity.ToRotation());
                    spriteEffects = ((drawPlayer.direction == 1 && drawType == 2) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
                    break;
            }

            for (int i = 0; i < DrillData.MaxBeams; i++)
            {
                if (data.Beams[i].TileTarget == Point16.NegativeOne)
                    continue;
            }

            return true;
        }

        public override void UpdateEffects(Player player)
        {
            player.mount._flyTime = 2;
        }

        public override void AimAbility(Player mountedPlayer, Vector2 mousePosition)
        {
            // TODO Decode this witchcraft
            Vector2 vector = ClampToDeadZone(mountedPlayer, mousePosition) - mountedPlayer.Center;
            float rotation = vector.ToRotation();
            if (rotation < 0f)
                rotation += MathHelper.TwoPi;

            DrillData data = (DrillData)mountedPlayer.mount._mountSpecificData;

            data.DesiredMiningDeviceRotation = rotation;

            float num2 = data.MiningDeviceRotation % MathHelper.TwoPi;
            if (num2 < 0f)
            {
                num2 += MathHelper.TwoPi;
            }
            if (num2 < rotation)
            {
                if (rotation - num2 > MathHelper.Pi)
                {
                    num2 += MathHelper.TwoPi;
                }
            }
            else if (num2 - rotation > MathHelper.Pi)
            {
                num2 -= MathHelper.TwoPi;
            }
            data.MiningDeviceRotation = num2;
            data.MousePosition = mousePosition;
        }

        public Vector2 ClampToDeadZone(Player mountedPlayer, Vector2 position)
        {
            // TODO Decode this witchcraft
            int width = (int)TextureSize.X;
            int height = (int)TextureSize.Y;

            Vector2 center = mountedPlayer.Center;
            position -= center;

            if (position.X > (-width) && position.X < width && position.Y > (-height) && position.Y < (float)height)
            {
                float num3 = width / Math.Abs(position.X);
                float num4 = height / Math.Abs(position.Y);
                if (num3 > num4)
                {
                    position *= num4;
                }
                else
                {
                    position *= num3;
                }
            }
            return position + center;
        }
    }
}*/
