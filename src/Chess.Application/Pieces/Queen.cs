using Chess.Domain;
using Chess.Domain.Entities.Base;
using Chess.Domain.Services.Extensions;
using Chess.Domain.Utils;

namespace Chess.Application.Pieces;

internal class Queen : Piece
{
	// Queen can go only to these directions.
	private int[] XDir = new[] { +1, +0, -1, +0, +1, +1, -1, -1 };
	private int[] YDir = new[] { +0, +1, +0, -1, +1, -1, +1, -1 };

	public static IPiece White = new Queen(0, 3, Color.White, "white-queen");
	public static IPiece Black = new Queen(7, 3, Color.Black, "black-queen");

	public override string Name { get; }

	private Queen(int rank, int file, Color color, string name)
			: base(rank, file, color)
	{
		Name = name;
	}

	public override bool CanMove(IBoard board, int toRank, int toFile)
	{
		for (var i = 0; i < XDir.Length; ++i) {
			var isValid = this.CanMoveToDestinition(
				 XDir[i], YDir[i], toRank, toFile, board, Constants.RANKS);

			if (isValid)
				return true;
		}

		return false;
	}
}
