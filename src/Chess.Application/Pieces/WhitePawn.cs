using Chess.Domain;
using Chess.Domain.Entities.Base;
using Chess.Domain.Services.Extensions;
using Chess.Domain.Utils;

namespace Chess.Application.Pieces;

internal class WhitePawn : Piece
{
	public static IPiece CreateInstance(int file) => new WhitePawn(file);
	public override string Name { get; }

	private WhitePawn(int file)
			: base(1, file, Color.White)
	{
		Name = "white-pawn";
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
		// White Pawn can go only to these directions.
		IList<int> x = new List<int> { +1, +1, +1 };
		IList<int> y = new List<int> { +0, +1, -1 };

		if (Rank == 1) // at initial rank
		{
			x.Add(+2);
			y.Add(+0);
		}

		return (x.ToArray(), y.ToArray());
	}
}
