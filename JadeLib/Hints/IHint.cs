// <copyright file="IHint.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Exiled.API.Features;
using RueI.Displays;
using RueI.Elements;

namespace JadeLib.Hints;

/// <summary>
/// A versatile dynamic hint.
/// </summary>
public interface IHint
{
    /// <summary>
    /// Gets or sets a dictionary of all players and their respective element.
    /// </summary>
    public Dictionary<Player, Element> Elements { get; set; }

    /// <summary>
    /// Gets or sets the identifier for this hint.
    /// </summary>
    public string UniqueIdentifier { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not this hint should tick.
    /// </summary>
    public bool ShouldTick { get; set; }

    /// <summary>
    /// A function to register listeners, etc.
    /// </summary>
    public void Register();

    /// <summary>
    /// The delegate to retrieve hint content.
    /// </summary>
    /// <param name="core">The player's displaycore.</param>
    /// <returns>The hint</returns>
    public string GetContent(DisplayCore core);

    /// <summary>
    /// If ticking is enabled, what should happen every 0.5 seconds.
    /// </summary>
    public void Tick();
}