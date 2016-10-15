using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod.Items
{
    public class BetterTerraBlade : ModItem
    {
        public override void SetDefaults()
        {
            // Clone Defaults
            item.CloneDefaults(ItemID.TerraBlade);
            item.name = "Better Terra Blade";
            item.damage = 500;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            // Add onfire debuff
            target.AddBuff(BuffID.OnFire, 5 * 60);

            // If you somehow hit an town npc, you hurt yourself
            if (target.townNPC)
            {
                player.Hurt(5, -player.direction, false, false, " is an evil twat...");
            }
        }

        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            // This way, the item can use a vanilla texture!
            texture = "Terraria/Item_" + ItemID.TerraBlade;
            return true;
        }
    }
}
