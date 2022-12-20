using RelevantLobster.Data.Pathing;
using RelevantLobster.Types.Numbers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Data.Variables
{
    /// <summary>
    /// A <see cref="Variable{T}"/> of type <see cref="Number"/>.
    /// </summary>
    [CreateAssetMenu(menuName = Menus.CreateAssetMenu.Variables + nameof(Number))]
    public class NumberVariable : Variable<Number>{ }
}