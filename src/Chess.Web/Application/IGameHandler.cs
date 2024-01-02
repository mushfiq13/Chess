using Chess.Web.Domain;
using Chess.Web.Domain.Board;
using Chess.Web.Domain.Piece.Base;
using Chess.Web.Models;

namespace Chess.Web.Application;

public interface IGameHandler
{
	List<IPiece> BlackPiecesHistory { get; }
	IBoard Board { get; }
	IList<IPiece> Captured { get; }
	Color? TheColorInTurn { get; }
	List<IPiece> WhitePiecesHistory { get; }
	Color? Winner { get; }

	/// <summary>
	/// Move the source piece into the target tile. If there is any oppenent, then 
	/// kill it.
	/// </summary>
	/// <returns>CurrentPieceMoveInfo obj.</returns>
	/// <exception cref="InvalidOperationException">
	/// When the color of current player is null, or the winner is detected, 
	/// or the user input is not valid.</exception>
	Task<CurrentPieceMoveInfo?> HandleAsync(int fromRank, int fromFile, int toRank, int toFile);
	void Init();
}