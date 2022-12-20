using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using System.IO;

namespace RelevantLobster.Data.Signals
{
    using Pathing;
    using Attributes;

    [CreateAssetMenu(menuName = Menus.CreateAssetMenu.Signals + nameof(Signal))]
    public class Signal : ScriptableObject
    {
        private readonly List<Action> _actions = new List<Action>();
        private readonly List<Action> _pendingRegisters = new List<Action>();
        private readonly List<Action> _pendingUnregisters = new List<Action>();

        private bool _isPosting;

        #region Static Interface

        public static void Register<T>([NotNull] IList<T> signals, [NotNull] Action action) where T : Signal
        {
            if (signals == null) { throw new ArgumentNullException(nameof(signals)); }
            if (action == null) { throw new ArgumentNullException(nameof(action)); }

            for (int i = 0; i < signals.Count; i++)
            {
                T signal = signals[i];

                if (signal == null)
                {
                    throw new NullReferenceException($"The provided {nameof(Signal)} in the {nameof(Register)} list at index {i} is null");
                }

                signal.Register(action);
            }
        }

        public void Unregister<T>([NotNull] IList<T> signals, [NotNull] Action action) where T : Signal
        {
            if (signals == null) { throw new ArgumentNullException(nameof(signals)); }
            if (action == null) { throw new ArgumentNullException(nameof(action)); }

            for (int i = 0; i < signals.Count; i++)
            {
                T signal = signals[i];

                if (signal == null)
                {
                    throw new NullReferenceException($"The provided {nameof(Signal)} in the {nameof(Unregister)} list at index {i} is null");
                }

                signal.Unregister(action);
            }
        }

        public static void Post<T>([NotNull] IList<T> signals, [CallerMemberName][CanBeNull] string memberName = "", [CallerFilePath][CanBeNull] string sourceFilePath = "") where T : Signal
        {
            if (signals == null) { throw new ArgumentNullException(nameof(signals)); }

            for (int i = 0; i < signals.Count; i++)
            {
                T signal = signals[i];

                if (signal == null)
                {
                    throw new NullReferenceException($"The provided {nameof(Signal)} in the {nameof(Post)} list at index {i} is null");
                }

                signal.Post();
            }
        }

        #endregion

        #region Unity Events

        private void OnEnable()
        {
            _isPosting = false;
        }

        private void OnDisable()
        {
            _isPosting = false;
        }

        #endregion

        #region Public Functions


        public void Register(Action action)
        {
            if (_isPosting)
            {
                _pendingRegisters.Add(action);

                _pendingRegisters.Remove(action);
            }
            else if (!_actions.Contains(action))
            {
                _actions.Add(action);
            }
        }

        public void Unregister(Action action)
        {
            if (_isPosting)
            {
                _pendingUnregisters.Add(action);

                _pendingUnregisters.Remove(action);
            }
            else if (!_actions.Contains(action))
            {
                _actions.Remove(action);
            }
        }

        public void Post([CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            if (_isPosting)
            {
                Debug.Log("Already posting!");

                return;
            }

            _isPosting = true;

            string className = Path.GetFileNameWithoutExtension(sourceFilePath);
            string posterClassMethodName = $"{className}.{memberName}";

            Debug.Log($"Posted by {posterClassMethodName}.{_actions.Count} Listener(s).");

            for (int i = 0; i < _actions.Count; i++)
            {
                Action action = _actions[i];

                // TODO: Make the help function to get class name so we can make a debug log
                //string listenerClassMethodName = Helpers.Get

                action.Invoke();
            }

            _isPosting = false;

            UpdateRegistry();
        }

        public IList<Action> GetCopyOfListeners()
        {
            return _actions.AsReadOnly();
        }

        #endregion

        #region Private Functions

        private void UpdateRegistry()
        {
            if (_isPosting) { return; }

            for (int i = 0; i < _pendingRegisters.Count; i++)
            {
                Register(_pendingRegisters[i]);
            }
            _pendingRegisters.Clear();

            for (int i = 0; i < _pendingUnregisters.Count; i++)
            {
                Unregister(_pendingUnregisters[i]);
            }
            _pendingUnregisters.Clear();
        }

        #endregion
    }

}