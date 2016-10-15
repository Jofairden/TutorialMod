using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TutorialMod.Dusts
{
    class TutorialDust : ModDust
    {
        // properties set when spawned
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
        }

        // ai (behaviour every tick)
        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.scale -= 0.01f;
            if (dust.scale < 0.75f)
            {
                dust.active = false;
            }
            return false;
        }

        // color of dust
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            // The closer alpha is to 0, the more additive it is
            // and to keep it short, the more it will ignore shadow (think: glowmasks)
            // remember these are byte values, so from 0-255
            return new Color(lightColor.R, lightColor.G, lightColor.B, 100);
        }
    }
}
