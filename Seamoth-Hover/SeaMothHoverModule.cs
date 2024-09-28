using Nautilus.Assets;
using Nautilus.Crafting;
using Nautilus.Utility;
using Nautilus.Assets.PrefabTemplates;
using Nautilus.Assets.Gadgets;
using static CraftData;

namespace SeamothHover
{
    public class SeaMothHoverModule
    {
        public static PrefabInfo Info { get; private set; } = PrefabInfo
            .WithTechType("SeamothHoverModule", "Seamoth Hover Module", "A module that lets the seamoth hover above water")
            .WithIcon(ImageUtils.LoadSpriteFromFile(Main.AssetsFolder + "/SeamothHoverModule.png"));
        public static void Register()
        {
            var customPrefab = new CustomPrefab(Info);

            var SeaHovObj = new CloneTemplate(Info, TechType.SeamothElectricalDefense);
            customPrefab.SetGameObject(SeaHovObj);
            customPrefab.SetRecipe(new RecipeData()
            {
                craftAmount = 1,
                Ingredients =
                {
                    new Ingredient(TechType.PrecursorIonCrystal, 1),
                    new Ingredient(TechType.ComputerChip, 2),
                    new Ingredient(TechType.Benzene, 1),
                    new Ingredient(TechType.Aerogel, 2)
                }
            })
                .WithFabricatorType(CraftTree.Type.SeamothUpgrades)
                .WithStepsToFabricatorTab("SeamothModules");
            customPrefab.SetEquipment(EquipmentType.SeamothModule)
                .WithQuickSlotType(QuickSlotType.Passive);
            customPrefab.SetUnlock(TechType.PrecursorKey_Orange);
            customPrefab.Register();
        }
    }
}
