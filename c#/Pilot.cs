// <copyright file="User.cs" author="Joshua Jex">
// Copyright (c) Joshua Jex. All rights reserved. 3/23/2025
// </copyright>
// <summary>
//   Code provides an abstract way of constructing users or "stations" for the Model to implement.
// </summary>

using JexPod.Abstract.Users;
using JexPod.Components.Shield;

namespace JexPod.User.WeaponsSystemOfficer;

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
public class Pilot : BaseUser
{


    public Pilot(string name) : base(name)
    {

    }

    /// <summary>
    ///     Abstract method that changes the state of any or all of the given components.
    /// </summary>
    /// <param name="components">A set of the names of components.</param>
    /// <returns>Returns the new set of components from the given set of components.</returns>
    public override HashSet<string> EditComponents(HashSet<string> components)
    {

    }
}
