using SolarWinds.MSP.Chess;
using System.Collections.Generic;

namespace src
{
    internal class ChessPieces
    {
        private int MaxAllowedNumberOfPawnsPerColor = 8;
        private Pawn[,] pawns;

        private Dictionary<PieceColor, short> pawnsCounter;
        // TODO other pieces collections

        internal ChessPieces(int maxBoardWidth, int maxBoardHeight)
        {
            this.pawns = new Pawn[maxBoardWidth + 1, maxBoardHeight + 1];
            this.pawnsCounter = new Dictionary<PieceColor, short>()
            {
                { PieceColor.White, 0 },
                { PieceColor.Black, 0 }
            };
        }

        internal bool CanAddPawn(int xCoordinate, int yCoordinate, PieceColor pieceColor)
        {
            /* Conditions:
                - position is valid (aka within the board)
                - three is not a different piece there empty
                - you have not reached maximum number of pawns allowed
            */
            
            return pawns[xCoordinate, yCoordinate] == null && pawnsCounter[pieceColor] < MaxAllowedNumberOfPawnsPerColor;
        }

        internal void AddPawn(Pawn pawn)
        {
            pawns[pawn.XCoordinate, pawn.YCoordinate] = pawn;
            pawnsCounter[pawn.PieceColor]++;
        }
    }
}
