using NUnit.Framework;
using src;

namespace SolarWinds.MSP.Chess
{
    [TestFixture]
	public class ChessBoardTest
	{
		private ChessBoard chessBoard;

        [SetUp]
		public void SetUp()
		{
			chessBoard = new ChessBoard();
		}

        [Test]
		public void Has_MaxBoardWidth_of_7()
		{
			Assert.AreEqual(Constants.ChessBoard.MaxBoardWidth, 7);
		}

        [Test]
		public void Has_MaxBoardHeight_of_7()
		{
			Assert.AreEqual(Constants.ChessBoard.MaxBoardHeight, 7);
		}

        [Test]
		public void IsLegalBoardPosition_True_X_equals_0_Y_equals_0()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(0, 0);
			Assert.IsTrue(isValidPosition);
		}

        [Test]
		public void IsLegalBoardPosition_True_X_equals_5_Y_equals_5()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(5, 5);
            Assert.IsTrue(isValidPosition);
		}

        [Test]
		public void IsLegalBoardPosition_False_X_equals_11_Y_equals_5()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(11, 5);
            Assert.IsFalse(isValidPosition);
		}

        [Test]
		public void IsLegalBoardPosition_False_X_equals_0_Y_equals_9()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(0, 9);
            Assert.IsFalse(isValidPosition);
		}

        [Test]
		public void IsLegalBoardPosition_False_X_equals_11_Y_equals_0()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(11, 0);
            Assert.IsFalse(isValidPosition);
		}

        [Test]
		public void IsLegalBoardPosition_False_For_Negative_X_Values()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(-1, 5);
            Assert.IsFalse(isValidPosition);
		}

        [Test]
		public void IsLegalBoardPosition_False_For_Negative_Y_Values()
		{
			var isValidPosition = chessBoard.IsLegalBoardPosition(5, -1);
            Assert.IsFalse(isValidPosition);
		}

        [Test]
        public void IsEmptyBoardPosition_False_For_Existing_Piece()
        {
            Pawn pawn = new Pawn(PieceColor.Black);
            chessBoard.Add(pawn, 6, 3, PieceColor.Black);

            var isEmptyPosition = chessBoard.IsEmptyBoardPosition(6, 3);

            Assert.IsFalse(isEmptyPosition);
        }

        [Test]
        public void IsEmptyBoardPosition_True_For_Missing_Piece()
        {
            Pawn pawn = new Pawn(PieceColor.Black);
            chessBoard.Add(pawn, 6, 3, PieceColor.Black);

            var isEmptyPosition = chessBoard.IsEmptyBoardPosition(5, 3);

            Assert.IsTrue(isEmptyPosition);
        }

        [Test]
		public void Avoids_Duplicate_Positioning()
		{
			Pawn firstPawn = new Pawn(PieceColor.Black);
			Pawn secondPawn = new Pawn(PieceColor.Black);
			chessBoard.Add(firstPawn, 6, 3, PieceColor.Black);
			chessBoard.Add(secondPawn, 6, 3, PieceColor.Black);
			Assert.AreEqual(firstPawn.XCoordinate, 6);
            Assert.AreEqual(firstPawn.YCoordinate, 3);
            Assert.AreEqual(secondPawn.XCoordinate, -1);
            Assert.AreEqual(secondPawn.YCoordinate, -1);
		}

        [Test]
		public void Limits_The_Number_Of_Pawns()
		{
			for (int i = 0; i < 10; i++)
			{
				Pawn pawn = new Pawn(PieceColor.Black);
				int row = i / (Constants.ChessBoard.MaxBoardWidth + 1),
					column = i % (Constants.ChessBoard.MaxBoardWidth + 1);
				chessBoard.Add(pawn, 6 + row, column, PieceColor.Black);
				if (row < 1)
				{
					Assert.AreEqual(pawn.XCoordinate, (6 + row));
					Assert.AreEqual(pawn.YCoordinate, column);
				}
				else
				{
					Assert.AreEqual(pawn.XCoordinate, -1);
                    Assert.AreEqual(pawn.YCoordinate, -1);
				}
			}
		}

		[Test]
        public void GetPieceFromBoardPosition_NotNull_For_Existing_Piece()
		{
            Pawn pawn = new Pawn(PieceColor.Black);
            chessBoard.Add(pawn, 6, 3, PieceColor.Black);

            var chessPiece = chessBoard.GetPieceFromBoardPosition(6, 3);

            Assert.IsNotNull(chessPiece);
        }

        [Test]
        public void GetPieceFromBoardPosition_ExactPiece_For_Existing_Piece()
        {
            Pawn pawn = new Pawn(PieceColor.Black);
            chessBoard.Add(pawn, 6, 3, PieceColor.Black);

            var chessPiece = chessBoard.GetPieceFromBoardPosition(6, 3);

			Assert.AreSame(pawn, chessPiece);
        }

        [Test]
        public void GetPieceFromBoardPosition_Null_For_Missing_Piece()
        {
            Pawn pawn = new Pawn(PieceColor.Black);
            chessBoard.Add(pawn, 6, 3, PieceColor.Black);

            var chessPiece = chessBoard.GetPieceFromBoardPosition(7, 4);

            Assert.IsNull(chessPiece);
        }

        [Test]
        public void GetPieceFromBoardPosition_Null_For_Invalid_Position()
        {
            Pawn pawn = new Pawn(PieceColor.Black);
            chessBoard.Add(pawn, 6, 3, PieceColor.Black);

            var chessPiece = chessBoard.GetPieceFromBoardPosition(9, 1);

            Assert.IsNull(chessPiece);
        }
    }
}
