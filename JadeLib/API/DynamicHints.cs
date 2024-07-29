namespace JadeLib.API
{
    using Exiled.API.Features;
    using RueI.Displays;
    using RueI.Elements;
    using RueI.Elements.Delegates;

    /// <summary>
    /// A utility class that allows easy creation of dynamic hints.
    /// </summary>
    public static class DynamicHints
    {
        /// <summary>
        /// Adds a hint to the player using RueI.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="position">The position (between 0-1000).</param>
        /// <param name="content">The delegate that retrieves the content of this hint.</param>
        /// <returns>The created dynamic element.</returns>
        public static DynamicElement AddElement(Player player, float position, GetContent content)
        {
            var element = new DynamicElement(content, position);
            var display = new Display(DisplayCore.Get(player.ReferenceHub));
            display.Elements.Add(element);
            display.Update();
            return element;
        }

        /// <summary>
        /// Removes an element from a player.
        /// </summary>
        /// <param name="element">The element to remove.</param>
        /// <param name="player">The player to remove the element from.</param>
        public static void RemoveElement(Element element, Player player)
        {
            var display = new Display(DisplayCore.Get(player.ReferenceHub));
            display.Elements.Remove(element);
            display.Update();
        }
    }
}