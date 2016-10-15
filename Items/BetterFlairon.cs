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
    public class BetterFlairon : ModItem
    {
        public override void SetDefaults()
        {
            // Clone vanilla defaults
            item.CloneDefaults(ItemID.Flairon);
            item.name = "Better Flairon";
            item.damage = 500;
        }

        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            // Use vanilla texture
            texture = "Terraria/Item_" + ItemID.Flairon;
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            // Can only use in hardmode
            return Main.hardMode;
        }
    }
}
