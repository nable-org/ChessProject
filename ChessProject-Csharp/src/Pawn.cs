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
                        this.ChessBoard.InvalidateBoardAfterMove(this, newX, newY);
                    }
                    break;
                case MovementType.Capture:
                    this.CanCapture(newX, newY);
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

        protected override bool CanCapture(int newX, int newY)
        {
            // TODO Here actually En Passant pawn move is the only one in chess where capture move does not land on the 
            // square where the actual captured piece is standing
            var pieceToCapture = this.ChessBoard.GetPieceFromBoardPosition(newX, newY);
            throw new NotImplementedException();
        }
    }
}
