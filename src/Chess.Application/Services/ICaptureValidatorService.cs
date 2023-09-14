using Chess.Domain.Entities.Base;
using Chess.Domain.Utils;

namespace Chess.Application.Services;

public interface ICaptureValidatorService
{
	bool CheckCaptureValidation(
					int fromRank,
					int fromFile,
					int toRank,
					int toFile,
					Color TheColorInTurn,
					IBoard Board);
}