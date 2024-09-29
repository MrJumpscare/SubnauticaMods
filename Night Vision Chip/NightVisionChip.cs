using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Crafting;
using Nautilus.Utility;
using static CraftData;

namespace Night_Vision_Update
{
    public class NightVisionChip
    {
        public static PrefabInfo Info { get; private set; } = PrefabInfo
            .WithTechType("NightVisionChip", "Night Vision HUD", "Adds night vision capabilities to your scuba HUD.")
            .WithIcon(ImageUtils.LoadSpriteFromFile(NightVisionChipMain.AssetsFolder + "/NightVisionChip.png"));
        public static void Register()
        {
            var customPrefab = new CustomPrefab(Info); 

            var NightChipObj = new CloneTemplate(Info, TechType.Compass);
            customPrefab.SetGameObject(NightChipObj);
            customPrefab.SetRecipe(new RecipeData() 
            { 
                craftAmount = 1, 
                Ingredients =
                {
                     new Ingredient(TechType.AdvancedWiringKit, 1),
                     new Ingredient(TechType.Magnetite, 1)
                }
            })
                .WithFabricatorType(CraftTree.Type.Fabricator)
                .WithStepsToFabricatorTab("Personal", "Equipment");
            customPrefab.SetEquipment(EquipmentType.Chip);
            customPrefab.SetPdaGroupCategory(TechGroup.Personal, TechCategory.Equipment);
            customPrefab.Register();
        }
    }
}
