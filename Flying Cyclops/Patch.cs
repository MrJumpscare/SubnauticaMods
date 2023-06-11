using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using Flying_Cyclops.Items.Equipment;
using HarmonyLib;
using UnityEngine;

namespace Flying_Cyclops
{
    [HarmonyPatch(typeof(SubControl), nameof(SubControl.FixedUpdate))]
    public class SubControlPatch
    {
        [HarmonyPostfix]
        static void Postfix(SubControl __instance)
        {
            SubRoot sub = __instance.GetComponent<SubRoot>();

            if (sub == null)
            {
                Plugin.Logger.LogError("ERROR: No SubRoot component of SubControl");
                return;
            }
            else if(sub.isCyclops)
            {
                if (sub.powerRelay.GetPower() <= 0f)
                {
                    sub.worldForces.aboveWaterGravity = 9.81f;
                    sub.worldForces.aboveWaterDrag = 0f;
                    return;
                }
                if (Ocean.GetDepthOf(__instance.gameObject) > 0f)
                {
                    SubRoot playerSub = Player.main.GetCurrentSub();
                    if (playerSub != null && playerSub.isCyclops)
                    {
                        if (playerSub.upgradeConsole.modules.GetCount(cycFlyPrefab.Info.TechType) > 0 && playerSub.powerRelay.GetPower() > 0f)
                        {
                            playerSub.worldForces.aboveWaterGravity = 0f;
                            playerSub.worldForces.aboveWaterDrag = playerSub.worldForces.underwaterDrag;
                        }
                        else
                        {
                            playerSub.worldForces.aboveWaterGravity = 9.81f;
                            playerSub.worldForces.aboveWaterDrag = 0f;
                        }
                    }
                }
            }
        }
    }
    [HarmonyPatch(typeof(Ocean), nameof(Ocean.GetDepthOf), new[] {typeof(GameObject)})]
    public class Ocean_GetDepthOf_Patch
    {

        [HarmonyPostfix]
        static void Postfix(GameObject obj, ref float __result)
        {
            if (Player.main.isPiloting)
            {
                if (obj.name == "Cyclops-MainPrefab(Clone)")
                {
                    if (__result <= 0f)
                    {
                        __result = 5f;
                    }
                }
            }

            return;

        }
    }
    [HarmonyPatch(typeof(CyclopsHelmHUDManager))]
    [HarmonyPatch("Update")]
    public class CyclopsHelmHUDManager_Update_Patch
    {

        [HarmonyPostfix]
        static void Postfix(CyclopsHelmHUDManager __instance)
        {

            if (__instance.subLiveMixin.IsAlive())
            {
                int num = (int)__instance.transform.position.y;
                if (num > 0)
                {
                    Color color = Color.white;
                    __instance.depthText.text = string.Format("Altitude: {0}m", num);
                    __instance.depthText.color = color;
                }
            }

            return;

        }
    }
}
