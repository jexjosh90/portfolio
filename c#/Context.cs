// <copyright file="Context.cs" author="Joshua Jex">
// Copyright (c) Joshua Jex. All rights reserved. 3/23/2025
// </copyright>
// <summary>
//   Code provides a way for storing the "context" of an object. Context like enviornment, local politics,
//   space debris, etc. can be manifested through a context class.
// </summary>

using JexPod.Abstract.Eviornment;
using JexPod.Normal.Planets;
using JexPod.Normal.Factions;
using JexPod.Normal.Ships;

namespace JexPod.Normal.Contexts;

/// <summary>
///     Provides context for an object. Will store the following items:
///     <list type="bullet">
///         <item>System name</item>
///         <item>System enviorment (solar storms, space debris, warzone, etc.)</item>
///         <item>Nearest planet</item>
///         <item>If someone controls the current context or if it's disputed</item>
///         <item>Since planet stores its own enviorment and other variables, these variables can be accessed through the planet</item>
///     </list>
/// </summary>
public class Context
{
    /// <summary>
    ///     Read only variable that gives the system name of the context.
    ///     <para>The system can be a solar system or any system that needs a defining context like a battle or astroid field.</para>
    /// </summary>
    public string System { get; }

    /// <summary>
    ///     Read only variables that gives the nearest/current planet.
    /// </summary>
    public Planet CurrentPlanet { get; }

    /// <summary>
    ///     Read only variable that gives the enviorment of the system.
    /// </summary>
    public Enviornments SystemEnviornment { get; }

    /// <summary>
    ///     Read only variable that gives the name of the system owner if any or wil be read as "Disputed" if there is no definitve owner.
    /// </summary>
    private Faction SystemOwner;

    /// <summary>
    ///     All the ships in the this context.
    /// </summary>
    private HashSet<Ship> ShipsInContext;

    /// <summary>
    ///     Constructor that builds a context with give a system owner.
    /// </summary>
    /// <param name="system">Name of the system.</param>
    /// <param name="currentPlanet">Nearest/current planet.</param>
    /// <param name="systemEnviornment">System Enviornment</param>
    /// <param name="territoryOwner">Faction that controls the system.</param>
    public Context(string system, Planet currentPlanet, Enviornments systemEnviornment, Faction systemOwner, HashSet<Ship> shipsInContext)
    {
        this.System = system;

        this.CurrentPlanet = currentPlanet;

        this.SystemEnviornment = systemEnviornment;

        this.SystemOwner = systemOwner;

        this.ShipsInContext = shipsInContext;
    }

    /// <summary>
    ///     Constructor that builds a context without a system owner ("Disputed").
    /// </summary>
    /// <param name="system">Name of the system.</param>
    /// <param name="currentPlanet">Nearest/current planet.</param>
    /// <param name="systemEnviornment">System Enviornment</param>
    public Context(string system, Planet currentPlanet, Enviornments systemEnviornment)
    {
        this.System = system;

        this.CurrentPlanet = currentPlanet;

        this.SystemEnviornment = systemEnviornment;

        this.SystemOwner = new Faction("Disputed");

        this.ShipsInContext = new HashSet<Ship>();
    }

    /// <summary>
    ///     Adds a ship to this context.
    /// </summary>
    /// <param name="ship">New ship to add</param>
    /// <returns>True if the ship was added and false if the ship was already present.</returns>
    public bool AddShip(Ship ship)
    {
        return this.ShipsInContext.Add(ship);
    }

    /// <summary>
    ///     Removes a ship from this context.
    /// </summary>
    /// <param name="ship">Ship to remove</param>
    /// <returns>True if the ship was removed and false if it was not present.</returns>
    public bool RemoveShip(Ship ship)
    {
        return this.ShipsInContext.Remove(ship);
    }

    /// <summary>
    ///     Changes the owner of the system.
    /// </summary>
    /// <param name="newSystemOwner"></param>
    /// <returns>True if the owner is changed to a valid faction and false if the given faction is the same
    /// as the current owning faction.</returns>
    public bool ChangeSystemOwner(Faction newSystemOwner)
    {
        if (this.SystemOwner == newSystemOwner)
        {
            return false;
        }

        this.SystemOwner = newSystemOwner;

        return true;
    }
}
