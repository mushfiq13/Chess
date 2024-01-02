using Chess.Web.Domain.Board;
using Chess.Web.Domain.Piece.Base;

namespace Chess.Web.Application.Services;

public static class PieceMoveValidatorExtensions
{
	public static bool CanMoveWithMultipleJump(
		this IPiece source,
		int xDir,
		int yDir,
		int toRank,
		int toFile,
		IBoard board)
	{
		var curRank = source.Rank;
		var curFile = source.File;

		while (true) {
			var nextRank = curRank + xDir;
			var nextFile = curFile + yDir;

			if (board.TileExists(nextRank, nextFile) == false
				|| source?.Color == board[nextRank, nextFile]?.Color)
				return false;

			if (source?.Color != board[nextRank, nextFile]?.Color
				&& (nextRank, nextFile) == (toRank, toFile))
				return true;

			curRank = nextRank;
			curFile = nextFile;
		}
	}

	public static bool CanMoveWithOneJump(
		this IPiece source,
		int xDir,
		int yDir,
		int toRank,
		int toFile,
		IBoard board)
	{
		var nextRank = source.Rank + xDir;
		var nextFile = source.File + yDir;

		if (board.TileExists(nextRank, nextFile)
			&& (nextRank, nextFile) == (toRank, toFile)
			&& source?.Color != board[nextRank, nextFile]?.Color)
			return true;

		return false;
	}
}
