using Chess.Web.Domain.Piece.Base;

namespace Chess.Web.Models;

public record struct CurrentPieceMoveInfo(
		bool IsPieceMoved = false,
		IPiece? PieceRef = null,
		IPiece? OpponentWhoHasBeenKilled = null);
