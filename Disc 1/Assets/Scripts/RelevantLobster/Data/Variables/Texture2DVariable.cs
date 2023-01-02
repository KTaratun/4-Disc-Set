using RelevantLobster.Data.Pathing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Data.Variables
{
    /// <summary>
    /// A <see cref="Variable{T}"/> of type <see cref="Texture2D"/>.
    /// </summary>
    [CreateAssetMenu(menuName = Menus.CreateAssetMenu.Variables + nameof(Texture2D))]
    public class Texture2DVariable : Variable<Texture2D> { }
}