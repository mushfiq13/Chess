using Chess.Web.Domain.Board;

namespace Chess.Web.Domain.Piece.Base;

public abstract class BasePiece : IPiece
{
	public string Name { get; private set; }
	public Color Color { get; private set; }
	public int Rank { get; private set; }
	public int File { get; private set; }
	public bool IsAlive { get; private set; } = true;

	protected BasePiece(Color color, string name, int rank, int file)
	{
		Color = color;
		Name = name;
		Rank = rank;
		File = file;
	}

	public virtual void ChangeTile(int toRank, int toFile)
	{
		Rank = toRank;
		File = toFile;
	}

	public virtual void Kill()
	{
		IsAlive = false;
		Rank = -1;
		File = -1;
	}

	public abstract bool CanMove(IBoard board, int toRank, int toFile);
}
