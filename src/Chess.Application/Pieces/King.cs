using Chess.Domain;
using Chess.Domain.Entities.Base;
using Chess.Domain.Services.Extensions;
using Chess.Domain.Utils;

namespace Chess.Application.Pieces;

internal class King : Piece
{
	public static IPiece White = new King(0, 4, Color.White, "white-king");
	public static IPiece Black = new King(7, 4, Color.Black, "black-king");

	public override string Name { get; }

	private King(int rank, int file, Color color, string name)
			: base(rank, file, color)
	{
		Name = name;
	}

	public override bool CanMove(IBoard board, int toRank, int toFile)
	{
		// King can go only to these directions.
		var xDir = new[] { +1, +1, +0, -1, -1, -1, +0, +1 };
		var yDir = new[] { +0, +1, +1, +1, +0, -1, -1, -1 };

		for (var i = 0; i < xDir.Length; ++i) {
			var isValid = this.CanMoveToDestinition(
				 xDir[i], yDir[i], toRank, toFile, board);

			if (isValid)
				return true;
		}

		return false;
	}
}
