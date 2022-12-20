using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RelevantLobster.Data.Variables.IVariable;

namespace RelevantLobster.Data.Variables
{
    /// <summary>
    /// Generic base for all types of variables.
    /// </summary>
    /// <typeparam name="T">The type of data this <see cref="BaseVariable{T}"/> contains.</typeparam>
    public abstract class BaseVariable<T> : SubLiteralScriptableObject<T>, IVariable<T>, IEquatable<BaseVariable<T>>
    {
        [Tooltip("The value to be used as the Variable's initial runtime value.")]
        [SerializeField]
        protected T m_initialValue => default;

#if UNITY_EDITOR

        /// <summary>
        /// Prevent magic string property access
        /// </summary>
        internal const string InitialValueFieldName = nameof(m_initialValue);

#endif

        protected private const string ResetUnavailableMessage = nameof(ResetValue) + " is unavailable for this type of " + nameof(IVariable<T>);
        protected private const string SetUnavailableMessage = "The value of this type of " + nameof(IVariable<T>) + " cannot be set at runtime.";

        public virtual object BaseValue
        {
            get => m_initialValue;
            set => Value = (T)value;
        }

        public virtual T InitialValue => m_initialValue;

        public virtual T Value
        {
            get => m_initialValue;
            set => SetValue(value);
        }

        public virtual T PreviousValue => m_initialValue;

        /// <summary>
        /// Determine if two <see cref="BaseVariable{T}"/>s are equal.
        /// </summary>
        /// <param name="other">The other <see cref="BaseVariable{T}"/> being compared for equality.</param>
        /// <returns><see langword="true""/> if the <see cref="BaseVariable{T}"/>s are equal. False otherwise.</returns>
        public bool Equals(BaseVariable<T> other) => null != other && ValueEquals(other.Value);

        public virtual void ResetValue(bool notify = false)
        {
            // Logger stuff was supposed to be here.
        }

        /// <summary>
        /// Set the value of this <see cref="Variable{T}"/>.
        /// </summary>
        /// <remarks>
        /// Normal behavior is that the value is set and change events sent only if the new value differs from the
        /// current one. Use the <paramref name="force"/> and <paramref name="notify"/> flags if you need to either
        /// force the setting of the value and/or the notification of changes.
        /// </remarks>
        /// <param name="value">The new value to be used.</param>
        /// <param name="force">Flag indicating if the setting of the value should be forced.</param>
        /// <param name="notify">Flag indicating if the notification of a change should be sent.</param>
        internal virtual void SetValue(T value, bool force = false, bool notify = true)
        {
            // Logger stuff was supposed to be here.
        }

        /// <summary>
        /// Determine if the <see cref="BaseVariable{T}"/>'s value is equal to another <see cref="BaseVariable{T}"/>'s value.
        /// </summary>
        /// <param name="other">The other <see cref="BaseVariable{T}"/> whose value should be compared.</param>
        /// <returns>true if the <i>value</i> of the two <see cref="BaseVariable{T}"/>'s are equal.</returns>
        protected virtual bool ValueEquals(T other) => Value == null ? other == null : Value.Equals(other);
    }
}
