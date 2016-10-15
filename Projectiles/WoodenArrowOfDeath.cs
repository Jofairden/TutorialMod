using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod.Projectiles
{
    public class WoodenArrowOfDeathProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            // Clone defaults of Wooden Arrow
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            projectile.name = "Wooden Arrow of Death";
            aiType = ProjectileID.WoodenArrowFriendly; // Same AI (behaviour) as wooden arrow
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            // Same texture as Wooden Arrow
            texture = "Terraria/Projectile_" + ProjectileID.WoodenArrowFriendly;
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            // Strike npc hit for its maximum life to instantly kill it
            target.StrikeNPCNoInteraction(target.lifeMax, 0f, -target.direction);
        }
    }
}
