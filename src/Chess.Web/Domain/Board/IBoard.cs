using Chess.Web.Domain.Piece.Base;

namespace Chess.Web.Domain.Board;

public interface IBoard
{
	IPiece this[int rank, int file] { get; }

	IPiece[,] Tiles { get; }
	int Ranks { get; }
	int Files { get; }

	void KillPiece(int rank, int file);
	void Move(IPiece piece, int toRank, int toFile);
	void Reset();
	bool TileExists(int rank, int file);
}
