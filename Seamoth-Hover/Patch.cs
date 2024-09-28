using HarmonyLib;

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
