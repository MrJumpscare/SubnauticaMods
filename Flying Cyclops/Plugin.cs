using BepInEx;
using BepInEx.Logging;
using Flying_Cyclops.Items.Equipment;
using HarmonyLib;
using System.Reflection;
using System.IO;
using Nautilus.Options.Attributes;
using Nautilus.Json;
using Nautilus.Handlers;
using Nautilus.Options;

namespace Flying_Cyclops
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.snmodding.nautilus")]
    public class Plugin : BaseUnityPlugin
    {
        public new static ManualLogSource Logger { get; private set; }

        private static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();
        private static string ModPath = Path.GetDirectoryName(Assembly.Location);
        internal static string AssetsFolder = Path.Combine(ModPath, "Assets");

        internal static SMLconfig config { get; } = OptionsPanelHandler.RegisterModOptions<SMLconfig>();

        private void Awake()
        {
            // set project-scoped logger instance
            Logger = base.Logger;

            // Initialize custom prefabs
            InitializePrefabs();

            KnownTechHandler.UnlockOnStart(cycFlyPrefab.Info.TechType);

            // register harmony patches, if there are any
            Harmony.CreateAndPatchAll(Assembly, $"{PluginInfo.PLUGIN_GUID}");
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void InitializePrefabs()
        {
            cycFlyPrefab.Register();
        }
        public class SMLconfig : ConfigFile
        {
            [Toggle("Unlock Cyclops Flying Module: <alpha=#00>---------------------------------------------------------------------------------------------------</alpha>", Order = 1)]
            public bool unlockDivider = false;

            [Button("Unlock", Order = 2)]
            public void UnlockModule(ButtonClickedEventArgs e) { KnownTech.Add(cycFlyPrefab.Info.TechType); }

            [Toggle("Unlearn Cyclops Flying Module: <alpha=#00>---------------------------------------------------------------------------------------------------</alpha>", Order = 3)]
            public bool unlearnDivider = false;

            [Button("Unlearn", Order = 4)]
            public void RemoveUnlock(ButtonClickedEventArgs e) { KnownTech.Remove(cycFlyPrefab.Info.TechType); }
        }
    }
}
