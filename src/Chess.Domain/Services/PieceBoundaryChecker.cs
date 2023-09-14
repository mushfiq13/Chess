using Chess.Domain.Utils;

namespace Chess.Domain.Services;

public static class PieceBoundaryChecker
{
	public static bool Inbounds(int rank, int file)
		=> rank > -1 && rank < Constants.RANKS
			&& file > -1 && file < Constants.FILES;
}
