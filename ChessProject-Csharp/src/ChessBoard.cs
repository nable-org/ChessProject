using src;
using System;
using System.Runtime.CompilerServices;

namespace SolarWinds.MSP.Chess
{
    public class ChessBoard
    {
        private ChessPieces pieces;

        public ChessBoard ()
        {
            pieces = new ChessPieces(Constants.ChessBoard.MaxBoardWidth, Constants.ChessBoard.MaxBoardHeight);
        }

        public void Add(Pawn pawn, int xCoordinate, int yCoordinate, PieceColor pieceColor)
        {
            if (this.IsLegalBoardPosition(xCoordinate, yCoordinate) && pieces.CanAddPawn(xCoordinate, yCoordinate, pieceColor))
            {
                pawn.XCoordinate = xCoordinate;
                pawn.YCoordinate = yCoordinate;
                pawn.ChessBoard = this;
                pieces.AddPawn(pawn);
            }
            else
            {
                // Set pawn's coordiantes to the invalid ones, do not add to the pieces collections and do not set the chessboard
                pawn.XCoordinate = Constants.InvalidXCoordinate;
                pawn.YCoordinate = Constants.InvalidYCoordinate;
            }
        }

        public bool IsLegalBoardPosition(int xCoordinate, int yCoordinate)
        {
            return 0 <= xCoordinate && xCoordinate <= Constants.ChessBoard.MaxBoardWidth &&
                0 <= yCoordinate && yCoordinate <= Constants.ChessBoard.MaxBoardHeight;
        }

        public bool IsEmptyBoardPosition(int xCoordinate, int yCoordinate)
        {
            return this.IsLegalBoardPosition(xCoordinate, yCoordinate) && this.pieces.IsEmptyPosition(xCoordinate, yCoordinate);
        }

        public ChessPiece GetPieceFromBoardPosition(int xCoordinate, int yCoordinate)
        {
            return this.IsLegalBoardPosition(xCoordinate, yCoordinate) ? this.pieces[xCoordinate, yCoordinate] : null;
        }

        /// <summary>
        /// Invalidates the board and the pieces after an allowed (valid) move.
        /// </summary>
        public void InvalidateBoardAfterMove(ChessPiece piece, int newX, int newY)
        {
            piece.XCoordinate = newX;
            piece.YCoordinate = newY;
            // TODO Update pieces arrays for positions
            // Remove captured figures
            // Make pawn transformations
            // Make castle
        }
    }
}
