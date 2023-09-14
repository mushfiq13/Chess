using Chess.Domain.Entities.Base;

namespace Chess.Domain.Services.Extensions;

public static class PeiceMovingValidatorExtension
{
	public static bool CanMoveToDestinition(
			this IPiece source,
			int xDir,
			int yDir,
			int toRank,
			int toFile,
			IBoard board,
			int sourceCanJumpUpto = 1)
	{
		var curRank = source.Rank;
		var curFile = source.File;

		while (PieceBoundaryChecker.Inbounds(curRank, curFile)) {
			var nextRank = curRank + xDir;
			var nextFile = curFile + yDir;

			if (sourceCanJumpUpto <= 0
				|| PieceBoundaryChecker.Inbounds(nextRank, nextFile) == false
				|| source?.Color == board[nextRank, nextFile]?.Color)
				return false;

			if (source?.Color != board[nextRank, nextFile]?.Color
							&& (nextRank, nextFile) == (toRank, toFile))
				return true;

			curRank = nextRank;
			curFile = nextFile;
			--sourceCanJumpUpto;
		}

		return false;
	}
}
