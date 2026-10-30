using Chess.Web.Domain;
using Chess.Web.Domain.Board;
using Chess.Web.Domain.Piece.Base;

namespace Chess.Web.Models.Ui;

/// <summary>
/// Frontend-only helpers for working with board coordinates.
/// </summary>
/// <remarks>
/// The domain layer exposes no algebraic-notation helper and no piece-type
/// discriminator, so the presentation layer derives both from the piece's
/// <see cref="IPiece.Name"/> string. Nothing here mutates domain state.
/// </remarks>
public static class BoardCoordinate
{
	/// <summary>
	/// Number of ranks (rows) on the board.
	/// </summary>
	public const int Ranks = 8;

	/// <summary>
	/// Number of files (columns) on the board.
	/// </summary>
	public const int Files = 8;

	/// <summary>
	/// True when the square is a light square. Square parity is derived from
	/// the coordinates so the renderer never has to mutate shared state.
	/// </summary>
	public static bool IsLight(int rank, int file) => (rank + file) % 2 == 0;

	/// <summary>
	/// Converts a board coordinate into algebraic notation (for example "e4").
	/// </summary>
	/// <remarks>
	/// Rank 0 is Black's back rank and is drawn at the top of the board, so the
	/// displayed rank number is <c>8 - rank</c>.
	/// </remarks>
	public static string ToAlgebraic(int rank, int file)
	{
		if (rank < 0 || rank >= Ranks || file < 0 || file >= Files)
			return "-";

		var fileLetter = (char)('a' + file);
		var rankNumber = Ranks - rank;

		return $"{fileLetter}{rankNumber}";
	}

	/// <summary>
	/// Returns the file letter for the given file index ("a" through "h").
	/// </summary>
	public static string FileLetter(int file)
		=> file < 0 || file >= Files ? "-" : ((char)('a' + file)).ToString();

	/// <summary>
	/// Returns the displayed rank number for the given rank index (8 down to 1).
	/// </summary>
	public static int RankNumber(int rank)
		=> rank < 0 || rank >= Ranks ? 0 : Ranks - rank;

	/// <summary>
	/// Safely reads a piece from the board. The board indexer throws when the
	/// coordinates are out of range, so bounds are checked first.
	/// </summary>
	public static IPiece? PieceAt(IBoard board, int rank, int file)
	{
		if (board is null || !board.TileExists(rank, file))
			return null;

		return board[rank, file];
	}

	/// <summary>
	/// Extracts the piece kind ("king", "queen", ...) from a piece name such as
	/// "white-knight". Returns an empty string when the name is not recognised.
	/// </summary>
	public static string KindOf(IPiece? piece) => KindOf(piece?.Name);

	/// <summary>
	/// Extracts the piece kind ("king", "queen", ...) from a piece name.
	/// </summary>
	public static string KindOf(string? pieceName)
	{
		if (string.IsNullOrWhiteSpace(pieceName))
			return string.Empty;

		var separator = pieceName.LastIndexOf('-');

		return separator >= 0 && separator < pieceName.Length - 1
			? pieceName[(separator + 1)..].ToLowerInvariant()
			: pieceName.ToLowerInvariant();
	}

	/// <summary>
	/// Extracts the colour prefix ("white" or "black") from a piece name.
	/// </summary>
	public static string ColorOf(string? pieceName)
	{
		if (string.IsNullOrWhiteSpace(pieceName))
			return string.Empty;

		var separator = pieceName.IndexOf('-');

		return separator > 0
			? pieceName[..separator].ToLowerInvariant()
			: string.Empty;
	}

	/// <summary>
	/// True when the piece belongs to the given colour.
	/// </summary>
	public static bool IsOfColor(IPiece? piece, Color color)
		=> piece is not null && piece.Color == color;

	/// <summary>
	/// Finds the square occupied by the given colour's king, if it is still on
	/// the board. Used to detect check.
	/// </summary>
	public static (int Rank, int File)? FindKing(IBoard board, Color color)
	{
		if (board is null)
			return null;

		for (var rank = 0; rank < board.Ranks; rank++)
		{
			for (var file = 0; file < board.Files; file++)
			{
				var piece = PieceAt(board, rank, file);

				if (piece is null)
					continue;

				if (piece.Color == color && KindOf(piece).Equals("king", StringComparison.OrdinalIgnoreCase))
					return (rank, file);
			}
		}

		return null;
	}
}
