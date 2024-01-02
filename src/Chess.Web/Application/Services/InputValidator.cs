using Chess.Web.Domain;
using Chess.Web.Domain.Board;

namespace Chess.Web.Application.Services;

public static class InputValidator
{
	public static bool Validate(
		int fromRank,
		int fromFile,
		int toRank,
		int toFile,
		Color TheColorInTurn,
		IBoard board)
	{
		if (board.TileExists(fromRank, fromFile) == false) {
			throw new InvalidDataException();
		}
		if (TheColorInTurn != board[fromRank, fromFile]?.Color) {
			throw new InvalidDataException();
		}
		if (board.TileExists(toRank, toFile) == false) {
			throw new InvalidDataException();
		}
		if (TheColorInTurn == board[toRank, toFile]?.Color) {
			throw new InvalidDataException();
		}

		return board[fromRank, fromFile].CanMove(
			board, toRank, toFile);
	}
}
