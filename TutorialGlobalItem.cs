using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod
{
    class TutorialGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            // If item is a Wooden Sword, we change some stats
            if (item.type == ItemID.WoodenSword)
            {
                item.damage = 100;
                item.knockBack += 5f;
            }

            // If item is ranged, decrease damage by 50 (make sure we don't go below 0, probably isn't required)
            // also in the second part, we are making sure to ignore our own items
            if (item.ranged && (item.modItem != null && item.modItem.mod != mod))
            {
                item.damage = Math.Max(0, item.damage - 50);
            }

            // If modItem is not null, we know the item comes from a mod (and also mod will not be null)
            if (item.modItem != null)
            {
                Mod mod = item.modItem.mod; // we can get the mod like this
                // for example, we can check if the mod comes from example mod
                if (mod.Name == "ExampleMod")
                {
                    // mod is ExampleMod, let's add more damage to the example sword
                    if (item.type == mod.ItemType("ExampleSword"))
                    {
                        item.damage += 50; // add 50 damage to example sword
                        // you will see example sword now has 100 damage instead of 50
                    }
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // Let's add a tooltip to all items we modified in SetDefaults
            string msg = "Altered by TutorialMod";
            Color msgColor = Colors.CoinGold;
            bool addMsg = false;
            // these 2 are easy to check
            if (item.type == ItemID.WoodenSword || (item.ranged && (item.modItem != null && item.modItem.mod != mod))
            {
                addMsg = true;
            }
            // this takes slightly more work, since we do this multiple times, we made a method of it. check below
            else if (IsExampleSword(item))
            {
                addMsg = true;
            }

            // you can also write the above if checks shorter like this:
            // addMsg = IsExampleSword(item) || item.type == ItemID.WoodenSword || item.ranged;

            // add our tooltip
            if (addMsg)
            {
                TooltipLine newTooltip = new TooltipLine(mod, "TutorialMod:OurTooltip", msg);
                newTooltip.overrideColor = msgColor; // you can override color like this
                tooltips.Add(newTooltip);
            }
        }

        // If you are doing the same code multiple times, it's wise to make a method of it and call that, like this
        private bool IsExampleSword(Item item)
        {
            if (item.modItem != null)
            {
                Mod mod = item.modItem.mod;
                if (mod.Name == "ExampleMod")
                {
                    if (item.type == mod.ItemType("ExampleSword"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Return true if item is from ExampleMod, else false
        private bool IsExampleModItem(Item item)
        {
            if (item.modItem != null)
            {
                Mod mod = item.modItem.mod;
                if (mod.Name == "ExampleMod")
                {
                    return true;
                }
            }
            return false;
        }

        // Let's say we want any of ExampleMod's melee items to swing with our own dust
        public override void MeleeEffects(Item item, Player player, Rectangle hitbox)
        {
            // Again we made a method to check, see above
            if (IsExampleModItem(item))
            {
                if (Main.rand.Next(3) == 0)
                {
                    int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, mod.DustType("TutorialDust"));
                }
            }
        }
    }
}
