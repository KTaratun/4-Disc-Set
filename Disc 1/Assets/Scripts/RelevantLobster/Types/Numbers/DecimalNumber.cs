using RelevantLobster.Utilities;
using System;
using System.Globalization;

namespace RelevantLobster.Types.Numbers
{
    /// <summary>
    /// Basic number type to be used for real (floating point) numbers.
    /// </summary>
    public class DecimalNumber : BaseNumber<double>, IComparable, IFormattable, IConvertible, IComparable<DecimalNumber>, IEquatable<DecimalNumber>
    {
        /// <summary>Represents the largest possible value of a <see cref="DecimalNumber"/>. <seealso cref="double.MaxValue"/></summary>
        public const double MaxValue = double.MaxValue;

        /// <summary>Represents the smallest possible value of a <see cref="DecimalNumber"/>. <seealso cref="double.MinValue"/></summary>
        public const double MinValue = double.MinValue;

        /// <summary>Represents the smallest positive <see cref="DecimalNumber"/> value that is greater than zero.</summary>
        public const double Epsilon = double.Epsilon;

        /// <summary>Represents negative infinity.</summary>
        public const double NegativeInfinity = double.NegativeInfinity;

        /// <summary>Represents positive infinity.</summary>
        public const double PositiveInfinity = double.PositiveInfinity;

        /// <summary>Represents a value that is not a number (<see cref="NaN"/>).</summary>
        public const double NaN = double.NaN;

        #region Conversion Operators

        // These conversions follow the same C# language built-in conversions as defined here:
        // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/numeric-conversions

        #region Conversions TO DecimalNumber

        public static implicit operator DecimalNumber(sbyte decimalNumber) => new DecimalNumber(decimalNumber);
        public static implicit operator DecimalNumber(byte decimalNumber) => new DecimalNumber(decimalNumber);
        public static implicit operator DecimalNumber(short decimalNumber) => new DecimalNumber(decimalNumber);
        public static implicit operator DecimalNumber(ushort decimalNumber) => new DecimalNumber(decimalNumber);
        public static implicit operator DecimalNumber(int decimalNumber) => new DecimalNumber(decimalNumber);
        public static implicit operator DecimalNumber(uint decimalNumber) => new DecimalNumber(decimalNumber);
        public static implicit operator DecimalNumber(long decimalNumber) => new DecimalNumber(decimalNumber);
        public static implicit operator DecimalNumber(Number decimalNumber) => new DecimalNumber(decimalNumber);
        public static implicit operator DecimalNumber(ulong decimalNumber) => new DecimalNumber(decimalNumber);
        public static implicit operator DecimalNumber(float decimalNumber) => new DecimalNumber(decimalNumber);
        public static implicit operator DecimalNumber(double decimalNumber) => new DecimalNumber(decimalNumber);

        public static explicit operator DecimalNumber(decimal decimalNumber) => new DecimalNumber((double)decimalNumber);

        #endregion

        #region Conversion FROM DecimalNumber

        public static implicit operator double(DecimalNumber decimalNumber) => decimalNumber.Value;

        public static explicit operator sbyte(DecimalNumber decimalNumber) => (sbyte)decimalNumber.Value;
        public static explicit operator byte(DecimalNumber decimalNumber) => (byte)decimalNumber.Value;
        public static explicit operator short(DecimalNumber decimalNumber) => (short)decimalNumber.Value;
        public static explicit operator ushort(DecimalNumber decimalNumber) => (ushort)decimalNumber.Value;
        public static explicit operator int(DecimalNumber decimalNumber) => (int)decimalNumber.Value;
        public static explicit operator uint(DecimalNumber decimalNumber) => (uint)decimalNumber.Value;
        public static explicit operator long(DecimalNumber decimalNumber) => (long)decimalNumber.Value;
        public static explicit operator ulong(DecimalNumber decimalNumber) => (ulong)decimalNumber.Value;
        public static explicit operator float(DecimalNumber decimalNumber) => (float)decimalNumber.Value;
        public static explicit operator decimal(DecimalNumber decimalNumber) => (decimal)decimalNumber.Value;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new <see cref="Number"/>.
        /// </summary>
        public DecimalNumber() { }

