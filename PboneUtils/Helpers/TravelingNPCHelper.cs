using Microsoft.Xna.Framework;
using PboneLib.Utils;
using System;
using System.Linq;
using Terraria;
using Terraria.Localization;

namespace PboneUtils.Helpers
{
    public static class TravelingNPCHelper
    {
        public static void DoTravellingMerchant(int type, int despawnTime, Func<bool> spawnCondition)
        {
            NPC traveler = Main.npc.FirstOrDefault(n => n.type == type && n.active);
            // This intentionally checks only 0, because I want them to arrive while sundial is sundialling
            if (traveler == null && (Main.dayTime && Main.time == 0)) // Spawn
            {
                if (spawnCondition())
                {
                    NPC arrival = Main.npc[NPC.NewNPC(Main.spawnTileX * 16, Main.spawnTileY * 16, type, 1)];
                    arrival.homeless = true;
                    arrival.direction = Main.spawnTileX >= WorldGen.bestX ? -1 : 1;
                    arrival.netUpdate = true;

                    MiscUtils.NewTextSynced(Language.GetTextValue("Announcement.HasArrived", arrival.FullName), new Color(50, 125, 255));
                }
            }
            else if (Main.time >= despawnTime && traveler != null && !IsNpcOnscreen(traveler.Center))
            {
#pragma warning disable CS0618 // Type or member is obsolete
                MiscUtils.NewTextSynced(Lang.misc[35].Format(traveler.FullName), new Color(50, 125, 255));
#pragma warning restore CS0618 // Type or member is obsolete

                traveler.active = false;
                traveler.netSkip = -1;
                traveler.life = 0;
                traveler = null;
            }
        }

        private static bool IsNpcOnscreen(Vector2 center)
        {
            int w = NPC.sWidth + NPC.safeRangeX * 2;
            int h = NPC.sHeight + NPC.safeRangeY * 2;
            Rectangle npcScreenRect = new Rectangle((int)center.X - w / 2, (int)center.Y - h / 2, w, h);
            foreach (Player player in Main.player)
            {
                // If any player is close enough to the traveling merchant, it will prevent the npc from despawning
                if (player.active && player.getRect().Intersects(npcScreenRect)) return true;
            }
            return false;
        }
    }
}
