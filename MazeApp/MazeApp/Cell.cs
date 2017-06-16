using System;

namespace MazeApp
{
    public class Cell : IEquatable<Cell>
    {
        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; set; }
        public int Col { get; set; }

        public bool Equals(Cell other)
        {
            if (other == null)
                return false;

            return Row == other.Row && Col == other.Col;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Cell))
                return false;

            return Equals(obj);
        }

        // in case we add Cell to HashSet or Dictionary etc they will rely on this method
        public override int GetHashCode()
        {
            return Row ^ Col;
        }
    }
}