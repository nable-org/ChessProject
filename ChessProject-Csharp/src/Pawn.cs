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
                            this.ChessBoard.InvalidateBoardAfterCapture(this);

                            // 2. Create a new piece from the default type via reflection and add it to the board
                            var newPiece = Activator.CreateInstance(Constants.DefaultPawnPromotionType, new object[] { this.PieceColor });
                            this.ChessBoard.Add(newPiece as ChessPiece, newX, newY);
                        }
                        else
                        {
                            this.ChessBoard.InvalidateBoardAfterMove(this, this.XCoordinate, this.YCoordinate, newX, newY);
                        }
                    }
                    break;
                case MovementType.Capture:
                    if (this.CanCapture(newX, newY, out ChessPiece pieceToCapture))
                    {
                        // TODO
                    }
                    break;
                default:
                    throw new NotSupportedException("Unknown MovementType!");
            }
        }

        public override string ToString()
        {
            return CurrentPositionAsString();
        }

        protected string CurrentPositionAsString()
        {
            return string.Format("Current X: {1}{0}Current Y: {2}{0}Piece Color: {3}", Environment.NewLine, XCoordinate, YCoordinate, PieceColor);
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
            // TODO Here actually En Passant pawn move is the only one in chess where capture move does not land on the 
            // square where the actual captured piece is standing
            pieceToCapture = this.ChessBoard.GetPieceFromBoardPosition(newX, newY);
            return false;
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
