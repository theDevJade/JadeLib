using JadeLib.API;

namespace JadeLib.Patches
{
    using Exiled.API.Features;
    using HarmonyLib;

    [HarmonyPatch(typeof(Player), "ShowHint", typeof(string), typeof(float))]
    public static class PlayerShowHintPatch
    {
        static bool Prefix(Player __instance, ref string message, ref float duration)
        {
            var method = Util.GetCallingMethod();
            var source = $"{method.ReflectedType?.AssemblyQualifiedName}::{method.Name}";
            SetHints.Display(__instance, 500, message, duration, source);
            return false;
        }
    }

    [HarmonyPatch(typeof(Player), "ShowHint", [typeof(Hint)])]
    public static class PlayerShowHintHintPatch
    {
        static bool Prefix(Player __instance, Hint hint)
        {
            var method = Util.GetCallingMethod();
            var source = $"{method.ReflectedType?.AssemblyQualifiedName}::{method.Name}";
            SetHints.Display(__instance, 500, hint.Content, hint.Duration, source);
            return false;
        }
    }
}