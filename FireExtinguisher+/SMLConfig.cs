using Nautilus.Json;
using Nautilus.Options.Attributes;

namespace FireExtinguisherPlus
{
    [Menu("Fire Extinguisher Plus")]
    internal class SMLConfig : ConfigFile
    {
        [Slider("Consumed Fuel per second", Min = 0f, Max = 10f, DefaultValue = 3.5f, Step = 0.1f, Tooltip = "Fuel consumption per second.", Format = "{0:F1}")]
        public float newFuelPerSec = 3.5f;

        [Slider("Fire Exinguishing Rate", Min = 10, Max = 300, DefaultValue = 20, Step = 1, Tooltip = "Amount of fire that is extinguished by the fire extinguisher. Default is 20")]
        public float FireExtRate = 20;

        [Slider("Fuel", Min = 1, Max = 999, Step = 1, DefaultValue = 100, Tooltip = "Amount of fuel the fire extinguisher has.")]
        public float newFuel = 100;

        [Toggle("Max Fuel")]
        public bool MaxFuelEnabled = false;

        [Toggle("ULTIMATE BOOST")]
        public bool Overkill = false;
    }
}
