using Chess.Domain.Entities.Base;
using Chess.Domain.Utils;

namespace Chess.Domain;

public abstract class Piece : IPiece
{
	public abstract string Name { get; }
	public int Rank { get; private set; }
	public int File { get; private set; }
	public Color Color { get; private set; }

	protected Piece(int rank, int file, Color color)
	{
		Rank = rank;
		File = file;
		Color = color;
	}

	public virtual void Put(int toRank, int toFile)
	{
		Rank = toRank;
		File = toFile;
	}

	public virtual void Kill()
	{
		Rank = -1;
		File = -1;
	}

	public abstract bool CanMove(IBoard board, int toRank, int toFile);
}
