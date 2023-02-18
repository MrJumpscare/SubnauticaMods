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
            __instance.gameObject.EnsureComponent<HoverMonoBehaviour>();
        }
    }    
}
