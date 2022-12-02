using NUnit.Framework;

namespace SolarWinds.MSP.Chess
{
    [TestFixture]
    public partial class PawnTest
    {
        [Test]
        public void My_Capture()
        {
            chessBoard.Add(pawn, 6, 3, PieceColor.Black);
            Assert.AreEqual(pawn.XCoordinate, 6);
        }

        /* 
         * TODO capture tests + En Passant
         *  There are a few requirements for the move to be legal:
         *  - The capturing pawn must have advanced exactly three ranks to perform this move.
         *  - The captured pawn must have moved two squares in one move, landing right next to the capturing pawn.
         *  - The en passant capture must be performed on the turn immediately after the pawn being captured moves. If the player does not capture en passant on that turn, they no longer can do it later.
        */
    }
}