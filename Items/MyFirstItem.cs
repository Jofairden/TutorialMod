using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod.Items
{
    public class MyFirstItem : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "My First Item";
            item.damage = 50;
            item.width = 40;
            item.height = 40;
            item.toolTip = "This is a modded sword.";
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = 10000;
            item.rare = 2;
            item.useSound = 1;
            item.autoReuse = true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // When we swing, spawn some of our custom dust randomly
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, mod.DustType("TutorialDust"));
            }
        }
    }
}
