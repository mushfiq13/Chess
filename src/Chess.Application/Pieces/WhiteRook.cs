using Chess.Domain;
using Chess.Domain.Entities.Base;
using Chess.Domain.Services.Extensions;
using Chess.Domain.Utils;

namespace Chess.Application.Pieces;

internal class WhiteRook : Piece
{
	public static IPiece Rook1 = new WhiteRook(0);
	public static IPiece Rook2 = new WhiteRook(7);

	public override string Name { get; }

	private WhiteRook(int file)
			: base(0, file, Color.White)
	{
		Name = "white-rook";
	}

	public override bool CanMove(IBoard board, int toRank, int toFile)
	{
		var xDir = new[] { +1, +0, -1, +0 };
		var yDir = new[] { +0, +1, +0, -1 };

		for (var i = 0; i < xDir.Length; ++i) {
			var isValid = this.CanMoveToDestinition(
				 xDir[i], yDir[i], toRank, toFile, board, Constants.RANKS);

			if (isValid)
				return true;
		}

		return false;
	}
}