        /// <summary>
        /// Construct a new <see cref="Number"/> with the given <paramref name="value"/>.
        /// </summary>
        /// <param name="value"></param>
        public DecimalNumber(double value) : base(value) { }

        #endregion

        #region Static Utilities

        public static bool IsNaN(DecimalNumber decimalNumber) => double.IsNaN(decimalNumber);
        public static bool IsInfinity(DecimalNumber decimalNumber) => double.IsInfinity(decimalNumber);
        public static bool IsPositiveInfinity(DecimalNumber decimalNumber) => double.IsPositiveInfinity(decimalNumber);
        public static bool IsNegativeInfinity(DecimalNumber decimalNumber) => double.IsNegativeInfinity(decimalNumber);

        #endregion

        #region IComparable<DecimalNumber> Implementation

        /// <summary>
        /// Compares this instance to a specified <see cref="DecimalNumber"/> and returns an integer that
        /// indicates whether the value of this instance is less than, equal to, or greater than the value of the
        /// specified <see cref="DecimalNumber"/>.
        /// </summary>
        /// <param name="other">A <see cref="DecimalNumber"/> to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.<br/>
        /// Less than zero: This instance is less than <paramref name="other"/>, or
        /// this instance is not a number (<see cref="DecimalNumber.NaN"/>) and <paramref name="other"/> is a number.<br/>
        /// Zero: This instance is equal to <paramref name="other"/>, or both this instance and <paramref name="other"/>
        /// are not a number <see cref="DecimalNumber.NaN"/>, <see cref="DecimalNumber.PositiveInfinity"/>,
        /// or <see cref="DecimalNumber.NegativeInfinity"/>.<br/>
        /// Greater than zero: This instance is greater than <paramref name="other"/>, or this instance is a number
        /// and <paramref name="other"/> is not a number (<see cref="DecimalNumber.NaN"/>.
        /// </returns>
        public int CompareTo(DecimalNumber other) => Value.CompareTo(other.Value);

        #endregion

        #region IComparable Implementation

        /// <summary>
        /// Compares this instance to a specified object and returns an integer that indicates indicates whether the value of 
        /// this instance is less than, equal to, or greater than the value of the specified object.
        /// </summary>
        /// <param name="other">An object to compare, or <see cref="null"/>.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.<br/>
        /// A negative integer: This instance is less than <paramref name="other"/>, or this instance is not a number 
        /// (<see cref="DecimalNumber.NaN"/>) and <paramref name="other"/> is a number.<br/>
        /// Zero: This instance is equal to <paramref name="other"/>, or both this instance and <paramref name="other"/>
        /// are not a number <see cref="DecimalNumber.NaN"/>, <see cref="DecimalNumber.PositiveInfinity"/>,
        /// or <see cref="DecimalNumber.NegativeInfinity"/>.<br/>
        /// A positive integer: This instance is greater than <paramref name="other"/>, or this instance is a number
        /// and <paramref name="other"/> is not a number (<see cref="DecimalNumber.NaN"/>, or
        /// <paramref name="other"/> is <see langword="null"/>.
        /// </returns>
        /// <exception cref="ArgumentException"><paramref name="other"/> is not a <see cref="double"/>.</exception>
        public int CompareTo(object other) => Value.CompareTo(other);

        #endregion

        #region Operator Overloads

        public static bool operator ==(DecimalNumber left, DecimalNumber right)
        {
            return ObjectUtility.AreBothNull(left, right) || (left?.Equals(right) ?? false);
        }

        public static bool operator !=(DecimalNumber left, DecimalNumber right)
        {
            return left == right == false;
        }

        public static bool operator <(DecimalNumber left, DecimalNumber right)
        {
            return ObjectUtility.AreBothNotNull(left, right) && left.Value < right.Value;
        }

        public static bool operator >(DecimalNumber left, DecimalNumber right)
        {
            return ObjectUtility.AreBothNotNull(left, right) && left.Value > right.Value;
        }

        public static bool operator <=(DecimalNumber left, DecimalNumber right)
        {
            return ObjectUtility.AreBothNotNull(left, right) && left.Value <= right.Value;
        }

        public static bool operator >=(DecimalNumber left, DecimalNumber right)
        {
            return ObjectUtility.AreBothNotNull(left, right) && left.Value >= right.Value;
        }

