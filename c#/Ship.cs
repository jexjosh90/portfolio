// <copyright file="Context.cs" author="Joshua Jex">
// Copyright (c) Joshua Jex. All rights reserved. 3/23/2025
// </copyright>
// <summary>
//     Represents a vehicle or structure that can move through space.
// </summary>

using JexPod.Abstract.Users;
using JexPod.Abstract.Components;
using JexPod.Abstract.Directions;
using JexPod.Normal.Missions;

namespace JexPod.Normal.Ships;

/// <summary>
///     Defines a ship/vehicle that can:
///     <list type="bullet">
///         <item>Move from planet to another.</item>
///         <item>Recieve and complete missions.</item>
///         <item>Have components added or removed.</item>
///         <item>Have a health between 0.00 and 1.00.S</item>
///         <item>Have a certain class that can be used to define its size and purpose.</item>
///     </list>
/// </summary>
public class Ship
{
    /// <summary>
    ///     Read Only. The number of users if the Ship represents a user ship. The number of people on board if it is a computer controlled ship.
    /// </summary>
    public int Capacity { get; }

    /// <summary>
    ///     Read only variable that is the name of the ship.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Read only that represents the class of the ship as string.
    /// </summary>
    public string ShipClass { get; }

    /// <summary>
    ///     Total mass of the ship.
    /// </summary>
    public int Mass { get; }

    /// <summary>
    ///     Read only enumeration of all the users in the ship. If the ship is computer controlled, the enumeration is empty.
    /// </summary>
    public IEnumerable<BaseUser> ShipUsers { get; }

    /// <summary>
    ///     The health of a ship as a fraction of 100 (i.e. 97% = 0.97). Health is calculated as the average of all components.
    /// </summary>
    private double Health;

    /// <summary>
    ///     The set of all mutable components on the ship.
    /// </summary>
    private HashSet<BaseComponent> ShipComponents;

    /// <summary>
    ///     The current mission or task of the ship. If has no mission or task it should be marked as "None"
    /// </summary>
    private Mission? CurrentMission;

    /// <summary>
    ///     Constructor that build a ship with all fields defined.
    /// </summary>
    /// <param name="capacity">Number of users if a user ship, population if computer controlled ship.</param>
    /// <param name="name">Name of the ship</param>
    /// <param name="shipUsers">An enumeration of all users/stations that will be on the ship.</param>
    /// <param name="shipComponents">Enumeration of the ship components.</param>
    /// <param name="shipClass"></param>
    /// <param name="mission"></param>
    public Ship(int capacity, string name, IEnumerable<BaseUser> shipUsers, HashSet<BaseComponent> shipComponents, string shipClass, Mission mission)
    {
        this.Capacity = capacity;

        this.Name = name;

        this.ShipUsers = shipUsers;

        foreach (BaseComponent shipComponent in shipComponents)
        {
            this.Health += shipComponent.GetHealth();

            this.Mass += shipComponent.Mass;
        }

        this.Health /= shipComponents.Count;

        this.ShipComponents = shipComponents;

        this.ShipClass = shipClass;

        this.CurrentMission = mission;
    }

    /// <summary>
    ///     Constructor that builds a ship without a current mission.
    /// </summary>
    /// <param name="capacity"></param>
    /// <param name="name"></param>
    /// <param name="shipUsers"></param>
    /// <param name="shipComponents"></param>
    /// <param name="shipClass"></param>
    public Ship(int capacity, string name, IEnumerable<BaseUser> shipUsers, HashSet<BaseComponent> shipComponents, string shipClass)
    {
        this.Capacity = capacity;

        this.Name = name;

        this.ShipUsers = shipUsers;

        foreach (BaseComponent shipComponent in shipComponents)
        {
            this.Health += shipComponent.GetHealth();

            this.Mass += shipComponent.Mass;
        }

        this.Health /= shipComponents.Count;

        this.ShipComponents = shipComponents;

        this.ShipClass = shipClass;

        this.CurrentMission = null;
    }

    /// <summary>
    ///     Returns the health of the ship.
    /// </summary>
    /// <returns></returns>
    public double GetHealth()
    {
        return Health;
    }

    /// <summary>
    ///     Retrieves the current mission.
    /// </summary>
    /// <returns></returns>
    public Mission GetMission()
    {
        return CurrentMission ?? new Mission("None", new List<Direction>());
    }

    /// <summary>
    ///     Sets the current mission.
    /// </summary>
    /// <param name="mission"></param>
    public void SetMission(Mission mission)
    {
        CurrentMission = mission;
    }

    /// <summary>
    ///     Returns an enumeration of all the ship components.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BaseComponent> GetComponents()
    {
        return this.ShipComponents;
    }

    /// <summary>
    ///     Adds a component to the ship.
    /// </summary>
    /// <param name="newComponent"></param>
    /// <returns>True if the component was not already present and added, false if otherwise.</returns>
    public bool AddComponent(BaseComponent newComponent)
    {
        if (this.ShipComponents.Contains(newComponent))
        {
            return false;
        }

        this.ShipComponents.Add(newComponent);

        return true;
    }

    /// <summary>
    ///     Removes a component from the ship.
    /// </summary>
    /// <param name="oldComponent"></param>
    /// <returns>True if the component was found and removed and false if otherwise.</returns>
    public bool RemoveComponent(BaseComponent oldComponent)
    {
        return this.ShipComponents.Remove(oldComponent);
    }

    /// <summary>
    ///     Hash code of the ship is determined off its name.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
