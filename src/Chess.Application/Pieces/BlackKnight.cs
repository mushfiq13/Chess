using Chess.Domain;
using Chess.Domain.Entities.Base;
using Chess.Domain.Services.Extensions;
using Chess.Domain.Utils;

namespace Chess.Application.Pieces;

internal class BlackKnight : Piece
{
	public static IPiece Knight1 = new BlackKnight(1);
	public static IPiece Knight2 = new BlackKnight(6);

	public override string Name { get; }

	private BlackKnight(int file)
			: base(7, file, Color.Black)
	{
		Name = "black-knight";
	}

	public override bool CanMove(IBoard board, int toRank, int toFile)
	{
		// Knight can go only to these directions.
		var xDir = new[] { +2, +1, -1, -2, -2, -1, +1, +2 };
		var yDir = new[] { +1, +2, +2, +1, -1, -2, -2, -1 };

		for (var i = 0; i < xDir.Length; ++i) {
			var isValid = this.CanMoveToDestinition(
				 xDir[i], yDir[i], toRank, toFile, board);

			if (isValid)
				return true;
		}

		return false;
	}
}

