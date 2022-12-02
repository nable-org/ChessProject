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
        private bool isCaptured;

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

        public bool IsCaptured
        {
            get
            {
                return this.isCaptured;
            }
            private set
            {
                this.isCaptured = value;
            }
        }

        public void SetAsCaptured()
        {
            // Move piece "out of the board"
            this.isCaptured = true;
            this.XCoordinate = Constants.InvalidXCoordinate;
            this.YCoordinate = Constants.InvalidYCoordinate;
        }

        protected abstract bool CanMove(int newX, int newY);

        protected abstract bool CanCapture(int newX, int newY, out ChessPiece pieceToCapture);

        public override string ToString()
        {
            return CurrentPositionAsString();
        }

        protected string CurrentPositionAsString()
        {
            return string.Format("Current X: {1}{0}Current Y: {2}{0}Piece Color: {3}", Environment.NewLine, XCoordinate, YCoordinate, PieceColor);
        }
    }
}
