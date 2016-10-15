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
    public class InstantHealingPotion : ModItem
    {
        public override void SetDefaults()
        {
            // We can clone fields from vanilla items!
            item.CloneDefaults(ItemID.SuperHealingPotion);
            item.name = "Instant health potion";
            item.toolTip = "Consume to instantly heal to full life";
            item.toolTip2 = "Also flips to Hardmode or back to pre-hardmode when consumed";
            item.healLife = 0;
        }

        public override bool CanUseItem(Player player)
        {
            // Make sure we can't consume the potion if we're at max life
            return player.statLife < player.statLifeMax2;
        }

        public override bool UseItem(Player player)
        {
            // When you consume this item, the Main.hardMode bool get swapped (if it was true, turn false, and vice versa)
            Main.hardMode = !Main.hardMode;
            // instead of using item.healLife, we can use custom code
            int amount = player.statLifeMax2; // get amount to heal
            player.statLife = amount; // set life to amount
            player.HealEffect(amount); // do healing effect
            return true;
        }

        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            // Use vanilla texture
            texture = "Terraria/Item_" + ItemID.SuperHealingPotion;
            return true;
        }
    }
}
