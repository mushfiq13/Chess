using Chess.Domain.Utils;

namespace Chess.Domain.Entities.Base;

public interface IPiece
{
	string Name { get; }
	int Rank { get; }
	int File { get; }
	Color Color { get; }

	void Kill();

	void Put(int toRank, int toFile);
	bool CanMove(IBoard board, int toRank, int toFile);
}