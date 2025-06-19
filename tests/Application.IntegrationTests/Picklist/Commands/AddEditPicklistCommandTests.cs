using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using StoreDashboard.Blazor.Application.Features.PicklistSets.Commands.AddEdit;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.IntegrationTests.Picklist.Commands;

using static Testing;

internal class AddEditPicklistCommandTests : TestBase
{
    [Test]
    public void ShouldThrowValidationException()
    {
        var command = new AddEditPicklistSetCommand();
        FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task InsertItem()
    {
        var addCommand = new AddEditPicklistSetCommand
            { Name = StoreDashboard.Blazor.Domain.Entities.Picklist.Brand, Text = "Test", Value = "Test", Description = "Description" };
        var result = await SendAsync(addCommand);
        var find = await FindAsync<PicklistSet>(result.Data);
        find.Should().NotBeNull();
        find.Name.Should().Be(StoreDashboard.Blazor.Domain.Entities.Picklist.Brand);
        find.Text.Should().Be("Test");
        find.Value.Should().Be("Test");
    }

    [Test]
    public async Task UpdateItem()
    {
        var addCommand = new AddEditPicklistSetCommand
            { Name = StoreDashboard.Blazor.Domain.Entities.Picklist.Brand, Text = "Test", Value = "Test", Description = "Description" };
        var result = await SendAsync(addCommand);
        var find = await FindAsync<PicklistSet>(result.Data);
        var editCommand = new AddEditPicklistSetCommand
            { Id = find.Id, Name = StoreDashboard.Blazor.Domain.Entities.Picklist.Brand, Text = "Test1", Value = "Test1", Description = "Description1" };
        await SendAsync(editCommand);
        var updated = await FindAsync<PicklistSet>(find.Id);
        updated.Should().NotBeNull();
        updated.Name.Should().Be(StoreDashboard.Blazor.Domain.Entities.Picklist.Brand);
        updated.Text.Should().Be("Test1");
        updated.Value.Should().Be("Test1");
        updated.Description.Should().Be("Description1");
    }
}