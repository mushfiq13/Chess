using Chess.Web.Domain.Board;

namespace Chess.Web.Domain.Piece.Base;

public interface IPiece
{
	string Name { get; }
	Color Color { get; }
	int Rank { get; }
	int File { get; }

	bool CanMove(IBoard board, int toRank, int toFile);
	void Kill();
	void ChangeTile(int toRank, int toFile);
}