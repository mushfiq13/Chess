namespace Chess.Domain.Entities.Base;

public interface IBoard
{
	IPiece this[int rank, int file] { get; }

	IPiece[,] GetPieces();

	void Put(IPiece src, int destTile, int toFile);
	void Remove(int rank, int file);
	void Reset();
}
