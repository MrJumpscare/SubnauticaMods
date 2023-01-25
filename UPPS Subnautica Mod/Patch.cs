using System;
using HarmonyLib;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Interfaces;
using SMLHelper.V2.Utility;
using SMLHelper.V2.Crafting;

namespace UPPS_Subnautica_Mod
{   
    public abstract class test : PdaItem
    {
        internal ICraftTreeHandler CraftTreeHandler { get; set; } = Handlers.CraftTreeHandler.Main;

        public virtual CraftTree.Type FabricatorType => CraftTree.Type.None;

        public virtual string[] StepsToFabricatorTab => null;

        public virtual float CraftingTime => 0f;

        protected Craftable(string classId, string friendlyName, string description)
           : base(classId, friendlyName, description)
        {
            CorePatchEvents += PatchCraftingData;
        }

    }
}