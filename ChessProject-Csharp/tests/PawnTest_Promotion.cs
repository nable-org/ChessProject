using NUnit.Framework;
using System.Reflection.Metadata;

namespace SolarWinds.MSP.Chess
{
    [TestFixture]
    public partial class PawnTest
    {
        [Test]
        public void WhitePawn_Transforms_After_Reaching_End()
        {
            pawn = new Pawn(PieceColor.White);
            chessBoard.Add(pawn, 6, 6, PieceColor.White);
            pawn.Move(MovementType.Move, 7, 6);
            var newPiece = chessBoard.GetPieceFromBoardPosition(7, 6);
            Assert.IsTrue(newPiece is Queen);
        }

        [Test]
        public void WhitePawn_Considered_Captured_After_Reaching_End()
        {
            pawn = new Pawn(PieceColor.White);
            chessBoard.Add(pawn, 6, 6, PieceColor.White);
            pawn.Move(MovementType.Move, 7, 6);
            Assert.IsTrue(pawn.IsCaptured);
        }

        [Test]
        public void BlackPawn_Transforms_After_Reaching_End()
        {
            chessBoard.Add(pawn, 1, 5, PieceColor.Black);
            pawn.Move(MovementType.Move, 0, 5);
            var newPiece = chessBoard.GetPieceFromBoardPosition(0, 5);
            Assert.AreEqual(newPiece.GetType(), src.Constants.DefaultPawnPromotionType);
        }

        [Test]
        public void BlackPawn_Considered_Captured_After_Reaching_End()
        {
            chessBoard.Add(pawn, 1, 5, PieceColor.Black);
            pawn.Move(MovementType.Move, 0, 5);
            Assert.IsTrue(pawn.IsCaptured);
        }
    }
}