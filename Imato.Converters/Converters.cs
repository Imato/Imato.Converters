using System;
using System.Data.SqlTypes;
using System.Globalization;

namespace Imato.Converters
{
    public static class Converters
    {
        public static CultureInfo PointCulture = new CultureInfo("en")
        {
            NumberFormat =
            {
                NumberDecimalSeparator = "."
            }
        };

        public static CultureInfo CommaCulture = new CultureInfo("en")
        {
            NumberFormat =
            {
                NumberDecimalSeparator = ","
            }
        };

        public static DateTime? ToDateTime(this string value)
        {
            if (DateTime.TryParse(value.Trim(), out var d))
            {
                return d;
            }
            return null;
        }

        public static DateTime ToMandatoryDateTime(this string value)
        {
            return ToDateTime(value)
                ?? throw new ConverException<DateTime>(value);
        }

        public static double? ToDouble(this string value)
        {
            value = value.Trim();
            if (!value.Contains(",") && !value.Contains("."))
            {
                if (double.TryParse(value, out var d))
                {
                    return d;
                }
            }

            if (value.Contains(","))
            {
                if (double.TryParse(value,
                     NumberStyles.Any,
                     CommaCulture,
                     out var d))
                {
                    return d;
                }
            }

            if (value.Contains("."))
            {
                if (double.TryParse(value,
                    NumberStyles.Any,
                    PointCulture,
                    out var d))
                {
                    return d;
                }
            }

            return null;
        }

        public static double ToMandatoryDouble(this string value)
        {
            return ToDouble(value)
                ?? throw new ConverException<double>(value);
        }

        public static decimal? ToDecimal(this string value)
        {
            value = value.Trim();
            if (!value.Contains(",") && !value.Contains("."))
            {
                if (decimal.TryParse(value, out var d))
                {
                    return d;
                }
            }

            if (value.Contains(","))
            {
                if (decimal.TryParse(value,
                     NumberStyles.Any,
                     CommaCulture,
                     out var d))
                {
                    return d;
                }
            }

            if (value.Contains("."))
            {
                if (decimal.TryParse(value,
                    NumberStyles.Any,
                    PointCulture,
                    out var d))
                {
                    return d;
                }
            }

            return null;
        }

        public static decimal ToMandatoryDecimal(this string value)
        {
            return ToDecimal(value)
                ?? throw new ConverException<decimal>(value);
        }

        public static long? ToLong(this string value)
        {
            if (long.TryParse(value, out var d))
            {
                return d;
            }
            return null;
        }

        public static long ToMandatoryLong(this string value)
        {
            return ToLong(value)
                ?? throw new ConverException<long>(value);
        }

        public static int? ToInt(this string value)
        {
            if (int.TryParse(value, out var d))
            {
                return d;
            }
            return null;
        }

        public static int ToMandatoryInt(this string value)
        {
            return ToInt(value)
                ?? throw new ConverException<int>(value);
        }

        public static bool? ToBool(this string value)
        {
            if (bool.TryParse(value, out var d))
            {
                return d;
            }
            return null;
        }

        public static bool ToMandatoryBool(this string value)
        {
            return ToBool(value)
                ?? throw new ConverException<bool>(value);
        }

        public static string ToSqlString(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        public static SqlString ToSqlString(this string value, int maxLength = 500)
        {
            return new SqlString(value.Length <= maxLength
                ? value
                : value.Substring(0, maxLength));
        }

        public static SqlDouble? ToSqlDouble(this string value)
        {
            var d = ToDouble(value);
            if (d.HasValue)
            {
                return new SqlDouble(d.Value);
            }
            else
            {
                return null;
            }
        }

        public static SqlInt64? ToSqlInt64(this string value)
        {
            var d = ToLong(value);
            if (d.HasValue)
            {
                return new SqlInt64(d.Value);
            }
            else
            {
                return null;
            }
        }

        public static SqlInt32? ToSqlInt(this string value)
        {
            var d = ToInt(value);
            if (d.HasValue)
            {
                return new SqlInt32(d.Value);
            }
            else
            {
                return null;
            }
        }

        public static SqlBoolean? SqlBoolean(this string value)
        {
            var d = ToBool(value);
            if (d.HasValue)
            {
                return new SqlBoolean(d.Value);
            }
            else
            {
                return null;
            }
        }
    }
}