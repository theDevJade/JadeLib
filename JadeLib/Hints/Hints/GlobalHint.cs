// <copyright file="GlobalHint.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.Extra.Events;
using JadeLib.API;
using RueI.Elements;

namespace JadeLib.Hints.Hints;

/// <inheritdoc />
public abstract class GlobalHint : CustomHint
{
    /// <inheritdoc/>
    public override string UniqueIdentifier { get; set; } = "global";

    /// <inheritdoc/>
    public override bool ShouldTick { get; set; } = false;

    /// <summary>
    /// Gets or sets the position of this hint (0-1000)
    /// </summary>
    public abstract int Position { get; set; }

    /// <inheritdoc/>
    public override void Tick()
    {
    }

    [Listener]
    public void OnVerify(VerifiedEventArgs args)
    {
        if (args.Player == null)
        {
            return;
        }

        this.Elements.TryAdd(
            new KeyValuePair<Player, Element>(
                args.Player,
                DynamicHints.AddElement(args.Player, this.Position, this.GetContent)));
    }
}