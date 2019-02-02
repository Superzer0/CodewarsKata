using System;
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

        private IMaze CreateSut()
        {
            return new MazeSolvingDfs().CreateMaze(_fixture.Create<int[,]>());
        }

        [Test]
        public void Maze_SetStartAndFinish_Correctly()
        {
            var anonymousInputTable = _fixture.Freeze<int[][]>();
            var start = (0, 0);
            var exit = (anonymousInputTable.Length - 1, anonymousInputTable[0].Length - 1);

            var sut = CreateSut();

            sut.SetStart(start)
                .SetExit(exit);

            sut.Start.Should().Be(start);
            sut.Exit.Should().Be(exit);
        }

        [Test]
        public void Maze_SetStartAndFinish_OutOfBounds_ExceptionThrown()
        {
            var anonymousInputTable = _fixture.Freeze<int[][]>();
            var anonymousLowerBoundCoordinate = (-1, -1);
            var anonymousUpperBoundCoordinate = (anonymousInputTable.Length + 1, anonymousInputTable[0].Length + 1);

            var sut = CreateSut();

            sut.Invoking(p => p.SetStart(anonymousLowerBoundCoordinate)).Should().Throw<ArgumentException>();
            sut.Invoking(p => p.SetExit(anonymousLowerBoundCoordinate)).Should().Throw<ArgumentException>();

            sut.Invoking(p => p.SetStart(anonymousUpperBoundCoordinate)).Should().Throw<ArgumentException>();
            sut.Invoking(p => p.SetExit(anonymousUpperBoundCoordinate)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Maze_ImplementsIndexer()
        {
            var anonymousInputTable = _fixture.Freeze<int[][]>();
            var expectedValue = _fixture.Create<int>();
            anonymousInputTable[0][0] = expectedValue;

            var sut = CreateSut();

            sut[0, 0].Should().Be(expectedValue);
        }
    }
}
