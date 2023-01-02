using RelevantLobster.Data.Pathing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Data.Variables
{
    /// <summary>
    /// A <see cref="Variable{T}"/> of type <see cref="string"/>.
    /// </summary>
    [CreateAssetMenu(menuName = Menus.CreateAssetMenu.Variables + nameof(String))]
    public class StringVariable : Variable<string>
    {
        protected override void Awake()
        {
            // The strings need to be sure to be non-null as the first upon load
            m_initialValue ??= string.Empty;
            m_previousValue ??= string.Empty;
            m_runtimeValue ??= string.Empty;
        }
    }
}