        #endregion

        #region IFormattable Implementation

        public string ToString(string format, IFormatProvider formatProvider) => Value.ToString(format, formatProvider);

        #endregion

        #region IConvertible Implementation

        public TypeCode GetTypeCode() => Value.GetTypeCode();

        public bool ToBoolean(IFormatProvider provider) => Convert.ToBoolean(Value);

        public byte ToByte(IFormatProvider provider) => Convert.ToByte(Value);

        public char ToChar(IFormatProvider provider) => Convert.ToChar(Value);

        public DateTime ToDateTime(IFormatProvider provider) => Convert.ToDateTime(Value);


        public decimal ToDecimal(IFormatProvider provider) => Convert.ToDecimal(Value);


        public double ToDouble(IFormatProvider provider) => Value;


        public short ToInt16(IFormatProvider provider) => Convert.ToInt16(Value);


        public int ToInt32(IFormatProvider provider) => Convert.ToInt32(Value);


        public long ToInt64(IFormatProvider provider) => Convert.ToInt64(Value);


        public sbyte ToSByte(IFormatProvider provider) => Convert.ToSByte(Value);


        public float ToSingle(IFormatProvider provider) => Convert.ToSingle(Value);


        public string ToString(IFormatProvider provider) => Convert.ToString(Value);


        public object ToType(Type conversionType, IFormatProvider provider) => ((IConvertible)Value).ToType(conversionType, provider);


        public ushort ToUInt16(IFormatProvider provider) => Convert.ToUInt16(Value);


        public uint ToUInt32(IFormatProvider provider) => Convert.ToUInt32(Value);


        public ulong ToUInt64(IFormatProvider provider) => Convert.ToUInt64(Value);

        #endregion

        #region String Utilities Implementations

        public override string ToString() => Value.ToString();

        public string ToString(string format) => Value.ToString(format);

        public static DecimalNumber Parse(string @string) => double.Parse(@string);

        public static DecimalNumber Parse(string @string, NumberStyles style) => double.Parse(@string, style);

        public static DecimalNumber Parse(string @string, IFormatProvider provider) => double.Parse(@string, provider);

        public static DecimalNumber Parse(string @string, NumberStyles style, IFormatProvider provider) => double.Parse(@string, style, provider);

        public static bool TryParse(string @string, out DecimalNumber result)
        {
            // Normally this implementation would delegate to the TryParse() with full arguments. However, the intent
            // here is to ensure that Number has the exact behavior of long; if delegation to the other TryParse() was
            // used, defaults would have to be passed to match long, and those could be changed in the future outside
            // of our control.
            bool success = double.TryParse(@string, out double parsedDouble);

            result = success ? parsedDouble : 0L;

            return success;
        }

        public static bool TryParse(string @string, NumberStyles style, IFormatProvider provider, out DecimalNumber result)
        {
            bool success = double.TryParse(@string, style, provider, out double parsedDouble);

            result = success ? parsedDouble : 0L;

            return success;
        }

        #endregion

        #region IEquatable<DecimalNumber> Implementation

        public bool Equals(DecimalNumber other) => other is null == false && Value.Equals(other.Value);

        #endregion

        #region Equality Members

        public override bool Equals(object other) => other is null == false && Equals((DecimalNumber)other);

        public override int GetHashCode() => Value.GetHashCode();

        #endregion

        /// <summary>
        /// Checks two <see cref="DecimalNumber"/>s together and ensures they are within a reasonable margin to be called equal.
        /// </summary>
        /// <param name="other"><see cref="DecimalNumber"/> to be compared against.</param>
        /// <param name="tolerance">Margin of error to be compared with. Any values with a difference that is
        /// within the tolerance is considered equal.</param>
        /// <returns>True if the difference between the value and other's value are less than the margin of error.</returns>
        public bool NearlyEqual(DecimalNumber other, DecimalNumber tolerance = null)
        {
            tolerance ??= 0.00001;

            if (Value.Equals(other.Value)) // Early exit, handles infinites
            {
                return true;
            }
            else
            {
                return Math.Abs(Value - other.Value) <= tolerance;
            }
        }
    }
}