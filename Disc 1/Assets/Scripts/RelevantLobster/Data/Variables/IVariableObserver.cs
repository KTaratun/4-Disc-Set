using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Data.Variables
{
    /// <summary>
    /// Interface used for objects which respond to changes to a Variable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IVariableObserver<out T>
    {
        /// <summary>
        /// Receive notifications when the Variable's value changes.
        /// </summary>
        public event Action OnChanged;

        /// <summary>
        /// Receive notifications with the new value when the Variable's value changes.
        /// </summary>
        public event Action<T> OnChangedTo;

        /// <summary>
        /// Receive notifications with the new and previous values when the Variable's value changes.
        /// </summary>
        /// <remarks>
        /// The first <see cref="Action"/> argument is the new value while the second is the previous value.
        /// </remarks>
        public event Action<T, T> OnChangedFrom;
    }
}
