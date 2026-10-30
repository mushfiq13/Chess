using Chess.Web.Domain;
using Chess.Web.Domain.Piece.Base;

namespace Chess.Web.Models.Ui;

/// <summary>
/// A move as observed by the frontend.
/// </summary>
/// <param name="Piece">The piece that moved.</param>
/// <param name="FromRank">Origin rank.</param>
/// <param name="FromFile">Origin file.</param>
/// <param name="ToRank">Destination rank.</param>
/// <param name="ToFile">Destination file.</param>
/// <param name="IsCapture">True when the move captured an enemy piece.</param>
public readonly record struct ObservedMove(
	IPiece? Piece,
	int FromRank,
	int FromFile,
	int ToRank,
	int ToFile,
	bool IsCapture);

/// <summary>
/// A frontend-side feed of moves.
/// </summary>
/// <remarks>
/// The domain's own history records only destination squares and drops the
/// very first white move, because its dictionary is keyed on the black move
/// counter. The presentation layer therefore observes moves as they are
/// played, which gives it both the origin and the capture flag, and keeps the
/// move list complete without changing any domain code.
/// </remarks>
public class MoveFeed
{
	/// <summary>
	/// Raised whenever a move is observed.
	/// </summary>
	public event Action? OnMove;

	/// <summary>
	/// The moves observed so far, in order.
	/// </summary>
	public List<ObservedMove> Moves { get; } = new();

	/// <summary>
	/// Records a move and notifies listeners.
	/// </summary>
	public void Add(ObservedMove move)
	{
		Moves.Add(move);
		OnMove?.Invoke();
	}

	/// <summary>
	/// Removes every observed move.
	/// </summary>
	public void Clear() => Moves.Clear();
}
