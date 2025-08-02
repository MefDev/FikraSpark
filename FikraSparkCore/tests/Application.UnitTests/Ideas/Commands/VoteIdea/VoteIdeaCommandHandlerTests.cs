using Application.UnitTests.Common;
using FikraSparkCore.Application.Ideas.Commands.VoteIdea;
using FikraSparkCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FikraSparkCore.Application.UnitTests.Ideas.Commands.VoteIdea;


[TestFixture]
public class VoteIdeaCommandHandlerTests : TestBase
{
    private VoteIdeaCommandHandler _handler = null!;
    
    [SetUp]
    public void TestSetUp()
    {
        _handler = new VoteIdeaCommandHandler(Context);
    }
    
    [Test]
    public async Task Handle_ShouldIncrementVotes_WhenDeltaIsPositive()
    {
        // Arrange
        var idea = new Idea("Test Title", "Test Description", 0);
        Context.Ideas.Add(idea);
        await Context.SaveChangesAsync();

        var command = new VoteIdeaCommand(idea.Id, 1);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        var updated = await Context.Ideas.FirstAsync();
        Assert.That(updated.Votes, Is.EqualTo(1));
    }

    [Test]
    public async Task Handle_ShouldDecrementVotes_WhenDeltaIsNegative()
    {
        var idea = new Idea("Title", "Desc", 5);
        Context.Ideas.Add(idea);
        await Context.SaveChangesAsync();

        var command = new VoteIdeaCommand(idea.Id, -1);

        await _handler.Handle(command, CancellationToken.None);

        var updated = await Context.Ideas.FindAsync(idea.Id);
        if (updated != null)
        {
            Assert.That(updated.Votes, Is.EqualTo(4));
        }
    }

    [Test]
    public void Handle_ShouldThrow_WhenDeltaIsInvalid()
    {
        var idea = new Idea("Title", "Desc", 0);
        Context.Ideas.Add(idea);
        Context.SaveChanges();

        var command = new VoteIdeaCommand(idea.Id, 5);

        Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public void Handle_ShouldThrow_WhenIdeaNotFound()
    {
        var command = new VoteIdeaCommand(999, 1);

        Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }
}
