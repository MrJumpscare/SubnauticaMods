using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Mroshaw.KnifeDamageModSN
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    public class UPPSMOD : BaseUnityPlugin
    {
        private const string myGUID = "upps.coalmod0442";
        private const string pluginName = "UPPS Mod";
        private const string versionString = "1.0.0";

        private static readonly Harmony harmony = new Harmony(myGUID);

        public static ManualLogSource logger;

        private void Awake()
        {
            harmony.PatchAll();
            Logger.LogInfo(pluginName + " " + versionString + " " + "loaded.");
            logger = Logger;
        }
    }
}