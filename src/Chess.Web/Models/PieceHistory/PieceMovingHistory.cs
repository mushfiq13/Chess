using Chess.Domain.Entities.Base;

namespace Chess.Web.Models.PieceHistory;

public class PieceMovingHistory
{
	/// <summary>
	/// Store total rows that has been appended.
	/// </summary>
	public int TotalRowsUntilNow { get; private set; } = 0;

	/// <summary>
	/// Store pieces history where int points to a row.
	/// 
	/// Item1: holds black piece's history
	/// Item2: holds white piece's history
	/// </summary>
	public Dictionary<int, (PieceSingleMoveContainer, PieceSingleMoveContainer)> History { get; private set; } = new();

	public void AddNew(IPiece piece, int ToRank, int ToFile)
	{
		var newHistory = new PieceSingleMoveContainer
		{
			Name = piece?.Name!,
			ToRank = ToRank,
			ToFile = ToFile
		};

		if (piece?.Color == Domain.Utils.Color.Black) {
			++TotalRowsUntilNow;
			History.Add(TotalRowsUntilNow, (newHistory, null!));
		}
		else if (piece?.Color == Domain.Utils.Color.White) {
			History[TotalRowsUntilNow] = (History[TotalRowsUntilNow].Item1, newHistory!);
		}
	}
}
