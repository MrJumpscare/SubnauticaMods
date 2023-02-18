using System.Collections;
using UnityEngine;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Utility;

namespace SeamothHover
{
    public class SeaMothHoverModule : Equipable
    {
        public static TechType TechTypeID { get; protected set; }
        public SeaMothHoverModule() : base("SeamothHoverModule", "Seamoth Hover Module", "A module that lets the seamoth hover above water")
        {
            OnFinishedPatching += () =>
            {
                TechTypeID = this.TechType;
            };
        }
        public override EquipmentType EquipmentType => EquipmentType.SeamothModule;
        public override TechType RequiredForUnlock => TechType.PrecursorKey_Orange;
        public override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;
        public override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;
        public override CraftTree.Type FabricatorType => CraftTree.Type.SeamothUpgrades;
        public override string[] StepsToFabricatorTab => new string[] {"SeamothModules"};
        public override QuickSlotType QuickSlotType => QuickSlotType.Passive;
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.SeamothElectricalDefense);
            yield return task;
            GameObject prefab = task.GetResult();
            GameObject obj = Object.Instantiate(prefab);
        }
        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients =
                {
                    new Ingredient(TechType.PrecursorIonCrystal, 1),
                    new Ingredient(TechType.ComputerChip, 2),
                    new Ingredient(TechType.Benzene, 1),
                    new Ingredient(TechType.Aerogel, 2)
                },
            };
        }
        protected override Atlas.Sprite GetItemSprite() => ImageUtils.LoadSpriteFromFile(Main.AssetsFolder + "/SeamothHoverModule.png");
    }
}
