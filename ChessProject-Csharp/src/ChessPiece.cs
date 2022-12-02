using SolarWinds.MSP.Chess;
using System;

namespace src
{
    public abstract class ChessPiece
    {
        private ChessBoard chessBoard;
        private int xCoordinate;
        private int yCoordinate;
        private PieceColor pieceColor;

        public ChessPiece(PieceColor pieceColor)
        {
            this.pieceColor = pieceColor;
        }

        public ChessBoard ChessBoard
        {
            get { return chessBoard; }
            set { chessBoard = value; }
        }

        public int XCoordinate
        {
            get { return xCoordinate; }
            set { xCoordinate = value; }
        }

        public int YCoordinate
        {
            get { return yCoordinate; }
            set { yCoordinate = value; }
        }

        public PieceColor PieceColor
        {
            get { return pieceColor; }
            private set { pieceColor = value; }
        }

        protected abstract bool CanMove(int newX, int newY);

        protected abstract bool CanCapture(int newX, int newY);
    }
}
