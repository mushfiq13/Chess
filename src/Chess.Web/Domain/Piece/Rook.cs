using Chess.Web.Application.Services;
using Chess.Web.Domain.Board;
using Chess.Web.Domain.Piece.Base;

namespace Chess.Web.Domain.Piece;

internal class Rook : BasePiece
{
	public Rook(Color color, string name, int rank, int file)
		: base(color, name, rank, file) { }

	public override bool CanMove(IBoard board, int toRank, int toFile)
	{
		int[] xDir = [+1, +0, -1, +0];
		int[] yDir = [+0, +1, +0, -1];

		for (var i = 0; i < xDir.Length; ++i) {
			var isValid = this.CanMoveWithMultipleJump(
							 xDir[i], yDir[i], toRank, toFile, board);

			if (isValid)
				return true;
		}

		return false;
	}
}

