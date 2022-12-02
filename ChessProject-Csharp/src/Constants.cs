using System;
using System.Collections.Generic;
using System.Text;

namespace src
{
    public class Constants
    {
        public class ChessBoard
        {
            public const int MaxBoardWidth = 7;
            public const int MaxBoardHeight = 7;

            /// <summary>
            /// The first row for the <see cref="SolarWinds.MSP.Chess.PieceColor.Black"/> pawns, aka from where they are allowed to start with a 2-position move.
            /// </summary>
            public const int BlackStartRow = 6;
            /// <summary>
            /// The first row for the <see cref="SolarWinds.MSP.Chess.PieceColor.White"/> pawns, aka from where they are allowed to start with a 2-position move.
            /// </summary>
            public const int WhiteStartRow = 1;
        }

        public const int InvalidXCoordinate = -1;
        public const int InvalidYCoordinate = -1;
    }
}
