using System;

namespace KataProject.TDD.Maze
{
    public interface IMaze
    {
        IMaze SetStart((int, int) start);
        IMaze SetExit((int, int) exit);
        (int x, int y) Start { get; }
        (int x, int y) Exit { get; }
        int this[int i, int y] { get; }
        int DimensionX { get; }
        int DimensionY { get; }
    }

    internal class Maze : IMaze
    {
        private readonly int[,] _inputTable;

        internal Maze(int[,] inputTable)
        {
            _inputTable = inputTable ?? throw new ArgumentNullException(nameof(inputTable)); ;
        }

        public IMaze SetStart((int, int) start)
        {
            IsCoordinateCorrect(start);
            Start = start;
            return this;
        }

        public IMaze SetExit((int, int) exit)
        {
            IsCoordinateCorrect(exit);
            Exit = exit;
            return this;
        }

        public (int x, int y) Start { get; private set; }
        public (int x, int y) Exit { get; private set; }

        public int this[int x, int y] => _inputTable[x, y];
        public int DimensionX => _inputTable.GetLength(0);
        public int DimensionY => _inputTable.GetLength(1);

        private void IsCoordinateCorrect((int x, int y) point)
        {
            if (point.x < 0)
                throw new ArgumentException("Cannot be < 0", nameof(point.x));

            if (point.y < 0)
                throw new ArgumentException("Cannot be < 0", nameof(point.y));

            if (point.x >= DimensionX)
                throw new ArgumentException("Cannot be greater than maze x index", nameof(point.x));

            if (point.y >= DimensionY)
                throw new ArgumentException("Cannot be greater than maze y index", nameof(point.y));
        }
    }
}
