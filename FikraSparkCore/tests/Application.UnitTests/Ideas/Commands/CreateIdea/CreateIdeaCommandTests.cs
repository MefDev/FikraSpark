using System.ComponentModel.DataAnnotations;
using Application.UnitTests.Common;
using FikraSparkCore.Application.Ideas.Commands.CreateIdea;
using FluentAssertions;
using NUnit.Framework;

namespace FikraSparkCore.Application.UnitTests.Ideas.Commands.CreateIdea;
[TestFixture]
public class CreateIdeaCommandTests: TestBase
{
    [Test]
    public async Task ShouldCreateIdea()
    {
        var handler = new CreateIdeaCommandHandler(Context);

        var id = await handler.Handle(
            new CreateIdeaCommand("New Idea", "Description")
            ,CancellationToken.None);

        var idea = await Context.Ideas.FindAsync(id);

        idea.Should().NotBeNull();
        idea!.Title.Should().Be("New Idea");
    }

    [Test]
    public void ShouldRequireTitle()
    {
        var handler = new CreateIdeaCommandHandler(Context);

        Func<Task> act = async () => await handler.Handle(
            new CreateIdeaCommand("", "Desc" ),
            CancellationToken.None);

        act.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public void ShouldRequireDescription()
    {
        var handler = new CreateIdeaCommandHandler(Context);

        Func<Task> act = async () => await handler.Handle(
            new CreateIdeaCommand("Title",  ""),
            CancellationToken.None);

        act.Should().ThrowAsync<ValidationException>();
    }
}


