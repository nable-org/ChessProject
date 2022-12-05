using NUnit.Framework;

namespace SolarWinds.MSP.Chess
{
	[TestFixture]
	public partial class PawnTest
	{
		private ChessBoard chessBoard;
		private Pawn pawn;

		[SetUp]
		public void SetUp()	
		{
			chessBoard = new ChessBoard();
			pawn = new Pawn(PieceColor.Black);
		}

		[Test]
		public void ChessBoard_Add_Sets_XCoordinate()
		{
			chessBoard.Add(pawn, 6, 3, PieceColor.Black);
			Assert.AreEqual(pawn.XCoordinate, 6);
		}

		[Test]
		public void ChessBoard_Add_Sets_YCoordinate()
		{
			chessBoard.Add(pawn, 6, 3, PieceColor.Black);
			Assert.AreEqual(pawn.YCoordinate, 3);
		}

		[Test]
		public void Pawn_Move_IllegalCoordinates_Right_DoesNotMove()
		{
			chessBoard.Add(pawn, 6, 3, PieceColor.Black);
			pawn.Move(MovementType.Move, 5, 4);
            Assert.AreEqual(pawn.XCoordinate, 6);
            Assert.AreEqual(pawn.YCoordinate, 3);
		}

		[Test]
		public void Pawn_Move_IllegalCoordinates_Left_DoesNotMove()
		{
			chessBoard.Add(pawn, 6, 3, PieceColor.Black);
			pawn.Move(MovementType.Move, 5, 2);
            Assert.AreEqual(pawn.XCoordinate, 6);
            Assert.AreEqual(pawn.YCoordinate, 3);
		}

        [Test]
        public void Pawn_Move_NotEmptyPosition_Forward_DoesNotMove()
        {
            chessBoard.Add(pawn, 6, 3, PieceColor.Black);

            Pawn blockingPawn = new Pawn(PieceColor.Black);
            // Right in front
            chessBoard.Add(blockingPawn, 5, 3, PieceColor.White);
            pawn.Move(MovementType.Move, 5, 3);

            Assert.AreEqual(pawn.XCoordinate, 6);
            Assert.AreEqual(pawn.YCoordinate, 3);
        }

        [Test]
		public void Pawn_Move_LegalCoordinates_Forward_UpdatesCoordinates()
		{
			chessBoard.Add(pawn, 6, 3, PieceColor.Black);
			pawn.Move(MovementType.Move, 5, 3);
			Assert.AreEqual(pawn.XCoordinate, 5);
            Assert.AreEqual(pawn.YCoordinate, 3);
		}

        [Test]
        public void Pawn_Move_LegalCoordinates_Forward_TwoPositions_UpdatesCoordinates()
        {
            chessBoard.Add(pawn, 6, 3, PieceColor.Black);
            pawn.Move(MovementType.Move, 4, 3);
            Assert.AreEqual(pawn.XCoordinate, 4);
            Assert.AreEqual(pawn.YCoordinate, 3);
        }

        [Test]
        public void Pawn_Move_LegalCoordinates_Forward_TwoPositions_PieceInPlace_DoesNotMove()
        {
            chessBoard.Add(pawn, 6, 3, PieceColor.Black);

            Pawn blockingPawn = new Pawn(PieceColor.Black);
            // Right in front
            chessBoard.Add(blockingPawn, 5, 3, PieceColor.Black);
            pawn.Move(MovementType.Move, 4, 3);

            Assert.AreEqual(pawn.XCoordinate, 6);
            Assert.AreEqual(pawn.YCoordinate, 3);
        }

        [Test]
        public void Pawn_Move_LegalCoordinates_Forward_TwoPositions_NotFirstRow_DoesNotMove()
        {
            chessBoard.Add(pawn, 5, 3, PieceColor.Black);

            pawn.Move(MovementType.Move, 3, 3);

            Assert.AreEqual(pawn.XCoordinate, 5);
            Assert.AreEqual(pawn.YCoordinate, 3);
        }

        // TODO Tests for white pawns
    }
}
