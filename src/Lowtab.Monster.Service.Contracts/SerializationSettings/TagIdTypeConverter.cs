using System.ComponentModel;
using System.Globalization;
using Lowtab.Monster.Service.Contracts.Tags.Common;

namespace Lowtab.Monster.Service.Contracts.SerializationSettings;

/// <inheritdoc />
public class TagIdTypeConverter : TypeConverter
{
    /// <inheritdoc />
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    /// <inheritdoc />
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string s)
        {
            return TagId.Parse(s);
        }

        return base.ConvertFrom(context, culture, value);
    }
}
