using Nautilus.Json;
using Nautilus.Options.Attributes;
using UnityEngine;
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

        [Choice("Fly Mode", new[] { "Hover", "Flight" })]
        public FlyMode Mode;

        [Keybind("Hover Toggle Keybind")]
        public KeyCode hovertoggle = KeyCode.P;

        [Toggle("Keep Seamoth in air after exiting", Tooltip = "Uncheck this to drop your seamoth")]
        public bool keepFlying = true;

        [Slider("Maximum Hover Height", Min = 0, Max = 50, DefaultValue = 25, Step = 1f)]
        public float hoverheight = 25.0f;

        [Slider("Power Consumption Multiplier", Min = 1, Max = 10f, DefaultValue = 2f, Step = 0.1f, Tooltip = "Extra power consumption multiplier. No extra power is consumed when set to 1", Format = "{0:F1}")]
        public float powerconsumption = 2f;
    }
}
