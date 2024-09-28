using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using System.IO;
using Nautilus.Handlers;

namespace SeamothHover
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    public class Main : BaseUnityPlugin
    {   
        private const string myGUID = "com.mrjumpscare.seamothhovermod";
        private const string pluginName = "SeaMoth Hover Mod";
        private const string versionString = "2.1.0";

        private static Assembly myAssembly = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(myAssembly.Location);
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");

        private static readonly Harmony harmony = new Harmony(myGUID);

        public static ManualLogSource logger;
        internal static SMLConfig config { get; } = OptionsPanelHandler.RegisterModOptions<SMLConfig>();

        private void Awake()
        {
            harmony.PatchAll();
            Logger.LogInfo("SEAMOTH HOVER PATCHED // ENABLING...");
            logger = Logger;
            SeaMothHoverModule.Register();
        }
    }
}
