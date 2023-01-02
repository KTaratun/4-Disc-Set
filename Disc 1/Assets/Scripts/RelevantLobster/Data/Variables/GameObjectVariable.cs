using RelevantLobster.Data.Pathing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Data.Variables
{
    /// <summary>
    /// A <see cref="Variable{T}"/> of type <see cref="GameObject"/>.
    /// </summary>
    [CreateAssetMenu(menuName = Menus.CreateAssetMenu.Variables + nameof(GameObject))]
    public class GameObjectVariable : Variable<GameObject> { }
}