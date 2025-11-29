using Bogus;
using Lowtab.Monster.Service.Application.GroupTags.Commands;
using Lowtab.Monster.Service.Application.GroupTags.Mappings;
using Lowtab.Monster.Service.Contracts.GroupTags.GetGroupTag;
using Lowtab.Monster.Service.Domain.Entities;

namespace Lowtab.Monster.Service.Application.UnitTests.GroupTags;

public static class Arrange
{
    private static readonly Faker<CreateGroupTagCommand> CreateReceiptCommandFaker = new Faker<CreateGroupTagCommand>()
        .RuleFor(x => x.Name, f => f.Commerce.ProductName());

    private static readonly Faker<UpdateGroupTagCommand> UpdateReceiptCommandFaker = new Faker<UpdateGroupTagCommand>()
        .RuleFor(x => x.Name, f => f.Commerce.ProductName())
        .RuleFor(x => x.Id, f => f.Random.Guid());

    private static readonly Faker<GetGroupTagResponse> GetReceiptBaseResponseFaker = new Faker<GetGroupTagResponse>()
        .RuleFor(x => x.Id, f => f.Random.Guid())
        .RuleFor(x => x.Name, f => f.Commerce.ProductName());

    public static CreateGroupTagCommand GetValidCreateGroupTagCommand() => CreateReceiptCommandFaker.Generate();
    public static UpdateGroupTagCommand GetValidUpdateGroupTagCommand() => UpdateReceiptCommandFaker.Generate();
    public static GetGroupTagResponse GetValidGetGroupTagResponse() => GetReceiptBaseResponseFaker.Generate();
    public static GroupTagEntity GenerateGroupTagEntity() => CreateReceiptCommandFaker.Generate().ToEntity();
}
