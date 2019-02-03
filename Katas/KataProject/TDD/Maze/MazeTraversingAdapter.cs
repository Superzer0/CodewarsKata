using System;

namespace KataProject.TDD.Maze
{
    internal interface IMazeTraversingAdapter : IMaze
    {
        (int x, int y) CurrentPosition { get; }
        void MoveRight();
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        bool CanMoveLeft();
        bool CanMoveUp();
        bool CanMoveDown();
        bool CanMoveRight();
    }

    internal class MazeTraversingAdapter : IMazeTraversingAdapter
    {
        private readonly IMaze _maze;

        public MazeTraversingAdapter(IMaze maze)
        {
            _maze = maze ?? throw new ArgumentNullException(nameof(maze));
        }

        public IMaze SetStart((int, int) start)
        {
            _maze.SetStart(start);
            CurrentPosition = start;
            return this;
        }
        
        public IMaze SetExit((int, int) exit)
        {
            _maze.SetExit(exit);
            return this;
        }

        public (int x, int y) Start => _maze.Start;

        public (int x, int y) Exit => _maze.Exit;

        public int this[int i, int y] => _maze[i, y];

        public int DimensionX => _maze.DimensionX;

        public int DimensionY => _maze.DimensionY;

        public (int x, int y) CurrentPosition { get; private set; } = (0, 0);

        public void MoveRight() => MakeMove(RightCoordinate);
        public void MoveUp() => MakeMove(UpCoordinate);
        public void MoveDown() => MakeMove(DownCoordinate);
        public void MoveLeft() => MakeMove(LeftCoordinate);

        public bool CanMoveLeft() => CanMakeMove(LeftCoordinate);
        public bool CanMoveUp() => CanMakeMove(UpCoordinate);
        public bool CanMoveDown() => CanMakeMove(DownCoordinate);
        public bool CanMoveRight() => CanMakeMove(RightCoordinate);
       
        private bool CanMakeMove(Func<(int, int)> coordinates)
        {
            var (proposedX, proposedY) = coordinates();
            return IsMoveOk(proposedX, proposedY);
        }

        private void MakeMove(Func<(int, int)> coordinates)
        {
            var (proposedX, proposedY) = coordinates();
            if (!IsMoveOk(proposedX, proposedY))
                throw new InvalidOperationException("Move is not valid");

            CurrentPosition = (proposedX, proposedY);
        }

        private (int x, int y) LeftCoordinate()
        {
            return (CurrentPosition.x, CurrentPosition.y - 1);
        }

        private (int x, int y) UpCoordinate()
        {
            return (CurrentPosition.x - 1, CurrentPosition.y);
        }

        private (int x, int y) DownCoordinate()
        {
            return (CurrentPosition.x + 1, CurrentPosition.y);
        }

        private (int x, int y) RightCoordinate()
        {
            return (CurrentPosition.x, CurrentPosition.y + 1);
        }

        private bool IsWall(int x, int y)
        {
            return _maze[x, y] == 1;
        }

        private bool IsWithinMaze(int x, int y)
        {
            return DimensionX > x && x >= 0 && DimensionY > y && y >= 0;
        }

        private bool IsMoveOk(int proposedX, int proposedY)
        {
            if (!IsWithinMaze(proposedX, proposedY))
                return false;

            if (IsWall(proposedX, proposedY))
                return false;

            return true;
        }
    }
}
