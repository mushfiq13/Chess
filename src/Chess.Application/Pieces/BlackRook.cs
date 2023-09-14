using Chess.Domain;
using Chess.Domain.Entities.Base;
using Chess.Domain.Services.Extensions;
using Chess.Domain.Utils;

namespace Chess.Application.Pieces;

internal class BlackRook : Piece
{
	public static IPiece Rook1 = new BlackRook(0);
	public static IPiece Rook2 = new BlackRook(7);

	public override string Name { get; }

	private BlackRook(int file)
			: base(7, file, Color.Black)
	{
		Name = "black-rook";
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
