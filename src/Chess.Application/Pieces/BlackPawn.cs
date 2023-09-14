using Chess.Domain;
using Chess.Domain.Entities.Base;
using Chess.Domain.Services.Extensions;
using Chess.Domain.Utils;

namespace Chess.Application.Pieces;

internal class BlackPawn : Piece
{
	public static IPiece CreateInstance(int file) => new BlackPawn(file);

	public override string Name { get; }

	private BlackPawn(int file)
			: base(6, file, Color.Black)
	{
		Name = "black-pawn";
	}

	public override bool CanMove(IBoard board, int toRank, int toFile)
	{
		(int[] xDir, int[] yDir) = GetDirection();

		for (var i = 0; i < xDir.Length; ++i) {
			var isValid = this.CanMoveToDestinition(
				 xDir[i], yDir[i], toRank, toFile, board);

			if (isValid)
				return true;
		}

		return false;
	}

	private (int[], int[]) GetDirection()
	{
		// Black Pawn can go only to these directions.
		IList<int> x = new List<int> { -1, -1, -1 };
		IList<int> y = new List<int> { +0, -1, +1 };

		if (Rank == 6) // initial rank
		{
			x.Add(-2);
			y.Add(+0);
		}

		return (x.ToArray(), y.ToArray());
	}
}
