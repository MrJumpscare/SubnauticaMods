using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using SMLHelper.V2.Handlers;

namespace FireExtinguisherPlus
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    public class Main : BaseUnityPlugin
    {
        private const string myGUID = "com.mrjumpscare.fire_extinguisherplus";
        private const string pluginName = "Fire Extinguisher +";
        private const string versionString = "1.0.0";

        private static readonly Harmony harmony = new Harmony(myGUID);

        public static ManualLogSource logger;
        internal static SMLConfig config { get; } = OptionsPanelHandler.RegisterModOptions<SMLConfig>();


        private void Awake()
        {
            harmony.PatchAll();
            Logger.LogInfo("FIRE EXTINGUISHER OVERCLOCKED // ENABLING...");
            logger = Logger;
        }
    }
}
