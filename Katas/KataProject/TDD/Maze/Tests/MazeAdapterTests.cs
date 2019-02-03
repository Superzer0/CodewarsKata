using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;

namespace KataProject.TDD.Maze.Tests
{
    [TestFixture]
    public class MazeAdapterTests
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        private IMazeTraversingAdapter CreateSut()
        {
            return new MazeTraversingAdapter(new MazeSolvingDfs().CreateMaze(_fixture.Create<int[,]>()));
        }

        private static readonly IEnumerable<int[,]> MoveRightTestCases = new List<int[,]>
        {
            new[,] { { 0, 0, 1, 0 } },
            new[,] { { 0, 0 } },
        };

        [Test]
        [TestCaseSource(nameof(MoveRightTestCases))]
        public void MazeAdapter_MovingRight_MazeStateIsUpdated(int[,] inputTable)
        {
            _fixture.Inject(inputTable);
            var start = (0, 0);
            var exit = (inputTable.GetLength(0) - 1, inputTable.GetLength(1) - 1);

            var sut = CreateSut();

            sut.SetStart(start).SetExit(exit);

            sut.CanMoveRight().Should().BeTrue();
            sut.MoveRight();
            sut.CanMoveRight().Should().BeFalse();
        }

        private static readonly IEnumerable<int[,]> MoveLeftTestCases = new List<int[,]>
        {
            new[,] { { 0, 1, 0, 0 } },
            new[,] { { 0, 0 } },
        };

        [Test]
        [TestCaseSource(nameof(MoveLeftTestCases))]
        public void MazeAdapter_MovingLeft_MazeStateIsUpdated(int[,] inputTable)
        {
            _fixture.Inject(inputTable);
            var start = (inputTable.GetLength(0) - 1, inputTable.GetLength(1) - 1);
            var exit = (0, 0);

            var sut = CreateSut();

            sut.SetStart(start).SetExit(exit);

            sut.CanMoveLeft().Should().BeTrue();
            sut.MoveLeft();
            sut.CanMoveLeft().Should().BeFalse();
        }

        private static readonly IEnumerable<int[,]> MoveUpTestCases = new List<int[,]>
        {
            new[,] {{1}, {0}, {0}},
            new[,] {{0}, {0}},
        };

        [Test]
        [TestCaseSource(nameof(MoveUpTestCases))]
        public void MazeAdapter_MovingUp_MazeStateIsUpdated(int[,] inputTable)
        {
            _fixture.Inject(inputTable);
            var start = (inputTable.GetLength(0) - 1, inputTable.GetLength(1) - 1);
            var exit = (0, 0);

            var sut = CreateSut();

            sut.SetStart(start).SetExit(exit);

            sut.CanMoveUp().Should().BeTrue();
            sut.MoveUp();
            sut.CanMoveUp().Should().BeFalse();
        }

        private static readonly IEnumerable<int[,]> MoveDownTestCases = new List<int[,]>
        {
            new[,] {{0}, {0}, {1}},
            new[,] {{0}, {0}},
        };

        [Test]
        [TestCaseSource(nameof(MoveDownTestCases))]
        public void MazeAdapter_MovingDown_MazeStateIsUpdated(int[,] inputTable)
        {
            _fixture.Inject(inputTable);
            var start = (0, 0);
            var exit = (inputTable.GetLength(0) - 1, inputTable.GetLength(1) - 1);

            var sut = CreateSut();

            sut.SetStart(start).SetExit(exit);

            sut.CanMoveDown().Should().BeTrue();
            sut.MoveDown();
            sut.CanMoveDown().Should().BeFalse();
        }

        [Test]
        public void MazeAdapter_MovingInProperly_ExceptionThrownPositionNotUpdated()
        {
            var inputTable = new int[,] { { 0 } };
            _fixture.Inject(inputTable);
            var start = (0, 0);
            var exit = (0, 0);

            var sut = CreateSut();

            sut.SetStart(start).SetExit(exit);
            var expectedPosition = sut.CurrentPosition;

            sut.Invoking(p => p.MoveLeft()).Should().Throw<InvalidOperationException>();
            sut.Invoking(p => p.MoveRight()).Should().Throw<InvalidOperationException>();
            sut.Invoking(p => p.MoveUp()).Should().Throw<InvalidOperationException>();
            sut.Invoking(p => p.MoveDown()).Should().Throw<InvalidOperationException>();

            sut.CurrentPosition.Should().Be(expectedPosition);
        }
    }
}
