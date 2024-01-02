using Chess.Web.Application.Services;
using Chess.Web.Domain.Board;
using Chess.Web.Domain.Piece.Base;

namespace Chess.Web.Domain.Piece;

internal class Knight : BasePiece
{
	public Knight(Color color, string name, int rank, int file)
		: base(color, name, rank, file) { }

	public override bool CanMove(IBoard board, int toRank, int toFile)
	{
		// Knight can go only to these directions.
		int[] xDir = [+2, +1, -1, -2, -2, -1, +1, +2];
		int[] yDir = [+1, +2, +2, +1, -1, -2, -2, -1];

		for (var i = 0; i < xDir.Length; ++i) {
			var isValid = this.CanMoveWithOneJump(
							 xDir[i], yDir[i], toRank, toFile, board);

			if (isValid)
				return true;
		}

		return false;
	}
}
