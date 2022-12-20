using RelevantLobster.Utilities;
using System;
using System.Globalization;

namespace RelevantLobster.Types.Numbers
{
    /// <summary>
    /// Basic number type to be used for whole numbers.
    /// </summary>
    public class Number : BaseNumber<long>, IComparable, IFormattable, IConvertible, IComparable<Number>, IEquatable<Number>
    {
        /// <summary>
        /// Represents the largest possible value of a <see cref="Number"/>. <seealso cref="long.MaxValue"/>
        /// </summary>
        public const long MaxValue = long.MaxValue;

        /// <summary>
        /// Represents the smallest possible value of a <see cref="Number"/>. <seealso cref="long.MinValue"/>
        /// </summary>
        public const long MinValue = long.MinValue;

        #region Conversion Operators

        // These conversions follow the same C# language built-in conversions as defined here:
        // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/numeric-conversions

        #region Conversions TO Number

        public static implicit operator Number(sbyte number) => new Number(number);
        public static implicit operator Number(byte number) => new Number(number);
        public static implicit operator Number(short number) => new Number(number);
        public static implicit operator Number(ushort number) => new Number(number);
        public static implicit operator Number(int number) => new Number(number);
        public static implicit operator Number(uint number) => new Number(number);
        public static implicit operator Number(long number) => new Number(number);

        public static explicit operator Number(ulong number) => new Number((long) number);
        public static explicit operator Number(float number) => new Number((long) number);
        public static explicit operator Number(double number) => new Number((long) number);
        public static explicit operator Number(DecimalNumber number) => new Number((long) number.Value);
        public static explicit operator Number(decimal number) => new Number((long) number);

        #endregion

        #region Conversion FROM Number

        public static implicit operator long(Number number) => number.Value;
        public static implicit operator float(Number number) => number.Value;
        public static implicit operator double(Number number) => number.Value;
        public static implicit operator decimal(Number number) => number.Value;

        public static explicit operator sbyte(Number number) => (sbyte) number.Value;
        public static explicit operator byte(Number number) => (byte) number.Value;
        public static explicit operator short(Number number) => (short) number.Value;
        public static explicit operator ushort(Number number) => (ushort) number.Value;
        public static explicit operator int(Number number) => (int) number.Value;
        public static explicit operator uint(Number number) => (uint) number.Value;
        public static explicit operator ulong(Number number) => (ulong) number.Value;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new <see cref="Number"/>.
        /// </summary>
        public Number() { }

        /// <summary>
        /// Construct a new <see cref="Number"/> with the given <paramref name="value"/>.
        /// </summary>
        /// <param name="value"></param>
        public Number(long value) : base(value) { }

        #endregion

        #region IComparable<Number> Implementation

        /// <summary>
        /// Compares this instance to a specified <see cref="Number"/> instance and returns an indication of
        /// their relative values.
        /// </summary>
        /// <param name="other">A <see cref="Number"/> to compare.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>.<br/>
        /// Less than zero: This instance is less than <paramref name="other"/>.<br/>
        /// Zero: This instance is equal to <paramref name="other"/>.
        /// Greater than zero: This instance is greater than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(Number other) => Value.CompareTo(other.Value);

        #endregion

        #region IComparable Implementation

        /// <summary>
        /// Compares this instance to a specified object and returns an indication of their relative values.
        /// </summary>
        /// <param name="other">An object to compare, or <see langword="null"/>.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="other"/>. <br/>
        /// Less than zero: This instance is less than <paramref name="other"/>.<br/>
        /// Zero: This instance is equal to <paramref name="other"/>.
        /// Greater than zero: This instance is greater than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(object other) => Value.CompareTo(other);

        #endregion

        #region Operator Overloads

        public static bool operator ==(Number left, Number right)
        {
            return ObjectUtility.AreBothNull(left, right) || (left?.Equals(right) ?? false);
        }

        public static bool operator !=(Number left, Number right)
        {
            return left == right == false;
        }

        public static bool operator <(Number left, Number right)
        {
            return ObjectUtility.AreBothNotNull(left, right) && left.Value < right.Value;
        }

        public static bool operator >(Number left, Number right)
        {
            return ObjectUtility.AreBothNotNull(left, right) && left.Value > right.Value;
        }

        public static bool operator <=(Number left, Number right)
        {
            return ObjectUtility.AreBothNotNull(left, right) && left.Value <= right.Value;
        }

        public static bool operator >=(Number left, Number right)
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

        public static Number Parse(string @string) => long.Parse(@string);

        public static Number Parse(string @string, NumberStyles style) => long.Parse(@string, style);

        public static Number Parse(string @string, IFormatProvider provider) => long.Parse(@string, provider);

        public static Number Parse(string @string, NumberStyles style, IFormatProvider provider) => long.Parse(@string, style, provider);

        public static bool TryParse(string @string, out Number result)
        {
            // Normally this implementation would delegate to the TryParse() with full arguments. However, the intent
            // here is to ensure that Number has the exact behavior of long; if delegation to the other TryParse() was
            // used, defaults would have to be passed to match long, and those could be changed in the future outside
            // of our control.
            bool success = long.TryParse(@string, out long parsedLong);

            result = success ? parsedLong : 0L;

            return success;
        }

        public static bool TryParse(string @string, NumberStyles style, IFormatProvider provider, out Number result)
        {
            bool success = long.TryParse(@string, style, provider, out long parsedLong);

            result = success ? parsedLong : 0L;

            return success;
        }

        #endregion

        #region IEquatable<Number> Implementation

        public bool Equals(Number other) => other is null == false && Value.Equals(other.Value);

        #endregion

        #region Equality Members

        public override bool Equals(object other) => other is null == false && Equals((Number)other);

        public override int GetHashCode() => Value.GetHashCode();

        #endregion
    }
}