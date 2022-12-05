using NUnit.Framework;

namespace SolarWinds.MSP.Chess
{
    [TestFixture]
    public partial class PawnTest
    {
        [Test]
        public void Capture_WhitePawn_ByBlackPawn_Left()
        {
            chessBoard.Add(pawn, 5, 7, PieceColor.Black);
            Pawn pawnToCapture = new Pawn(PieceColor.White);
            chessBoard.Add(pawnToCapture, 4, 6, PieceColor.White);
            pawn.Move(MovementType.Capture, 4, 6);

            // Assert white pawn is captured
            Assert.IsTrue(pawnToCapture.IsCaptured);
        }

        [Test]
        public void Capture_WhitePawn_ByBlackPawn_Right()
        {
            chessBoard.Add(pawn, 5, 6, PieceColor.Black);
            Pawn pawnToCapture = new Pawn(PieceColor.White);
            chessBoard.Add(pawnToCapture, 4, 7, PieceColor.White);
            pawn.Move(MovementType.Capture, 4, 7);

            // Assert white pawn is captured
            Assert.IsTrue(pawnToCapture.IsCaptured);
        }

        [Test]
        public void Capture_WhitePawn_ByBlackPawn_Left_UpdatesCoordinates()
        {
            chessBoard.Add(pawn, 5, 7, PieceColor.Black);
            Pawn pawnToCapture = new Pawn(PieceColor.White);
            chessBoard.Add(pawnToCapture, 4, 6, PieceColor.White);
            pawn.Move(MovementType.Capture, 4, 6);

            // Assert black pawn is on new position
            Assert.AreEqual(pawn.XCoordinate, 4);
            Assert.AreEqual(pawn.YCoordinate, 6);
        }

        [Test]
        public void Capture_WhitePawn_ByBlackPawn_Right_UpdatesCoordinates()
        {
            chessBoard.Add(pawn, 5, 6, PieceColor.Black);
            Pawn pawnToCapture = new Pawn(PieceColor.White);
            chessBoard.Add(pawnToCapture, 4, 7, PieceColor.White);
            pawn.Move(MovementType.Capture, 4, 7);

            // Assert black pawn is on new position
            Assert.AreEqual(pawn.XCoordinate, 4);
            Assert.AreEqual(pawn.YCoordinate, 7);
        }

        [Test]
        public void NoCapture_ByBlackPawn_Right_MissingPiece_DoesNotMove()
        {
            chessBoard.Add(pawn, 5, 6, PieceColor.Black);
            pawn.Move(MovementType.Capture, 4, 7);

            Assert.AreEqual(pawn.XCoordinate, 5);
            Assert.AreEqual(pawn.YCoordinate, 6);
        }

        [Test]
        public void NoCapture_ByBlackPawn_Left_MissingPiece_DoesNotMove()
        {
            chessBoard.Add(pawn, 5, 7, PieceColor.Black);
            pawn.Move(MovementType.Capture, 4, 6);

            Assert.AreEqual(pawn.XCoordinate, 5);
            Assert.AreEqual(pawn.YCoordinate, 7);
        }

        [Test]
        public void NoCapture_ByBlackPawn_Right_SameColorPiece()
        {
            chessBoard.Add(pawn, 5, 6, PieceColor.Black);
            Pawn pawnToCapture = new Pawn(PieceColor.Black);
            chessBoard.Add(pawnToCapture, 4, 7, PieceColor.Black);
            pawn.Move(MovementType.Capture, 4, 7);

            // Assert same color piece by black pawn is NOT captured
            Assert.IsFalse(pawnToCapture.IsCaptured);
        }

        [Test]
        public void NoCapture_ByBlackPawn_Left_SameColorPiece()
        {
            chessBoard.Add(pawn, 5, 7, PieceColor.Black);
            Pawn pawnToCapture = new Pawn(PieceColor.Black);
            chessBoard.Add(pawnToCapture, 4, 6, PieceColor.Black);
            pawn.Move(MovementType.Capture, 4, 6);

            // Assert same color piece by black pawn is NOT captured
            Assert.IsFalse(pawnToCapture.IsCaptured);
        }

        [Test]
        public void NoCapture_ByBlackPawn_Right_SameColorPiece_DoesNotMove()
        {
            chessBoard.Add(pawn, 5, 6, PieceColor.Black);
            Pawn pawnToCapture = new Pawn(PieceColor.Black);
            chessBoard.Add(pawnToCapture, 4, 7, PieceColor.Black);
            pawn.Move(MovementType.Capture, 4, 7);

            Assert.AreEqual(pawn.XCoordinate, 5);
            Assert.AreEqual(pawn.YCoordinate, 6);
        }

