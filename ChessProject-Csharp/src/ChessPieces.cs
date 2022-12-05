using SolarWinds.MSP.Chess;
using System;
using System.Collections.Generic;

namespace src
{
    internal class ChessPieces
    {
        private int MaxAllowedNumberOfPawnsPerColor = 8;
        private ChessPiece[,] allPieces;

        private List<ChessPiece> capturedPieces;

        [Obsolete]
        private Pawn[,] pawns;
        private Dictionary<PieceColor, short> pawnsCounter;

        // Declare all other pieces collections

        internal ChessPieces(int maxBoardWidth, int maxBoardHeight)
        {
            this.allPieces = new ChessPiece[maxBoardWidth + 1, maxBoardHeight + 1];
            this.capturedPieces = new List<ChessPiece>();

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
            set
            {
                this.allPieces[xCoordinate, yCoordinate] = value;
            }
        }

        public List<ChessPiece> CapturedPieces
        {
            get
            {
                return this.capturedPieces;
            }
        }

        internal bool CanAddPawn(int xCoordinate, int yCoordinate, PieceColor pieceColor)
        {
            /* Conditions:
                - position is valid (aka within the board)
                - there is not a different piece there aka empty
                - you have not reached maximum number of pawns allowed
            */
            
            return this.IsEmptyPosition(xCoordinate, yCoordinate) && pawnsCounter[pieceColor] < MaxAllowedNumberOfPawnsPerColor;
            //pawns[xCoordinate, yCoordinate] == null;
        }

        internal void AddPawn(Pawn pawn)
        {
            pawns[pawn.XCoordinate, pawn.YCoordinate] = pawn;
            pawnsCounter[pawn.PieceColor]++;

            this.AddPiece(pawn);
        }

        internal void AddPiece(ChessPiece piece)
        {
            this.allPieces[piece.XCoordinate, piece.YCoordinate] = piece;
        }

        internal bool IsEmptyPosition(int xCoordinate, int yCoordinate)
        {
            // Loop all collections and see if position is empty or (better) just keep track of pieces in one array regardless of type
            return this.allPieces[xCoordinate, yCoordinate] == null;
        }
    }
}
