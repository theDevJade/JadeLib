using UnityEngine;

namespace JadeLib.API
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Exiled.API.Features;
    using RueI.Displays;
    using RueI.Displays.Scheduling;
    using RueI.Elements;
    using RueI.Elements.Enums;
    using Display = RueI.Displays.Display;

    /// <summary>
    /// A utility class that allows creation of reflection-based hints.
    /// </summary>
    public static class SetHints
    {
        private static Dictionary<Tuple<string, Player>, Tuple<JobToken, SetElement>> elements = new();

        /// <summary>
        /// Display's a hint with targeted properties.
        /// </summary>
        /// <param name="player">The Player.</param>
        /// <param name="position">The Position (0-1000).</param>
        /// <param name="hint">The hint in text to send.</param>
        /// <param name="timeTillRemove">The time until it should be removed.</param>
        /// <param name="source">An optional parameter that contains the identifier for the maker of this hint.</param>
        /// <returns>An element with the specified properties.</returns>
        public static SetElement Display(Player player, float position, string hint, float timeTillRemove = 1f, string source = null)
        {
            if (source == null)
            {
                var stackTrace = new StackTrace();
                var method = stackTrace.GetFrame(1).GetMethod();
                source = $"{method.ReflectedType?.AssemblyQualifiedName}::{method.Name}";
            }

            // Check if there's an existing hint from the same source for the same player
            var key = new Tuple<string, Player>(source, player);
            if (elements.TryGetValue(key, out var existingHint))
            {
                // Remove the existing hint and cancel its scheduled removal
                Remove(player, existingHint.Item2);
                Kill(player, existingHint.Item1);
                elements.Remove(key);
            }

            SetElement element = new(position, hint)
            {
                Options = ElementOptions.Vanilla,
            };
            var core = DisplayCore.Get(player.ReferenceHub);
            Display display = new(core);
            display.Elements.Add(element);
            display.Update();
            var token = new JobToken();
            core.Scheduler.Schedule(
                TimeSpan.FromSeconds(timeTillRemove),
                () =>
                {
                    display.Elements.Remove(element);
                    display.Update();
                }, token);

            elements[key] = new Tuple<JobToken, SetElement>(token, element);
            return element;
        }

        private static void Remove(Player player, SetElement element)
        {
            var core = DisplayCore.Get(player.ReferenceHub);
            Display display = new(core);
            display.Elements.Remove(element);
            display.Update();
        }

        private static void Kill(Player player, JobToken token)
        {
            var core = DisplayCore.Get(player.ReferenceHub);
            core.Scheduler.KillJob(token);
        }
    }
}