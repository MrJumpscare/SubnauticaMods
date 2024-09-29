using HarmonyLib;
using UnityEngine;

namespace Night_Vision_Update
{
    [HarmonyPatch(typeof(Player), nameof(Player.LateUpdate))]
    internal class Player_Update_Patcher
    {
        [HarmonyPostfix]
        public static void ToggleNightVision()
        {
            if (Player.main == null)
                return;
            if (NightVisionChipMain.ambientLight == null)
            {
                NightVisionChipMain.ambientIntensity = RenderSettings.ambientIntensity;
                NightVisionChipMain.ambientLight = RenderSettings.ambientLight;
            }

            if (Inventory.main.equipment.GetCount(NightVisionChip.Info.TechType) <= 0)
            {

                RenderSettings.ambientIntensity = NightVisionChipMain.ambientIntensity;
                RenderSettings.ambientLight = NightVisionChipMain.ambientLight;
            }
            else
            {
                if (NightVisionChipMain.nightVisionEnabled)
                {
                    RenderSettings.ambientIntensity = 100f;
                    RenderSettings.ambientLight = Color.green;
                }
                else
                {
                    RenderSettings.ambientIntensity = NightVisionChipMain.ambientIntensity;
                    RenderSettings.ambientLight = NightVisionChipMain.ambientLight;
                }
            }
        }
    }
}
