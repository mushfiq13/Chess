using Chess.Web.Application.Services;
using Chess.Web.Domain.Board;
using Chess.Web.Domain.Piece.Base;

namespace Chess.Web.Domain.Piece;

internal class Bishop : BasePiece
{
	public Bishop(Color color, string name, int rank, int file)
		: base(color, name, rank, file) { }

	public override bool CanMove(IBoard board, int toRank, int toFile)
	{
		// Bishop can go only to these directoins.
		int[] xDir = [+1, +1, -1, -1];
		int[] yDir = [+1, -1, +1, -1];

		for (var i = 0; i < xDir.Length; ++i) {
			var isValid = this.CanMoveWithMultipleJump(
				xDir[i], yDir[i], toRank, toFile, board);

			if (isValid)
				return true;
		}

		return false;
	}
}
