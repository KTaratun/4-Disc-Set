using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RelevantLobster.Data.Functions
{
    /// <inheritdoc cref="FunctionBase{TR}/>
    /// <summary>
    /// Base class for <see cref="FunctionBase{TR}"/>s with no parameters.
    /// </summary>
    /// <typeparam name="TR"></typeparam>
    public abstract class ZeroParameterFunction<TR> : FunctionBase<TR>
    {
        private Func<TR> _func;

        public virtual TR Invoke()
        {

#if UNITY_EDITOR
            if (SubLiteralValue) { return LiteralValue; }
#endif

            if (_func != null)
            {
                return _func();
            }

            string errTag = $"{nameof(ZeroParameterFunction<TR>)}.{nameof(Invoke)}";
            string err = $"{errTag}: no function logic is available. Either set {nameof(_func)} or override {nameof(Invoke)}";

            throw new InvalidOperationException(err);
        }

        /// <summary>
        /// Assign dynamic custom logic to this <see cref="ZeroParameterFunction{TR}"/>.
        /// </summary>
        /// <param name="function">The dynamic <see cref="Func{TR}"/> to be used when <see cref="Invoke"/> is called.
        /// </param>
        /// <returns>A reference to this <see cref="ZeroParameterFunction{TR}"/>.</returns>
        /// Thrown if the assigned <see cref="Func{TR}"/> is null.
        public ZeroParameterFunction<TR> Assign([NotNull] Func<TR> function)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            // Justification: NotNull attribute is compile-time safety only
            if (function != null)
            {
                _func = function;
                return this;
            }

            string errTag = $"{nameof(ZeroParameterFunction<TR>)}.{nameof(Assign)}";
            string err = $"{errTag}: Cannot assign a null function!";

            throw new ArgumentException(nameof(function), err);
        }
    }

    /// <inheritdoc cref="FunctionBase{TR}"/>
    /// <summary>
    /// Base class for <see cref="FunctionBase{TR}"/>s with a single parameter.
    /// </summary>
    /// <typeparam name="TP1">The type of the first parameter this <see cref="OneParameterFunction{TR, TP1}"/> accepts.</typeparam>
    public abstract class OneParameterFunction<TR, TP1> : FunctionBase<TR>
    {
        private Func<TP1, TR> _func;

        public virtual TR Invoke(TP1 p1)
        {
#if UNITY_EDITOR
            if (SubLiteralValue) { return LiteralValue; }
#endif

            if (_func != null)
            {
                return _func(p1);
            }

            string errTag = $"{nameof(OneParameterFunction<TR, TP1>)}.{nameof(Invoke)}";
            string err = $"{errTag}: no function logic is available. Either set {nameof(_func)} or override {nameof(Invoke)}";

            throw new InvalidOperationException(err);
        }

        public OneParameterFunction<TR, TP1> Assign([NotNull] Func<TP1, TR> function)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            // Justification: NotNull attribute is compile-time safety only
            if (function != null)
            {
                _func = function;
                return this;
            }

            string errTag = $"{nameof(OneParameterFunction<TR, TP1>)}.{nameof(Assign)}";
            string err = $"{errTag}: Cannot assign a null function!";

            throw new ArgumentException(nameof(function), err);
        }
    }

    public abstract class TwoParameterFunction<TR, TP1, TP2> : FunctionBase<TR>
    {
        private Func<TP1, TP2, TR> _func;

        public virtual TR Invoke(TP1 p1, TP2 p2)
        {
#if UNITY_EDITOR
            if (SubLiteralValue) { return LiteralValue; }
#endif

            if (_func != null)
            {
                return _func(p1, p2);
            }

            string errTag = $"{nameof(TwoParameterFunction<TR, TP1, TP2>)}.{nameof(Invoke)}";
            string err = $"{errTag}: no function logic is available. Either set {nameof(_func)} or override {nameof(Invoke)}";

            throw new InvalidOperationException(err);
        }

        public TwoParameterFunction<TR, TP1, TP2> Assign([NotNull] Func<TP1, TP2, TR> function)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            // Justification: NotNull attribute is compile-time safety only
            if (function != null)
            {
                _func = function;
                return this;
            }

            string errTag = $"{nameof(TwoParameterFunction<TR, TP1, TP2>)}.{nameof(Assign)}";
            string err = $"{errTag}: Cannot assign a null function!";

            throw new ArgumentException(nameof(function), err);
        }
    }

    public abstract class ThreeParameterFunction<TR, TP1, TP2, TP3> : FunctionBase<TR>
    {
        private Func<TP1, TP2, TP3, TR> _func;

        public virtual TR Invoke(TP1 p1, TP2 p2, TP3 p3)
        {
#if UNITY_EDITOR
            if (SubLiteralValue) { return LiteralValue; }
#endif

            if (_func != null)
            {
                return _func(p1, p2, p3);
            }

            string errTag = $"{nameof(ThreeParameterFunction<TR, TP1, TP2, TP3>)}.{nameof(Invoke)}";
            string err = $"{errTag}: no function logic is available. Either set {nameof(_func)} or override {nameof(Invoke)}";

            throw new InvalidOperationException(err);
        }

        public ThreeParameterFunction<TR, TP1, TP2, TP3> Assign([NotNull] Func<TP1, TP2, TP3, TR> function)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            // Justification: NotNull attribute is compile-time safety only
            if (function != null)
            {
                _func = function;
                return this;
            }

            string errTag = $"{nameof(ThreeParameterFunction<TR, TP1, TP2, TP3>)}.{nameof(Assign)}";
            string err = $"{errTag}: Cannot assign a null function!";

            throw new ArgumentException(nameof(function), err);
        }
    }

    public abstract class FourParameterFunction<TR, TP1, TP2, TP3, TP4> : FunctionBase<TR>
    {
        private Func<TP1, TP2, TP3, TP4, TR> _func;

        public virtual TR Invoke(TP1 p1, TP2 p2, TP3 p3, TP4 p4)
        {
#if UNITY_EDITOR
            if (SubLiteralValue) { return LiteralValue; }
#endif

            if (_func != null)
            {
                return _func(p1, p2, p3, p4);
            }

            string errTag = $"{nameof(FourParameterFunction<TR, TP1, TP2, TP3, TP4>)}.{nameof(Invoke)}";
            string err = $"{errTag}: no function logic is available. Either set {nameof(_func)} or override {nameof(Invoke)}";

            throw new InvalidOperationException(err);
        }

        public FourParameterFunction<TR, TP1, TP2, TP3, TP4> Assign([NotNull] Func<TP1, TP2, TP3, TP4, TR> function)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            // Justification: NotNull attribute is compile-time safety only
            if (function != null)
            {
                _func = function;
                return this;
            }

            string errTag = $"{nameof(FourParameterFunction<TR, TP1, TP2, TP3, TP4>)}.{nameof(Assign)}";
            string err = $"{errTag}: Cannot assign a null function!";

            throw new ArgumentException(nameof(function), err);
        }
    }

    public abstract class FiveParameterFunction<TR, TP1, TP2, TP3, TP4, TP5> : FunctionBase<TR>
    {
        private Func<TP1, TP2, TP3, TP4, TP5, TR> _func;

        public virtual TR Invoke(TP1 p1, TP2 p2, TP3 p3, TP4 p4, TP5 p5)
        {
#if UNITY_EDITOR
            if (SubLiteralValue) { return LiteralValue; }
#endif

            if (_func != null)
            {
                return _func(p1, p2, p3, p4, p5);
            }

            string errTag = $"{nameof(FiveParameterFunction<TR, TP1, TP2, TP3, TP4, TP5>)}.{nameof(Invoke)}";
            string err = $"{errTag}: no function logic is available. Either set {nameof(_func)} or override {nameof(Invoke)}";

            throw new InvalidOperationException(err);
        }

        public FiveParameterFunction<TR, TP1, TP2, TP3, TP4, TP5> Assign([NotNull] Func<TP1, TP2, TP3, TP4, TP5, TR> function)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            // Justification: NotNull attribute is compile-time safety only
            if (function != null)
            {
                _func = function;
                return this;
            }

            string errTag = $"{nameof(FiveParameterFunction<TR, TP1, TP2, TP3, TP4, TP5>)}.{nameof(Assign)}";
            string err = $"{errTag}: Cannot assign a null function!";

            throw new ArgumentException(nameof(function), err);
        }
    }

    /// <summary>
    /// Base class for <see cref="FunctionBase{TR}"/>s that take a <see cref="T:params{TP}"/> list as parameters/
    /// </summary>
    /// <typeparam name="TP">The type of the <see cref="T:params{TP}"/> list this <see cref="ParamsFunction{TR, TP}"/> accepts.</typeparam>
    public abstract class ParamsFunction<TR, TP> : FunctionBase<TR>
    {
        private Func<TP[], TR> _func;

        public virtual TR Invoke(params TP[] pList)
        {
#if UNITY_EDITOR
            if (SubLiteralValue) { return LiteralValue; }
#endif

            if (_func != null)
            {
                return _func(pList);
            }

            string errTag = $"{nameof(ParamsFunction<TR, TP>)}.{nameof(Invoke)}";
            string err = $"{errTag}: no function logic is available. Either set {nameof(_func)} or override {nameof(Invoke)}";

            throw new InvalidOperationException(err);
        }

        public ParamsFunction<TR, TP> Assign([NotNull] Func<TP[], TR> function)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            // Justification: NotNull attribute is compile-time safety only
            if (function != null)
            {
                _func = function;
                return this;
            }

            string errTag = $"{nameof(ParamsFunction<TR, TP>)}.{nameof(Assign)}";
            string err = $"{errTag}: Cannot assign a null function!";

            throw new ArgumentException(nameof(function), err);
        }
    }
}
