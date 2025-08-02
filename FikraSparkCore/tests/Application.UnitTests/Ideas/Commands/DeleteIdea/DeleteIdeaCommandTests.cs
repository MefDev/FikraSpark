using Application.UnitTests.Common;
using Ardalis.GuardClauses;
using FikraSparkCore.Application.Ideas.Commands.DeleteIdea;
using FikraSparkCore.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace FikraSparkCore.Application.UnitTests.Ideas.Commands.DeleteIdea;

public class DeleteIdeaCommandTests : TestBase
{
    [Test]
    public async Task ShouldDeleteIdea()
    {
        var idea = new Idea("Title", "Desc", 0);
        Context.Ideas.Add(idea);
        await Context.SaveChangesAsync(CancellationToken.None);

        var handler = new DeleteIdeaCommandHandler(Context);

        await handler.Handle(new DeleteIdeaCommand(idea.Id), CancellationToken.None);

        var deleted = await Context.Ideas.FindAsync(idea.Id);
        deleted.Should().BeNull();
    }

    [Test]
    public void ShouldThrowIfNotFound()
    {
        var handler = new DeleteIdeaCommandHandler(Context);

        Func<Task> act = async () => await handler.Handle(
            new DeleteIdeaCommand(12345),
            CancellationToken.None);

        act.Should().ThrowAsync<NotFoundException>();
    }
}
