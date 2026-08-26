using System;
using System.Globalization;
using System.Text;

public static class NumberFormatter
{
    #region Public API

    public static string FormatCompact(BigNumber number)
    {
        if (number.Mantissa == 0)
            return "0";

        if (number.Exponent < 3)
            return FormatSmallNumber(number);

        int suffixIndex = number.Exponent / 3;
        string suffix = SuffixGenerator.GetSuffix(suffixIndex);

        return $"{FormatMantissa(number.Mantissa)} {suffix}";
    }

    public static string FormatFull(BigNumber number)
    {
        if (number.Mantissa == 0)
            return "0";

        double value = number.Mantissa * Math.Pow(10, number.Exponent % 3);

        string digits = value.ToString("N0", CultureInfo.InvariantCulture);

        int remainingExponent = number.Exponent - (number.Exponent % 3);

        if (remainingExponent == 0)
            return digits;

        return $"{digits} {SuffixGenerator.GetSuffix(remainingExponent / 3)}";
    }

    public static string FormatScientific(BigNumber number)
    {
        return $"{number.Mantissa:0.###}e{number.Exponent}";
    }

    #endregion

    #region Formatting

    private static string FormatSmallNumber(BigNumber number)
    {
        double value = number.Mantissa * Math.Pow(10, number.Exponent);

        if (value >= 100)
            return value.ToString("0", CultureInfo.InvariantCulture);

        if (value >= 10)
            return value.ToString("0.0", CultureInfo.InvariantCulture);

        return value.ToString("0.00", CultureInfo.InvariantCulture);
    }

    private static string FormatMantissa(double mantissa)
    {
        if (mantissa >= 100)
            return mantissa.ToString("0", CultureInfo.InvariantCulture);

        if (mantissa >= 10)
            return mantissa.ToString("0.0", CultureInfo.InvariantCulture);

        return mantissa.ToString("0.00", CultureInfo.InvariantCulture);
    }

    public static FormattedNumber FormatUI(BigNumber number)
    {
        // Usa la tua logica già esistente.
        // L'importante è restituire separatamente
        // il numero e il suffisso.

        string formatted = FormatCompact(number);

        string[] parts = formatted.Split(' ');

        if (parts.Length == 1)
            return new FormattedNumber(parts[0], "");

        return new FormattedNumber(parts[0], parts[1]);
    }


    #endregion

    #region Suffix Generator

    private static class SuffixGenerator
    {
        public static string GetSuffix(int index)
        {
            if (index <= 0)
                return "";

            if (index == 1)
                return "K";

            if (index == 2)
                return "M";

            if (index == 3)
                return "B";

            if (index == 4)
                return "T";

            // Dal 5 in poi:
            // 5 = AA
            // 6 = AB
            // ...
            // 30 = AZ
            // 31 = BA
            // ...

            index -= 5;

            StringBuilder builder = new();

            do
            {
                builder.Insert(0, (char)('A' + (index % 26)));
                index /= 26;
                index--;
            }
            while (index >= 0);

            if (builder.Length == 1)
                builder.Insert(0, 'A');

            return builder.ToString();
        }
    }

    #endregion
}

public struct FormattedNumber
{
    public string Value;
    public string Suffix;

    public FormattedNumber(string value, string suffix)
    {
        Value = value;
        Suffix = suffix;
    }
}