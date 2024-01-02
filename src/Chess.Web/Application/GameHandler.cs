using Chess.Web.Application.Services;
using Chess.Web.Domain;
using Chess.Web.Domain.Board;
using Chess.Web.Domain.Piece.Base;
using Chess.Web.Models;

namespace Chess.Web.Application;

public class GameHandler : IGameHandler
{
	public Color? TheColorInTurn { get; private set; } = null!;
	public Color? Winner { get; private set; } = null;
	public List<IPiece> WhitePiecesHistory { get; } = new();
	public List<IPiece> BlackPiecesHistory { get; } = new();

	public IList<IPiece> Captured { get; } = new List<IPiece>();
	public IBoard Board { get; }

	public GameHandler(IBoard board) => Board = board;

	public void Init()
	{
		Winner = null;
		WhitePiecesHistory.Clear();
		BlackPiecesHistory.Clear();
		SetInitialPlayer();
	}

	private void SetInitialPlayer() => TheColorInTurn = Color.White;

	/// <summary>
	/// Move the source piece into the target tile. If there is any oppenent, then 
	/// kill it.
	/// </summary>
	/// <returns>CurrentPieceMoveInfo obj.</returns>
	/// <exception cref="InvalidOperationException">
	/// When the color of current player is null, or the winner is detected, 
	/// or the user input is not valid.</exception>
	public async Task<CurrentPieceMoveInfo?> HandleAsync(
		int fromRank, int fromFile, int toRank, int toFile)
	{
		try {
			if (TheColorInTurn == null || Winner != null) {
				throw new InvalidOperationException();
			}
			if (InputValidator.Validate(
				fromRank, fromFile, toRank, toFile, TheColorInTurn.Value, Board) == false) {
				throw new InvalidOperationException("Invalid move");
			}
			Console.WriteLine("ok");
			var currentPiece = Board[fromRank, fromFile];
			if (currentPiece.CanMove(Board, toRank, toFile) is false)
				throw new InvalidOperationException("Invalid operation!");

			// Store the current piece's movement
			if (currentPiece?.Color is Color.White)
				WhitePiecesHistory.Add(currentPiece);
			else
				BlackPiecesHistory.Add(currentPiece!);

			// Kill if there is an opponent.
			var mayBeOpponentExists = Board[toRank, toFile];
			if (mayBeOpponentExists != null) {
				Board.KillPiece(toRank, toFile);
				Captured.Add(mayBeOpponentExists);
			}

			// Move the current piece
			Board.Move(currentPiece, toRank, toFile);

			DetectWinner(mayBeOpponentExists);
			TogglePlayer();

			// Returning data
			var infoToReturn = new CurrentPieceMoveInfo(
				IsPieceMoved: true,
				PieceRef: currentPiece,
				OpponentWhoHasBeenKilled: mayBeOpponentExists);

			return await Task.FromResult(infoToReturn);
		}
		catch {
			return null;
		}
	}

	private void DetectWinner(IPiece? opponentKilledPiece)
	{
		if (opponentKilledPiece?.Name?.ToLower().Contains("king") == true) {
			Winner = TheColorInTurn;
		}
	}

	private void TogglePlayer() =>
		TheColorInTurn = TheColorInTurn == Color.White ? Color.Black : Color.White;
}
