using src;
using System;

namespace SolarWinds.MSP.Chess
{
    public class Pawn : ChessPiece
    {
        public Pawn(PieceColor pieceColor)
            : base(pieceColor)
        {

        }

        public void Move(MovementType movementType, int newX, int newY)
        {
            // If pawn can move update coordinates
            // IMplement capture logic + an passan
            switch (movementType)
            {
                case MovementType.Move:
                    if (this.CanMove(newX, newY))
                    {
                        // Check for pawn promotion
                        if (this.CanPromote(newX))
                        {
                            // 1. Act like pawn is captured and remove it from board
                            this.ChessBoard.PerformCaptureOnBoard(this);

                            // 2. Create a new piece from the default type via reflection and add it to the board
                            var newPiece = Activator.CreateInstance(Constants.DefaultPawnPromotionType, new object[] { this.PieceColor });
                            this.ChessBoard.Add(newPiece as ChessPiece, newX, newY);
                        }
                        else
                        {
                            this.ChessBoard.PerformMoveOnBoard(this, newX, newY);
                        }
                    }
                    break;
                case MovementType.Capture:
                    if (this.CanCapture(newX, newY, out ChessPiece pieceToCapture))
                    {
                        // 1. Captured the piece and remove it from board
                        this.ChessBoard.PerformCaptureOnBoard(pieceToCapture);

                        // 2. Move the actual pawn
                        this.ChessBoard.PerformMoveOnBoard(this, newX, newY);
                    }
                    break;
                default:
                    throw new NotSupportedException("Unknown MovementType!");
            }
        }

        #region ChessPiece implementation
        protected override bool CanMove(int newX, int newY)
        {
            // Move black down, white up one place, or if initial position and no piece to jump, move two places
            // and always land on empty board position
            return this.ChessBoard.IsEmptyBoardPosition(newX, newY) && this.YCoordinate == newY &&
                ((this.PieceColor == PieceColor.Black &&
                    (this.XCoordinate == newX + 1 ||
                        (this.XCoordinate == Constants.ChessBoard.BlackStartRow && this.XCoordinate == newX + 2 && this.ChessBoard.IsEmptyBoardPosition(newX + 1, this.YCoordinate)))) ||
                (this.PieceColor == PieceColor.White &&
                    (this.XCoordinate == newX - 1 ||
                        (this.XCoordinate == Constants.ChessBoard.WhiteStartRow && this.XCoordinate == newX - 2 && this.ChessBoard.IsEmptyBoardPosition(newX - 1, this.YCoordinate)))));
        }

        protected override bool CanCapture(int newX, int newY, out ChessPiece pieceToCapture)
        {
            bool result = false;
            pieceToCapture = this.ChessBoard.GetPieceFromBoardPosition(newX, newY);
            if (pieceToCapture != null)
            {
                // Normal pawn capture
                result = pieceToCapture.PieceColor != this.PieceColor;
            }
            else
            {
                /* En Passant
                 * Here actually En Passant pawn move is the only one in chess where capture move does not land on the 
                 * square where the actual captured piece is standing
                 * Requirements:
                 *  - The capturing pawn must have advanced exactly three ranks to perform this move.
                 *  - The captured pawn must have moved two squares in one move, landing right next to the capturing pawn.
                 *  - The en passant capture must be performed on the turn immediately after the pawn being captured moves. If the player does not capture en passant on that turn, they no longer can do it later.
                */

                bool rule1 = this.PieceColor == PieceColor.Black && this.XCoordinate == Constants.ChessBoard.BlackStartRow - 3 ||
                    this.PieceColor == PieceColor.White && this.XCoordinate == Constants.ChessBoard.WhiteStartRow + 3;

                // TODO implement later with history of moves
                bool rule2 = true, rule3 = true;

                int xCoordinateToCheck = this.XCoordinate; // same row as the current pawn pos
                int yCoordinateToCheck = newY; // same column as the requested

                pieceToCapture = this.ChessBoard.GetPieceFromBoardPosition(xCoordinateToCheck, yCoordinateToCheck);

                if (pieceToCapture != null && pieceToCapture.PieceColor != this.PieceColor && pieceToCapture.GetType() == typeof(Pawn) &&
                    rule1 && rule2 && rule3) 
                {
                    result = true;
                }
                else
                {
                    pieceToCapture = null;
                }
            }

            return result;
        }
        #endregion

        #region Private members
        private bool CanPromote(int newX)
        {
            // Note that move is already valid
            return this.PieceColor == PieceColor.Black && newX == 0 ||
                this.PieceColor == PieceColor.White && newX == Constants.ChessBoard.MaxBoardHeight;
        }
        #endregion
    }
}
