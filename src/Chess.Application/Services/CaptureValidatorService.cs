using Chess.Domain.Entities.Base;
using Chess.Domain.Services;
using Chess.Domain.Utils;

namespace Chess.Application.Services;

public class CaptureValidatorService : ICaptureValidatorService
{
	public bool CheckCaptureValidation(
		int fromRank,
		int fromFile,
		int toRank,
		int toFile,
		Color TheColorInTurn,
		IBoard Board)
	{
		var sourceTileValid = ValidateTile(
																		fromRank,
																		fromFile,
																		(rank, file) => TheColorInTurn == Board[fromRank, fromFile]?.Color);

		var targetTileValid = ValidateTile(
																		toRank,
																		toFile,
																		(rank, file) => TheColorInTurn != Board[rank, file]?.Color);

		if (sourceTileValid && targetTileValid) {
			return Board[fromRank, fromFile].CanMove(
																			Board, toRank, toFile);
		}

		return false;
	}

	private bool ValidateTile(int rank, int file, Func<int, int, bool>? validator)
	{
		if (PieceBoundaryChecker.Inbounds(rank, file) is false)
			throw new InvalidDataException();

		if (validator is not null && validator(rank, file) is false)
			throw new InvalidDataException();

		return true;
	}
}
