using src;
using System;

namespace SolarWinds.MSP.Chess
{
    public class Queen : ChessPiece
    {
        public Queen(PieceColor pieceColor)
            : base(pieceColor)
        {

        }

        #region ChessPiece implementation
        protected override bool CanMove(int newX, int newY)
        {
            throw new NotImplementedException();
        }

        protected override bool CanCapture(int newX, int newY, out ChessPiece pieceToCapture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}