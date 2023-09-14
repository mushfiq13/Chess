using Chess.Domain.Entities.Base;
using Chess.Domain.Utils;

namespace Chess.Application.Services;

public interface IGameRunnerService
{
	IBoard Board { get; }
	Color? TheColorInTurn { get; }
	IList<IPiece> Captured { get; }
	Color? Winner { get; }
	List<IPiece> WhitePiecesHistory { get; }
	List<IPiece> BlackPiecesHistory { get; }

	Task<PieceMovementInfoService?> MovePieceAsync(
			int fromRank,
			int fromFile,
			int toRank,
			int toFile);

	void Start();
}