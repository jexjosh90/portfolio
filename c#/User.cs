// <copyright file="User.cs" author="Joshua Jex">
// Copyright (c) Joshua Jex. All rights reserved. 3/23/2025
// </copyright>
// <summary>
//   Code provides an abstract way of constructing users or "stations" for the Model to implement.
// </summary>

namespace JexPod.Abstract.Users;

/// <summary>
///     User is the modeling of a set of controllers operated by one person (e.g. Pilot, Engineer, Captain, etc.)
///     <para>
///         The abstract class provides a way for other parts of the model to create and store Users without having
///         to have the specific subclass of each User. The User subclasses should not change the model itself, but provide
///         the controller with a way to access the model.
///         For example: 
///         <list type="bullet">
///             <item>Say there is a "Pilot" class. The pilot inputs through a joy stick (controller). The controller changes the state of the pilot subclass which
///             in turn edits the state of the ship. </item>
///             <item>Say there is an "Engineering" class. The engineer flips switches (controller). The switches changes the state of the engineer subclass which
///             in turn edits the state of the ship. </item>
///         </list>
///     </para>
///     <para>
///         A ship only has two editable variales, health and components. Thus each User subclass only needs to be able to change these two variables.
///         However, the user methods can be used to edit any given health or component.
///     </para>
/// </summary>
public abstract class BaseUser
{

    /// <summary>
    ///     Read only variable that cannot be edited once constructed. The name is what hashcode will be based off and how each User will be identified.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Parent class constructor that stores the name of the User.
    /// </summary>
    /// <param name="name"></param>
    public BaseUser(string name)
    {
        this.Name = name;
    }

    /// <summary>
    ///     Determines the hashcode of a user object by its name.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    /// <summary>
    ///     Abstract method that changes the state of any or all of the given components.
    /// </summary>
    /// <param name="components">A set of the names of components.</param>
    /// <returns>Returns the new set of components from the given set of components.</returns>
    public abstract HashSet<string> EditComponents(HashSet<string> components);
}
