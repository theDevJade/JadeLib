using System.Diagnostics;

namespace JadeLib.Patches
{
    using Exiled.API.Features;
    using HarmonyLib;

    [HarmonyPatch(typeof(Player), "ShowHint", typeof(string), typeof(float))]
    public static class PlayerShowHintPatch
    {
        static bool Prefix(Player __instance, ref string message, ref float duration)
        {
            API.SetHints.Display(__instance, 500, message, duration);
            return false;
        }
    }

    [HarmonyPatch(typeof(Player), "ShowHint", [typeof(Hint)])]
    public static class PlayerShowHintHintPatch
    {
        static bool Prefix(Player __instance, Hint hint)
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(1).GetMethod();
            var name = $"{method.GetType().AssemblyQualifiedName}::{method.Name}";
            Log.Debug(name);
            API.SetHints.Display(__instance, 500, hint.Content, hint.Duration);
            return false;
        }
    }
}