using SolarWinds.MSP.Chess;
using System;
using System.Collections.Generic;

namespace src
{
    internal class ChessPieces
    {
        private int MaxAllowedNumberOfPawnsPerColor = 8;
        private ChessPiece[,] allPieces;

        private Pawn[,] pawns;
        private Dictionary<PieceColor, short> pawnsCounter;

        // Declare all other pieces collections

        internal ChessPieces(int maxBoardWidth, int maxBoardHeight)
        {
            this.allPieces = new ChessPiece[maxBoardWidth + 1, maxBoardHeight + 1];

            // init all individual collections of pieces
            this.pawns = new Pawn[maxBoardWidth + 1, maxBoardHeight + 1];
            this.pawnsCounter = new Dictionary<PieceColor, short>()
            {
                { PieceColor.White, 0 },
                { PieceColor.Black, 0 }
            };
        }

        public ChessPiece this[int xCoordinate, int yCoordinate]
        {
            get
            {
                return this.allPieces[xCoordinate, yCoordinate];
            }
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

            this.allPieces[pawn.XCoordinate, pawn.YCoordinate] = pawn;
        }

        internal bool IsEmptyPosition(int xCoordinate, int yCoordinate)
        {
            // Loop all collections and see if position is empty or (better) just keep track of pieces in one array regardless of type
            return this.allPieces[xCoordinate, yCoordinate] == null;
        }
    }
}
