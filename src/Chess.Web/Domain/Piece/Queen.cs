using Chess.Web.Application.Services;
using Chess.Web.Domain.Board;
using Chess.Web.Domain.Piece.Base;

namespace Chess.Web.Domain.Piece;

internal class Queen : BasePiece
{
	public Queen(Color color, string name, int rank, int file)
		: base(color, name, rank, file) { }

	public override bool CanMove(IBoard board, int toRank, int toFile)
	{
		// Queen can go only to these directions.
		int[] XDir = [+1, +0, -1, +0, +1, +1, -1, -1];
		int[] YDir = [+0, +1, +0, -1, +1, -1, +1, -1];

		for (var i = 0; i < XDir.Length; ++i) {
			var isValid = this.CanMoveWithMultipleJump(
							 XDir[i], YDir[i], toRank, toFile, board);

			if (isValid)
				return true;
		}

		return false;
	}
}
