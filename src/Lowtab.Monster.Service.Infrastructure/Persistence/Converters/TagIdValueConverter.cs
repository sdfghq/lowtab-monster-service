using Lowtab.Monster.Service.Contracts.Tags.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lowtab.Monster.Service.Infrastructure.Persistence.Converters;

public class TagIdValueConverter()
    : ValueConverter<TagId, string>(v => v.ToString(), v => TagId.Parse(v));
