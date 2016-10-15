using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod.Items
{
    public class WoodenArrowOfDeath : ModItem
    {
        public override void SetDefaults()
        {
            // Clone defaults of Wooden Arrow
            item.CloneDefaults(ItemID.WoodenArrow);
            item.name = "Wooden Arrow of Death";
            // Set the item to shoot our custom arrow
            item.shoot = mod.ProjectileType("WoodenArrowOfDeath");
            // Same ammo type as wooden arrow
            item.ammo = ProjectileID.WoodenArrowFriendly;
        }

        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            // Same texture as Wooden Arrow
            texture = "Terraria/Item_" + ItemID.WoodenArrow;
            return true;
        }
    }
}
