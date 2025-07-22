// <copyright file="Context.cs" author="Joshua Jex">
// Copyright (c) Joshua Jex. All rights reserved. 3/23/2025
// </copyright>
// <summary>
//     Describes the typical planet with a unilateral planetary enviornment, a set of factions, connecting planets and resources.
// </summary>

using JexPod.Normal.Factions;
using JexPod.Abstract.Environnment;

namespace JexPod.Normal.Planets;

/// <summary>
///     Represents the fundamental data of planet.
/// </summary>
public class Planet
{
    /// <summary>
    ///     Read only. Name of the planet.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Read only. The enviornment of the planet.
    /// </summary>
    public BaseEnvironment PlanetEnviornment { get; }

    /// <summary>
    ///     A set that contains all the factions on this planet as well as their accompanying percentage of how much ownership they have
    ///     over the planet.
    /// </summary>
    private Dictionary<Faction, double> Factions;

    /// <summary>
    ///     A hash set of all the connecting planets. Each planetary pathway is assumed to be equally weighted and two way.
    ///     (i.e. if planet1 has a connection to planet2 then planet2 has a connection to planet1.
    /// </summary>
    private HashSet<Planet> ConnectingPlanets;

    /// <summary>
    ///     A map that represents all of the available resources the planet has to offer as well as its accompanying amount. The amount units and value is
    ///     dictated by the resource (g, kg, t).
    /// </summary>
    private Dictionary<string, double> Resources;

    /// <summary>
    ///     Constructor that builds a planet with all field variables being assigned to given parameters at initialization.
    /// </summary>
    /// <param name="name">Name of the planet.</param>
    /// <param name="factions">A map of all the factions and their percentage control. Percentage should be given as a fraction of 100.
    /// i.e. 0.97 = 97%, 1 = 100% etc</param>
    /// <param name="planetEnviornment">Enviornment that defines the whole planet.</param>
    /// <param name="connectingPlanets">Every planet immediately adjacent to this one.</param>
    /// <param name="resources">All available and playable resources.</param>
    public Planet(string name, Dictionary<Faction, double> factions, BaseEnvironment planetEnviornment, HashSet<Planet> connectingPlanets, Dictionary<string, double> resources)
    {
        this.Name = name;

        this.Factions = factions;

        this.PlanetEnviornment = planetEnviornment;

        this.ConnectingPlanets = connectingPlanets;

        this.Resources = resources;
    }

    /// <summary>
    ///     Constructor that builds a planet with a given name, connecting planets, enviornment, and resource set. No factions on planet in initialization.
    /// </summary>
    /// <param name="name">Name of the planet.</param>
    /// <param name="connectingPlanets">Every planet immediately adjacent to this one.</param>
    /// i.e. 0.97 = 97%, 1 = 100% etc</param>
    /// <param name="planetEnviornment">Enviornment that defines the whole planet.</param>
    /// <param name="resources">All available and playable resources.</param>
    public Planet(string name, HashSet<Planet> connectingPlanets, BaseEnvironment planetEnviornment, Dictionary<string, double> resources)
    {
        this.Name= name;

        this.Factions = new Dictionary<Faction, double>();

        this.PlanetEnviornment = planetEnviornment;

        this.ConnectingPlanets = connectingPlanets;

        this.Resources = resources;
    }

    /// <summary>
    ///     Constructor that builds a planet with a given name, enviorment, and resource set. No factions and no connecting planets on initialization.
    /// </summary>
    /// <param name="name">Name of the planet.</param>
    /// <param name="planetEnviornment">Enviornment that defines the whole planet.</param>
    /// <param name="resources">All available and playable resources.</param>
    public Planet(string name, BaseEnvironment planetEnviornment, Dictionary<string, double> resources)
    {
        this.Name = name;

        this.Factions = new Dictionary<Faction, double>();

        this.PlanetEnviornment = planetEnviornment;

        this.ConnectingPlanets = new HashSet<Planet>();

        this.Resources = resources;
    }

