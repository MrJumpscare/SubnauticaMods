using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using UnityEngine;
using HarmonyLib;
using UnityEngine.UI;

namespace SeamothHover
{
    [Menu("Seamoth Hover Module")]
    internal class SMLConfig : ConfigFile
    {
        public enum FlyMode
        {
            Hover,
            Flight
        }

        [Choice("Fly Mode", new[] {"Hover", "Flight"})]
        public FlyMode Mode;

        [Keybind("Hover Toggle Keybind")]
        public KeyCode hovertoggle = KeyCode.P;

        [Slider("Maximum Hover Height", Min = 0, Max = 50, DefaultValue = 25, Step = 1f)]
        public float hoverheight = 25.0f;
    }
}
