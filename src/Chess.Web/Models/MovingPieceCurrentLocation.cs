using Chess.Web.Domain.Board;

namespace Chess.Web.Models;

public class MovingPieceCurrentLocation
{
	public int FromRank { get; set; } = -1;
	public int FromFile { get; set; } = -1;
	public int ToRank { get; set; } = -1;
	public int ToFile { get; set; } = -1;

	/// <summary>
	/// Set source only if the given rank and file
	/// holds a valid piece.
	/// </summary>
	/// <param name="rank"></param>
	/// <param name="file"></param>
	public void SetSourceLocation(IBoard board, int rank, int file)
	{
		if (board.TileExists(rank, file)
			&& board[rank, file] is null)
			return;

		FromRank = rank;
		FromFile = file;
	}

	public void SetDestinition(int rank, int file)
	{
		ToRank = rank;
		ToFile = file;
	}

	public void ClearLocation()
	{
		// Clear source and target
		FromRank = -1;
		FromFile = -1;
		ToRank = -1;
		ToFile = -1;
	}
}