        [Test]
        public void NoCapture_ByBlackPawn_Left_SameColorPiece_DoesNotMove()
        {
            chessBoard.Add(pawn, 5, 7, PieceColor.Black);
            Pawn pawnToCapture = new Pawn(PieceColor.Black);
            chessBoard.Add(pawnToCapture, 4, 6, PieceColor.Black);
            pawn.Move(MovementType.Capture, 4, 6);

            Assert.AreEqual(pawn.XCoordinate, 5);
            Assert.AreEqual(pawn.YCoordinate, 7);
        }

        #region En Passant tests - not full set because of missing functionality
        [Test]
        public void EnPassant_Capture_WhitePawn_ByBlackPawn_Left()
        {
            chessBoard.Add(pawn, 3, 2, PieceColor.Black);
            Pawn pawnToCapture = new Pawn(PieceColor.White);
            chessBoard.Add(pawnToCapture, 3, 1, PieceColor.White);
            pawn.Move(MovementType.Capture, 2, 1); // note the actual pawn to capture is on (3,1)

            Assert.IsTrue(pawnToCapture.IsCaptured);
        }

        [Test]
        public void EnPassant_Capture_WhitePawn_ByBlackPawn_Right()
        {
            chessBoard.Add(pawn, 3, 2, PieceColor.Black);
            Pawn pawnToCapture = new Pawn(PieceColor.White);
            chessBoard.Add(pawnToCapture, 3, 3, PieceColor.White);
            pawn.Move(MovementType.Capture, 2, 3); // note the actual pawn to capture is on (3,3)

            Assert.IsTrue(pawnToCapture.IsCaptured);
        }

        [Test]
        public void EnPassant_NoCapture_WhitePawn_ByBlackPawn_Left_NotOnCorrectRow()
        {
            chessBoard.Add(pawn, 5, 2, PieceColor.Black);
            Pawn pawnToCapture = new Pawn(PieceColor.White);
            chessBoard.Add(pawnToCapture, 5, 1, PieceColor.White);
            pawn.Move(MovementType.Capture, 4, 1);

            Assert.IsFalse(pawnToCapture.IsCaptured);
        }

        [Test]
        public void EnPassant_NoCapture_WhitePawn_ByBlackPawn_NotOnCorrectRow()
        {
            chessBoard.Add(pawn, 5, 2, PieceColor.Black);
            Pawn pawnToCapture = new Pawn(PieceColor.White);
            chessBoard.Add(pawnToCapture, 5, 3, PieceColor.White);
            pawn.Move(MovementType.Capture, 4, 3);

            Assert.IsFalse(pawnToCapture.IsCaptured);
        }

        [Test]
        public void EnPassant_Capture_WhitePawn_ByBlackPawn_Left_UpdatesCoordinates()
        {
            chessBoard.Add(pawn, 3, 2, PieceColor.Black);
            Pawn pawnToCapture = new Pawn(PieceColor.White);
            chessBoard.Add(pawnToCapture, 3, 1, PieceColor.White);
            pawn.Move(MovementType.Capture, 2, 1);

            Assert.AreEqual(pawn.XCoordinate, 2);
            Assert.AreEqual(pawn.YCoordinate, 1);
        }

        [Test]
        public void EnPassant_Capture_WhitePawn_ByBlackPawn_Right_UpdatesCoordinates()
        {
            chessBoard.Add(pawn, 3, 2, PieceColor.Black);
            Pawn pawnToCapture = new Pawn(PieceColor.White);
            chessBoard.Add(pawnToCapture, 3, 3, PieceColor.White);
            pawn.Move(MovementType.Capture, 2, 3);

            Assert.AreEqual(pawn.XCoordinate, 2);
            Assert.AreEqual(pawn.YCoordinate, 3);
        }

        [Test]
        public void EnPassant_NoCapture_WhitePawn_ByBlackPawn_Left_NotOnCorrectRow_DoesNotMove()
        {
            chessBoard.Add(pawn, 5, 2, PieceColor.Black);
            Pawn pawnToCapture = new Pawn(PieceColor.White);
            chessBoard.Add(pawnToCapture, 5, 1, PieceColor.White);
            pawn.Move(MovementType.Capture, 4, 1);

            Assert.AreEqual(pawn.XCoordinate, 5);
            Assert.AreEqual(pawn.YCoordinate, 2);
        }

        [Test]
        public void EnPassant_NoCapture_WhitePawn_ByBlackPawn_NotOnCorrectRow_DoesNotMove()
        {
            chessBoard.Add(pawn, 5, 2, PieceColor.Black);
            Pawn pawnToCapture = new Pawn(PieceColor.White);
            chessBoard.Add(pawnToCapture, 5, 3, PieceColor.White);
            pawn.Move(MovementType.Capture, 4, 3);

            Assert.AreEqual(pawn.XCoordinate, 5);
            Assert.AreEqual(pawn.YCoordinate, 2);
        }

        #endregion

        // TODO En Passant test

        // TODO white pawn tests
    }
}