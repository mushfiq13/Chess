using Chess.Domain.Entities.Base;

namespace Chess.Application.Services;

public record struct PieceMovementInfoService(
		bool IsPieceMoved = false,
		IPiece? PieceRef = null,
		IPiece? OpponentWhoHasBeenKilled = null);
