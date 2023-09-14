using Chess.Domain.Entities.Base;
using Chess.Domain.Services.Extensions;
using Chess.Domain.Utils;

namespace Chess.Application.Services;

public class GameRunnerService : IGameRunnerService
{
	private readonly ICaptureValidatorService _captureValidator;

	/// <summary>
	/// Starter player contains black pieces.
	/// </summary>
	public Color? TheColorInTurn { get; private set; } = null!;

	public Color? Winner { get; private set; } = null;
	public IList<IPiece> Captured { get; private set; } = new List<IPiece>();
	public IBoard Board { get; }
	public List<IPiece> WhitePiecesHistory { get; } = new();
	public List<IPiece> BlackPiecesHistory { get; } = new();

	public GameRunnerService()
	{
		Board = new Board.Board();
		_captureValidator = new CaptureValidatorService();
	}

	public void Start()
	{
		Winner = null;
		WhitePiecesHistory.Clear();
		BlackPiecesHistory.Clear();
		SetInitialPlayer();
	}

	/// <summary>
	/// Move the source piece into the target tile.
	/// If there is any oppenent, then it must be killed.
	/// </summary>
	/// <param name="fromRank"></param>
	/// <param name="fromFile"></param>
	/// <param name="toRank"></param>
	/// <param name="toFile"></param>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException"></exception>
	/// <exception cref="Exception"></exception>
	public async Task<PieceMovementInfoService?> MovePieceAsync(int fromRank, int fromFile, int toRank, int toFile)
	{
		try {
			if (TheColorInTurn == null || Winner is not null) {
				throw new InvalidOperationException();
			}

			var isCaptureValid = _captureValidator.CheckCaptureValidation(
				fromRank, fromFile, toRank, toFile, TheColorInTurn.Value, Board);

			if (isCaptureValid is false) {
				throw new InvalidOperationException("Invalid move");
			}

			var srcPiece = Board[fromRank, fromFile];
			var opponentKilledPiece = srcPiece.MoveAndKillOpponentIfPossible(
				Board, toRank, toFile);

			if (srcPiece?.Color is Color.White)
				WhitePiecesHistory.Add(srcPiece);
			else
				BlackPiecesHistory.Add(srcPiece!);

			if (opponentKilledPiece is not null) {
				Captured.Add(opponentKilledPiece);
			}

			var info = CheckIfWinnerDetectedAndTogglePlayer(opponentKilledPiece);
			info.PieceRef = srcPiece;

			return await Task.FromResult(info);
		}
		catch {
			return null;
		}
	}

	private void SetInitialPlayer() => TheColorInTurn = Color.Black;

	private PieceMovementInfoService CheckIfWinnerDetectedAndTogglePlayer(IPiece? opponentKilledPiece)
	{
		var info = new PieceMovementInfoService(
				IsPieceMoved: true,
				OpponentWhoHasBeenKilled: opponentKilledPiece);

		if (opponentKilledPiece?.Name?.ToLower().Contains("king") == true) {
			Winner = TheColorInTurn;

			// As the current player is the winner, it is not required
			// to change the player anymore.
			return info;
		}

		TheColorInTurn = TheColorInTurn == Color.White
					? Color.Black
					: Color.White;

		return info;
	}
}
