using Bogus;
using Lowtab.Monster.Service.Application.Tags.Commands;
using Lowtab.Monster.Service.Application.Tags.Mappings;
using Lowtab.Monster.Service.Contracts.Tags.GetTag;
using Lowtab.Monster.Service.Domain.Entities;

namespace Lowtab.Monster.Service.Application.UnitTests.Tags;

public static class Arrange
{
    private static readonly Faker<CreateTagCommand> CreateReceiptCommandFaker = new Faker<CreateTagCommand>()
        .RuleFor(x => x.Name, f => f.Commerce.ProductName());

    private static readonly Faker<UpdateTagCommand> UpdateReceiptCommandFaker = new Faker<UpdateTagCommand>()
        .RuleFor(x => x.Name, f => f.Commerce.ProductName())
        .RuleFor(x => x.Id, f => f.Random.Guid());

    private static readonly Faker<GetTagResponse> GetReceiptBaseResponseFaker = new Faker<GetTagResponse>()
        .RuleFor(x => x.Id, f => f.Random.Guid())
        .RuleFor(x => x.Name, f => f.Commerce.ProductName());

    public static CreateTagCommand GetValidCreateTagCommand() => CreateReceiptCommandFaker.Generate();
    public static UpdateTagCommand GetValidUpdateTagCommand() => UpdateReceiptCommandFaker.Generate();
    public static GetTagResponse GetValidGetTagResponse() => GetReceiptBaseResponseFaker.Generate();
    public static TagEntity GenerateTagEntity() => CreateReceiptCommandFaker.Generate().ToEntity();
}
