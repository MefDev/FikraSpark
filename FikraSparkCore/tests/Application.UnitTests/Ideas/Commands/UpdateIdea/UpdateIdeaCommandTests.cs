using Application.UnitTests.Common;
using Ardalis.GuardClauses;
using FikraSparkCore.Application.Common.Exceptions;
using FikraSparkCore.Application.Ideas.Commands.UpdateIdea;
using FikraSparkCore.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace FikraSparkCore.Application.UnitTests.Ideas.Commands.UpdateIdea;


[TestFixture]
public class UpdateIdeaCommandTests : TestBase
{
    [Test]
    public async Task ShouldUpdateIdea()
    {
        var idea = new Idea("Old title", "Old desc", 0);
        Context.Ideas.Add(idea);
        await Context.SaveChangesAsync(CancellationToken.None);

        var handler = new UpdateIdeaCommandHandler(Context);

        await handler.Handle(new UpdateIdeaCommand(idea.Id, "Updated", "Updated desc"), CancellationToken.None);

        var updated = await Context.Ideas.FindAsync(idea.Id);
        updated!.Title.Should().Be("Updated");
        updated.Description.Should().Be("Updated desc");
    }

    [Test]
    public void ShouldThrowIfIdeaNotFound()
    {
        var handler = new UpdateIdeaCommandHandler(Context);

        Func<Task> act = async () => await handler.Handle(
            new UpdateIdeaCommand(999, "Updated", "Updated desc"),
            CancellationToken.None);

        act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public void ShouldValidateEmptyFields()
    {
        var handler = new UpdateIdeaCommandHandler(Context);

        Func<Task> act = async () => await handler.Handle(
            new UpdateIdeaCommand(1, "", ""),
            CancellationToken.None);

        act.Should().ThrowAsync<ValidationException>();
    }
}

