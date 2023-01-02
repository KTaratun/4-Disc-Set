using RelevantLobster.Data.Pathing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Data.Variables
{
    /// <summary>
    /// A <see cref="Variable{T}"/> of type <see cref="Vector2"/>.
    /// </summary>
    [CreateAssetMenu(menuName = Menus.CreateAssetMenu.Variables + nameof(Vector2))]
    public class Vector2Variable : Variable<Vector2> { }
}