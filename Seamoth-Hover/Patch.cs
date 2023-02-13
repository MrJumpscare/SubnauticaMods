using System;
using System.Collections;
using System.Data.Odbc;
using System.Linq;
using System.Runtime.CompilerServices;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace SeamothHover
{
    [HarmonyPatch(typeof(SeaMoth), nameof(SeaMoth.Update))]
    public static class Patch
    {
        [HarmonyPostfix]
        public static void Postfix(SeaMoth __instance) 
        {
            float depth = __instance.gameObject.transform.position.y;
            int count = __instance.modules.GetCount(SeaMothHoverModule.TechTypeID);
            if(Main.config.Mode == SMLConfig.FlyMode.Hover) 
            {
                Hover();
            }
            else
            {
                Flight();
            }
            void Hover()
            {
                if (depth >= Main.config.hoverheight)
                {
                    ErrorMessage.AddDebug("Max Height Reached");
                    ErrorMessage.AddDebug("Disengaging Thrusters");
                    __instance.worldForces.aboveWaterGravity = 119.81f;
                }
                if (depth <= Main.config.hoverheight && __instance.worldForces.underwaterGravity != 0)
                {
                    __instance.worldForces.aboveWaterGravity = 0;
                }
                if (count > 0 && Input.GetKeyDown(Main.config.hovertoggle))
                {
                    if (__instance.worldForces.underwaterGravity == 0)
                    {
                        __instance.worldForces.underwaterGravity = -9.81f;
                        __instance.worldForces.aboveWaterDrag = __instance.worldForces.underwaterDrag;
                        __instance.moveOnLand = true;
                        __instance.worldForces.aboveWaterGravity = 0;
                        ErrorMessage.AddDebug("Hovering ON");
                        ErrorMessage.AddDebug("Maneuver Jets now configured to over water flight.");
                    }
                    else
                    {
                        __instance.worldForces.underwaterGravity = 0f;
                        __instance.worldForces.aboveWaterGravity = 9.81f;
                        __instance.worldForces.aboveWaterDrag = 0f;
                        __instance.moveOnLand = false;
                        ErrorMessage.AddDebug("Hovering OFF");
                    }
                }
            }
            void Flight()
            {
                if (count > 0) 
                {
                    __instance.worldForces.aboveWaterDrag = __instance.worldForces.underwaterDrag;
                    __instance.moveOnLand = true;
                    __instance.worldForces.aboveWaterGravity = 0;
                }
                else
                {
                    __instance.worldForces.aboveWaterGravity = 9.81f;
                    __instance.worldForces.aboveWaterDrag = 0f;
                    __instance.moveOnLand = false;
                }
            }
        }
    }
}
