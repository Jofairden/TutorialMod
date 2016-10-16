using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TutorialMod
{

    // Follow the tutorial series from here:
    // http://forums.terraria.org/index.php?threads/tutorial-1-your-first-mod.44817/

    class TutorialMod : Mod
    {
        // Create a static mod variable, we can use in places we normally don't have access to a mod variable!
        // This is set in Load()
        public static TutorialMod mod
        {
            get; private set;
        }

        // Make sure Autoload is true, it saves a lot of code in the Load() hook
        public TutorialMod()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true,
                AutoloadBackgrounds = true
            };
        }

        public override void Load()
        {
            // Set mod variable
            mod = this;
        }

        public override void AddRecipes()
        {
            // Our first recipe, transforming a dirtblock to a gold coin while near any workbench.
            ModRecipe recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Wood);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.GoldCoin);
            recipe.AddRecipe();

            // Our second recipe, the same as above, but this time 10 times as fast
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.GoldCoin, 10);
            recipe.AddRecipe();

            // Removing Recipes.
            List<Recipe> rec = Main.recipe.ToList();
            int numberRecipesRemoved = 0;
            // The Recipes to remove.
            numberRecipesRemoved += rec.RemoveAll(x => x.createItem.type == ItemID.AlphabetStatueU); // Remove AlphabetStatueU
            //Change AlphabetStatueT's recipe to create 10 clentaminator from 10 wood
            rec.Where(x => x.createItem.type == ItemID.AlphabetStatueT).ToList().ForEach(s =>
            {
                // Loop the ingredients and reset them
                for (int i = 0; i < s.requiredItem.Length; i++)
                {
                    s.requiredItem[i] = new Item();
                }
                // Change the first ingredient to wood and require 10
                s.requiredItem[0].SetDefaults(ItemID.Wood, false);
                s.requiredItem[0].stack = 10;

                // Set the result as Clentaminator and aquire 10
                s.createItem.SetDefaults(ItemID.Clentaminator, false);
                s.createItem.stack = 10;

                //alternatively, we could use our ResetRecipe() function
            });

            // Filter our recipe list for recipes that use StoneBlock as an ingredient and does not have GoldCoin as ingredient
            // Then proceed to change the Stone Block ingredient to gold coin.
            rec.Where(x => x.requiredItem.Any(i => i.type == ItemID.StoneBlock)).Select((a, b) => new { b, a }).Where(x => !x.a.requiredItem.Any(item => item.type == ItemID.GoldCoin)).ToList().ForEach(s =>
            {
                s.a.requiredItem.Where(x => x.type == ItemID.StoneBlock).Select((a, b) => new { a, b }).ToList().ForEach(y =>
                {
                    var refStack = (object)y.a.stack;
                    y.a.SetDefaults(ItemID.GoldCoin, false);
                    y.a.stack = (int)refStack;
                });
            });

            // Remove all recipes that use Wood as an ingredient and require 10 or more.
            numberRecipesRemoved += rec.RemoveAll(x => x.requiredItem.Any(i => i.type == ItemID.Wood && i.stack >= 10));

            Main.recipe = rec.ToArray(); // recreate array
            Array.Resize(ref Main.recipe, Recipe.maxRecipes); // resize the array
            Recipe.numRecipes -= numberRecipesRemoved; // remove amount of removed recipes

            // Convert any wood at a workbench for a gold coin!
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Wood);
            recipe.anyWood = true;
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.GoldCoin);
            recipe.AddRecipe();

            // While near water, you can magically convert gold coins back to wood!
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.GoldCoin);
            recipe.needWater = true;
            recipe.SetResult(ItemID.Wood);
            recipe.AddRecipe();

            // IGNORE THIS
            // (Gets all ModItem classes, and makes an empty recipe for them)
            var type = typeof(ModItem);
            var exporters = Assembly.GetExecutingAssembly().GetTypes().Where(t => t != type && type.IsAssignableFrom(t));
            if (exporters.Any())
            {
                foreach (var item in exporters)
                {
                    var className = item.Name;
                    var strnamespace = item.Namespace;
                    var rootNamespace = strnamespace.Substring(0, strnamespace.IndexOf('.'));
                    if (rootNamespace == typeof(TutorialMod).Name)
                    {
                        Type t = Type.GetType(strnamespace + "." + className);
                        if (t != null)
                        {
                            recipe = new ModRecipe(this);
                            recipe.SetResult(mod.ItemType(className));
                            recipe.AddRecipe();
                        }
                    }
                }
            }
            // IGNORE ABOVE
        }

        // Fully reset a recipe.
        public static void ResetRecipe(Recipe s)
        {
            for (int i = 0; i < Recipe.maxRequirements; i++)
            {
                s.requiredItem[i] = new Item();
            }
            for (int i = 0; i < Recipe.maxRequirements; i++)
            {
                s.requiredTile[i] = -1;
            }
            s.anyFragment = false;
            s.anyIronBar = false;
            s.anyPressurePlate = false;
            s.anySand = false;
            s.anyWood = false;
            s.needHoney = false;
            s.needLava = false;
            s.needWater = false;
            s.alchemy = false;
            s.acceptedGroups.Clear();
            s.createItem = new Item();
        }

        // Log stuff for testing
        public static void Log(Array array)
        {
            foreach (var item in array)
            {
                ErrorLogger.Log(item.ToString());
            }
        }
    }
}