    /// <summary>
    ///     Adds a faction to planet based off the given percentage of control that faction will have.
    ///     Other faction percentages will readjust for the new faction automatically.
    /// </summary>
    /// <param name="faction"></param>
    /// <param name="percentage"></param>
    /// <returns>True if the faction was not already present on the planet.</returns>
    public bool AddFaction(Faction faction, double percentage)
    {
        if (this.Factions.ContainsKey(faction))
        {
            return false;
        
        }
        this.Factions.Add(faction, percentage);

        return true;
    }

    /// <summary>
    ///     Removes a faction from planet.
    ///     Other faction percentages will readjust percentages automatically.
    /// </summary>
    /// <param name="faction"></param>
    /// <param name="percentage"></param>
    /// <returns>True if the faction was present on the planet and successfully removed.</returns>
    public bool RemoveFaction(Faction faction)
    {
        return this.Factions.Remove(faction);
    }

    /// <summary>
    ///     Adds a connecting planet to the current planet.
    /// </summary>
    /// <param name="newPlanet"></param>
    /// <returns>True if that connecting planet was not already connected. False if otherwise.</returns>
    public bool AddConnectingPlanet(Planet newPlanet)
    {
        return this.ConnectingPlanets.Add(newPlanet);
    }

    /// <summary>
    ///     Removes a connecting planet from the current planet.
    /// </summary>
    /// <param name="oldPlanet"></param>
    /// <returns>True if the given planet was connected and successfully removed. False otherwise.</returns>
    public bool RemoveConnectingPlanet(Planet oldPlanet)
    {
        return this.ConnectingPlanets.Remove(oldPlanet);
    }

    /// <summary>
    ///     Removes a given amount of the resource indicated.
    ///     <para>If resource level reaches 0 the resource will be remvoed from the set.</para>
    ///     <para>There is no upper limit to resource limit.</para>
    /// </summary>
    /// <param name="resourceName"></param>
    /// <param name="amount"></param>
    /// <exception cref="ArgumentException"></exception>
    public void RemoveResource(string resourceName, double amount)
    {
        if (this.Resources.TryGetValue(resourceName, out double resource))
        {
            if (resource is double.NaN || resource - amount < 0)
            {
                throw new ArgumentException("Amount removed exceed amount present");
            }

            this.Resources[resourceName] -= amount;
        }
    }

    /// <summary>
    ///     Adds a resource with a given amount. If the resource is already present, the amount will be added to the current amount.
    ///     If the resource is not present, the amount will be set as the current amount of that resource.
    /// </summary>
    /// <param name="resourceName"></param>
    /// <param name="amount"></param>
    public void AddResource(string resourceName, double amount)
    {
        if (this.Resources.TryGetValue(resourceName, out double resource))
        {
            this.Resources[resourceName] += amount;
        }
        else
        {
            this.Resources.Add(resourceName, amount);
        }
    }

    /// <returns>An enumeration of all of the faction on the planet.</returns>
    public IEnumerable<Faction> GetFactions()
    {
        return this.Factions.Keys;
    }

    /// <returns>An enumeration of all of the resource names on the planet.</returns>
    public IEnumerable<string> GetResources()
    {
        return this.Resources.Keys;
    }

    /// <returns>An enumeration of all of the planets connected to this planet.</returns>
    public IEnumerable<Planet> GetConnectingPlanets()
    {
        return this.ConnectingPlanets;
    }

    /// <summary>
    ///     Bases the hashcode of a planet object off its name.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    /// <summary>
    ///     Uses a simple breadth first search algorithm to calculate the shortest path between this planet and the given planet.
    /// </summary>
    /// <param name="planet"></param>
    /// <returns>The a list of the pathway to the given planet. List INCLUDES this planet as the first planet.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public List<Planet> TravelTo(Planet destinationPlanet)
    {
        Queue<Planet> planetQueue = new Queue<Planet>();

        HashSet<Planet> visited = new HashSet<Planet> { this };

        foreach (Planet connectingPlanet in this.ConnectingPlanets)
        {
            planetQueue.Enqueue(connectingPlanet);
        }

        List<Planet> shortestPath = new List<Planet> { this };

        while (planetQueue.Count > 0)
        {
            if (planetQueue.Peek() == destinationPlanet)
            {
                shortestPath.Add(planetQueue.Dequeue());

                return shortestPath;
            }
            

        }
    }
}
