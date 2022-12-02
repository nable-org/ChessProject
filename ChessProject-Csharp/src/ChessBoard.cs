using src;

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
            // Add pawn with special logic unlice other pieces to be able to track counter
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

        public void Add(ChessPiece piece, int xCoordinate, int yCoordinate)
        {
            // Basic rules to add a new piece
            if (this.IsEmptyBoardPosition(xCoordinate, yCoordinate))
            {
                piece.XCoordinate = xCoordinate;
                piece.YCoordinate = yCoordinate;
                piece.ChessBoard = this;
                pieces.AddPiece(piece);
            }
            else
            {
                // Set pieces's coordiantes to the invalid ones, do not add to the pieces collections and do not set the chessboard
                piece.XCoordinate = Constants.InvalidXCoordinate;
                piece.YCoordinate = Constants.InvalidYCoordinate;
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
        public void InvalidateBoardAfterMove(ChessPiece piece, int xCoordinate, int yCoordinate, int newX, int newY)
        {
            this.pieces[xCoordinate, yCoordinate] = null;
            this.pieces[newX, newY] = piece;

            piece.XCoordinate = newX;
            piece.YCoordinate = newY;
        }

        /// <summary>
        /// Invalidates the board and the pieces after an allowed (valid) capture.
        /// </summary>
        public void InvalidateBoardAfterCapture(ChessPiece piece)
        {
            this.pieces[piece.XCoordinate, piece.YCoordinate] = null;

            // Set as captured in order to be able to track history and restore moves in a future functionality via memento
            piece.SetAsCaptured();
            pieces.CapturedPieces.Add(piece);
        }
    }
}
