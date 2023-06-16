using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Extensions;
using Nautilus.Handlers;
using Nautilus.Utility;
using UnityEngine;
using Ingredient = CraftData.Ingredient;

namespace Flying_Cyclops.Items.Equipment
{
    public static class cycFlyPrefab
    {
        public static PrefabInfo Info { get; private set; } = PrefabInfo
            .WithTechType("CyclopsFlyingModule", "Cyclops Flying Module", "Module that makes the cyclops fly.")
            .WithIcon(ImageUtils.LoadSpriteFromFile(Plugin.AssetsFolder + "/cycflysprite.png"));
        
        public static void Register()
        {
            var customPrefab = new CustomPrefab(Info);

            var cycFlyObj = new CloneTemplate(Info, TechType.CyclopsThermalReactorModule);
            customPrefab.SetGameObject(cycFlyObj);
            customPrefab.SetRecipe(new RecipeData()
            {
                craftAmount = 1,
                Ingredients = {
                new Ingredient(TechType.PrecursorIonCrystal, 2),
                new Ingredient(TechType.Benzene, 2),
                new Ingredient(TechType.ComputerChip, 3)
                }
            }).WithFabricatorType(CraftTree.Type.CyclopsFabricator);
            customPrefab.SetPdaGroupCategory(TechGroup.Cyclops, TechCategory.CyclopsUpgrades);
            customPrefab.SetEquipment(EquipmentType.CyclopsModule);
            customPrefab.Register();
        }
    }
}
