using Bogus;
using Lowtab.Monster.Service.Application.Tags.Commands;
using Lowtab.Monster.Service.Application.Tags.Mappings;
using Lowtab.Monster.Service.Contracts.Tags.Common;
using Lowtab.Monster.Service.Contracts.Tags.GetTag;
using Lowtab.Monster.Service.Domain.Entities;

namespace Lowtab.Monster.Service.Application.UnitTests.Tags;

public static class Arrange
{
    private static readonly Faker<CreateTagCommand> CreateReceiptCommandFaker = new Faker<CreateTagCommand>()
        .RuleFor(x => x.Id, f => new TagId(f.PickRandom<GroupTagEnum>(), f.Random.String2(10)))
        .RuleFor(x => x.Description, f => f.Lorem.Sentence());

    private static readonly Faker<UpdateTagCommand> UpdateReceiptCommandFaker = new Faker<UpdateTagCommand>()
        .RuleFor(x => x.Id, f => new TagId(f.PickRandom<GroupTagEnum>(), f.Random.String2(10)))
        .RuleFor(x => x.Description, f => f.Lorem.Sentence());

    private static readonly Faker<GetTagResponse> GetReceiptBaseResponseFaker = new Faker<GetTagResponse>()
        .RuleFor(x => x.Id, f => new TagId(f.PickRandom<GroupTagEnum>(), f.Random.String2(10)))
        .RuleFor(x => x.Description, f => f.Lorem.Sentence());

    public static CreateTagCommand GetValidCreateTagCommand()
    {
        return CreateReceiptCommandFaker.Generate();
    }

    public static UpdateTagCommand GetValidUpdateTagCommand()
    {
        return UpdateReceiptCommandFaker.Generate();
    }

    public static GetTagResponse GetValidGetTagResponse()
    {
        return GetReceiptBaseResponseFaker.Generate();
    }

    public static TagEntity GenerateTagEntity()
    {
        return CreateReceiptCommandFaker.Generate().ToEntity();
    }
}
