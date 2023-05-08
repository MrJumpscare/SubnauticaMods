using HarmonyLib;

namespace FireExtinguisherPlus
{
    [HarmonyPatch(typeof(FireExtinguisher), nameof(FireExtinguisher.Update))]
    public static class FireExtPatch
    {
        [HarmonyPostfix]
        public static void Postfix(FireExtinguisher __instance)
        {
            __instance.gameObject.EnsureComponent<FireExtPlusMonoBehaviour>();
        }
    }
}
