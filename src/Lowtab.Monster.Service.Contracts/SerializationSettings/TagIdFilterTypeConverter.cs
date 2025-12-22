using System.ComponentModel;
using System.Globalization;
using Lowtab.Monster.Service.Contracts.Tags.Common;

namespace Lowtab.Monster.Service.Contracts.SerializationSettings;

/// <summary>
///     Type converter для <see cref="TagIdFilter" /> для работы с HTTP query параметрами
/// </summary>
public class TagIdFilterTypeConverter : TypeConverter
{
    /// <inheritdoc />
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    /// <inheritdoc />
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string stringValue)
        {
            return TagIdFilter.TryParse(stringValue, out var result) ? result : null;
        }

        return base.ConvertFrom(context, culture, value);
    }

    /// <inheritdoc />
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
    }

    /// <inheritdoc />
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value,
        Type destinationType)
    {
        if (destinationType == typeof(string) && value is TagIdFilter filter)
        {
            return filter.ToString();
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }
}
