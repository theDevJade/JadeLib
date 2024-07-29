// <copyright file="CustomHint.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.Extra;
using MEC;
using RueI.Displays;
using RueI.Elements;

namespace JadeLib.Hints;

/// <summary>
/// A extensible CustomHint.
/// </summary>
public abstract class CustomHint : IHint
{
    /// <summary>
    /// Gets or sets feature group for this hint.
    /// </summary>
    private FeatureGroup featureGroup;

    /// <inheritdoc/>
    public Dictionary<Player, Element> Elements { get; set; } = new();

    /// <inheritdoc/>
    public abstract string UniqueIdentifier { get; set; }

    /// <inheritdoc/>
    public abstract bool ShouldTick { get; set; }

    private bool isRegistered = false;

    /// <summary>
    /// Gets or sets the tick coroutine (CAN be null)
    /// </summary>
    public CoroutineHandle? TickCoroutineHandle { get; protected set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomHint"/> class.
    /// </summary>
    protected CustomHint()
    {
        this.Register();
    }

    /// <inheritdoc/>
    public void Register()
    {
        if (this.isRegistered)
        {
            return;
        }

        this.featureGroup = new FeatureGroup(this.UniqueIdentifier);
        this.SupplyEvents(ref this.featureGroup);
        this.featureGroup.Supply(this);
        if (this.ShouldTick)
        {
            this.TickCoroutineHandle = Timing.RunCoroutine(this.TickEnumerator());
        }

        this.featureGroup.Register();

        this.isRegistered = true;
    }

    /// <inheritdoc/>
    public abstract string GetContent(DisplayCore core);

    /// <inheritdoc/>
    public abstract void Tick();

    /// <summary>
    /// A function that registers all external events relating to this hint.
    /// <param name="group">A reference to the feature group.</param>
    /// </summary>
    protected virtual void SupplyEvents(ref FeatureGroup group)
    {
    }

    private IEnumerator<float> TickEnumerator()
    {
        for (; ;)
        {
            this.Tick();
            Timing.WaitForSeconds(0.5f);
        }
    }
}