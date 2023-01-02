using RelevantLobster.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Data.Variables
{
    /// <summary>
    /// Base class for all non-constant Variables.
    /// </summary>
    /// <typeparam name="T">The type of data this <see cref="Variable{T}"/> contains.</typeparam>
    public abstract class Variable<T> : BaseVariable<T>, IVariableObserver<T>
    {
        [SerializeField]
        [Tooltip("The Variable's current runtime Value")]
        protected T m_runtimeValue = default;

        [SerializeField]
        [Tooltip("The Variable's previous runtime Value")]
        protected T m_previousValue = default;

#if UNITY_EDITOR

        // Prevent magic string property access
        internal const string RuntimeValuePropertyName = nameof(m_runtimeValue);
        internal const string PreviousValuePropertyName = nameof(m_previousValue);

#endif

        private bool _shouldTreatAsValueType = false;

        public event Action OnChanged;
        public event Action<T> OnChangedTo;
        public event Action<T, T> OnChangedFrom;

        public override object BaseValue { get => m_runtimeValue; set => base.BaseValue = value; }

        public override T Value { get => m_runtimeValue; set => SetValue(value); }

        /// <summary>
        /// The <see cref="Variable{T}"/>'s previous runtime value.
        /// </summary>
        public override T PreviousValue => m_previousValue;

        public override void ResetValue(bool notify = false)
        {
            if (notify) { SetValue(InitialValue, true); }
            else
            {
                Copy(m_runtimeValue, m_previousValue, ref m_previousValue);
                Copy(InitialValue, m_runtimeValue, ref m_runtimeValue);
            }
        }

        ///<remarks>
        /// Note that if the <paramref name="force"/> flag is not set, the value will not be set if it is the same as it
        /// was previously. Setting the <paramref name="force"/> flag will result in setting the <see cref="Variable{T}"/>'s
        /// value, regardless of what it previously was.
        /// </remarks>
        internal override void SetValue(T value, bool force = false, bool notify = true)
        {
            bool isSameValue = ValueEquals(value);

            if (isSameValue && false == force && false == notify) { return; }

            if (false == isSameValue || force)
            {
                Copy(m_runtimeValue, m_previousValue, ref m_previousValue);
                Copy(value, m_runtimeValue, ref m_runtimeValue);

                if (notify) { Notify(m_runtimeValue, m_previousValue); }
            }
        }

        #region Unity Events

        protected virtual void Awake()
        {
            _shouldTreatAsValueType = ObjectUtility.IsTreatedAsValueType<T>();
        }

        protected virtual void OnEnable()
        {
            Copy(InitialValue, m_runtimeValue, ref m_runtimeValue);
            Copy(InitialValue, m_previousValue, ref m_previousValue);

            CustomOnEnable();
        }

        #endregion

        /// <summary>
        /// Additional virtual steps that must be taken in <see cref="OnEnable"/>.
        /// </summary>
        protected virtual void CustomOnEnable()
        {
            SetValue(InitialValue, true);
        }

        /// <summary>
        /// Helper method to be used when <see cref="IVariableObserver{T}"/>s should be notified.
        /// </summary>
        /// <param name="newValue">The new value of the Variable.</param>
        /// <param name="prevValue">The previous value of the Variable.</param>
        protected void Notify(T newValue, T prevValue)
        {
            OnChanged?.Invoke();
            OnChangedTo?.Invoke(newValue);
            OnChangedFrom?.Invoke(newValue, prevValue);
        }

        /// <summary>
        /// Method to use when assigning <see cref="Variable{T}"/> values.
        /// </summary>
        /// <remarks>
        /// As there is different behavior in C# between how value types and references are treated, and even within
        /// those rules there are exceptions (e. g. <see cref="string"/> is technically a reference type but is treated
        /// like a value type), some logic surrounding <see cref="Variable{T}"/> value assignment is required to ensure
        /// that they are properly updated <b>by value only</b>.
        /// </remarks>
        /// <param name="from">The object of the type <see cref="T"/> that should be copied.</param>
        /// <param name="to">The object of the type <see cref="T"/> that is being assigned/copied to.</param>
        /// <param name="toRef">The reference to the same <paramref name="to"/> object, only used in the case that <typeparamref name="T"/>
        /// is treated as a value type.
        /// </param>
        protected void Copy(T from, T to, ref T toRef)
        {
            if (_shouldTreatAsValueType)
            {
                // As this is a value type, the reference parameter is used to directly overwrite the values in the
                // desired target.
                toRef = from;
                return;
            }

            // As this is a reference type, the non-reference parameter is used so that serialization can be used to
            // overwrite the values in the desired target. If the reference parameter was used here, the actual reference
            // would be assigned to the "from" reference instead of the values in the target being re-assigned.
            string serializedOriginal = JsonUtility.ToJson(from);
            JsonUtility.FromJsonOverwrite(serializedOriginal, to);
        }
    }
}