using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;

namespace KataProject.TDD.Maze.Tests
{
    [TestFixture]
    public class MazeTests
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        private static readonly IEnumerable<Func<IFixture, IMaze>> TestCases = new List<Func<IFixture, IMaze>>
        {
            fixture => new MazeSolvingDfs().CreateMaze(fixture.Create<int[,]>()),
            fixture => new MazeTraversingAdapter(new MazeSolvingDfs().CreateMaze(fixture.Create<int[,]>()))
        };

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void Maze_IsEmpty_ExceptionIsThrown(Func<IFixture, IMaze> createSut)
        {
            var anonymousInputTable = new int[0, 0];
            _fixture.Inject(anonymousInputTable);
            createSut.Invoking(p => p(_fixture)).Should().Throw<ArgumentException>();
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void Maze_SetStartAndFinish_Correctly(Func<IFixture, IMaze> createSut)
        {
            var anonymousInputTable = _fixture.Freeze<int[,]>();
            var start = (0, 0);
            var exit = (anonymousInputTable.GetLength(0) - 1, anonymousInputTable.GetLength(1) - 1);

            var sut = createSut(_fixture);

            sut.SetStart(start)
                .SetExit(exit);

            sut.Start.Should().Be(start);
            sut.Exit.Should().Be(exit);
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void Maze_SetStartAndFinish_OutOfBounds_ExceptionThrown(Func<IFixture, IMaze> createSut)
        {
            var anonymousInputTable = _fixture.Freeze<int[,]>();
            var anonymousLowerBoundCoordinate = (-1, -1);
            var anonymousUpperBoundCoordinate = (anonymousInputTable.GetLength(0) + 1, anonymousInputTable.GetLength(1) + 1);

            var sut = createSut(_fixture);

            sut.Invoking(p => p.SetStart(anonymousLowerBoundCoordinate)).Should().Throw<ArgumentException>();
            sut.Invoking(p => p.SetExit(anonymousLowerBoundCoordinate)).Should().Throw<ArgumentException>();

            sut.Invoking(p => p.SetStart(anonymousUpperBoundCoordinate)).Should().Throw<ArgumentException>();
            sut.Invoking(p => p.SetExit(anonymousUpperBoundCoordinate)).Should().Throw<ArgumentException>();
        }

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void Maze_ImplementsIndexer(Func<IFixture, IMaze> createSut)
        {
            var anonymousInputTable = _fixture.Freeze<int[,]>();
            var expectedValue = _fixture.Create<int>();
            anonymousInputTable[0, 0] = expectedValue;

            var sut = createSut(_fixture);

            sut[0, 0].Should().Be(expectedValue);
        }
    }
}
