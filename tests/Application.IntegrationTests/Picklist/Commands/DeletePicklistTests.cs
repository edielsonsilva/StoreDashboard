using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using StoreDashboard.Blazor.Application.Common.ExceptionHandlers;
using StoreDashboard.Blazor.Application.Features.PicklistSets.Commands.AddEdit;
using StoreDashboard.Blazor.Application.Features.PicklistSets.Commands.Delete;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.IntegrationTests.Picklist.Commands;

using static Testing;

public class DeletePicklistTests : TestBase
{
    [Test]
    public void ShouldRequireValidKeyValueId()
    {
        var command = new DeletePicklistSetCommand(new[] { 99 });

        FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteKeyValue()
    {
        var addCommand = new AddEditPicklistSetCommand
        {
            Name = StoreDashboard.Blazor.Domain.Entities.Picklist.Brand,
            Text = "Word",
            Value = "Word",
            Description = "For Test"
        };
        var result = await SendAsync(addCommand);

        await SendAsync(new DeletePicklistSetCommand(new[] { result.Data }));

        var item = await FindAsync<Document>(result.Data);

        item.Should().BeNull();
    }
}