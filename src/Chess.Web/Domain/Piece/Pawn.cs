using Chess.Web.Application.Services;
using Chess.Web.Domain.Board;
using Chess.Web.Domain.Piece.Base;

namespace Chess.Web.Domain.Piece;

internal class Pawn : BasePiece
{
	// In a 2D board, a Pawn can only move to upward or to downward.
	private bool _thisCanGoOnlyDownward;

	public Pawn(Color color, string name, int rank, int file)
		: base(color, name, rank, file)
	{
		if (Rank == 1)
			_thisCanGoOnlyDownward = true;
		else
			_thisCanGoOnlyDownward = false;
	}

	public override bool CanMove(IBoard board, int toRank, int toFile)
	{
		(var xDir, var yDir) = GetDirection();

		for (var i = 0; i < xDir.Count; ++i) {
			var isValid = this.CanMoveWithOneJump(
							 xDir[i], yDir[i], toRank, toFile, board);

			if (isValid)
				return true;
		}

		return false;
	}

	private (List<int>, List<int>) GetDirection()
	{
		int inWhichSide = _thisCanGoOnlyDownward ? +1 : -1;

		var xDir = new List<int> { 1 * inWhichSide, 1 * inWhichSide, 1 * inWhichSide };
		var yDir = new List<int> { 0, 1 * inWhichSide, 1 * inWhichSide };

		// Handling Special Case for intial position only
		if (_thisCanGoOnlyDownward && Rank == 1) {
			xDir.Add(+2);
			yDir.Add(+0);
		}
		if (_thisCanGoOnlyDownward == false && Rank == 6) {
			xDir.Add(-2);
			yDir.Add(+0);
		}

		return (xDir, yDir);
	}
}
