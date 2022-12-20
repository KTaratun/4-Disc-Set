using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Attributes
{
    /// <summary>
    /// An <see cref="Attribute"/> which applies a given texture as an icon for a behavior or object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EditorIconAttribute : Attribute
    {
        public EditorIconAttribute(string iconName)
        {
            IconName = iconName;
        }

        public string IconName { get; }
    }
}