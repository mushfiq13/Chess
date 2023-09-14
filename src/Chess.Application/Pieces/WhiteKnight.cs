using Chess.Domain;
using Chess.Domain.Entities.Base;
using Chess.Domain.Services.Extensions;
using Chess.Domain.Utils;

namespace Chess.Application.Pieces;

internal class WhiteKnight : Piece
{
	public static IPiece Knight1 = new WhiteKnight(1);
	public static IPiece Knight2 = new WhiteKnight(6);

	public override string Name { get; }

	private WhiteKnight(int file)
			: base(0, file, Color.White)
	{
		Name = "white-knight";
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
