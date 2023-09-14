using Chess.Domain.Entities.Base;

namespace Chess.Domain.Services.Extensions;

public static class PieceMovingServiceExtension
{
	/// <summary>
	/// Try to move the source piece into the target.
	/// Kill if there is an opponent.
	/// </summary>
	/// <param name="board"></param>
	/// <param name="sourceChess"></param>
	/// <param name="newRank"></param>
	/// <param name="newFile"></param>
	/// <returns>Opponent piece if it killed.</returns>
	/// <exception cref="InvalidOperationException"></exception>
	public static IPiece MoveAndKillOpponentIfPossible(
		this IPiece sourceChess,
		IBoard board,
		int toRank,
		int toFile)
	{
		if (sourceChess is null)
			return null!;

		if (sourceChess.CanMove(board, toRank, toFile) is false)
			throw new InvalidOperationException("Invalid operation!");

		var opponent = board[toRank, toFile];

		if (opponent is not null) {
			// killing opponent
			board.Remove(opponent.Rank, opponent.File);
			opponent.Kill();
		}

		// Move source chess
		board.Remove(sourceChess.Rank, sourceChess.File);
		board.Put(sourceChess, toRank, toFile);
		sourceChess.Put(toRank, toFile);

		return opponent!;
	}
}
