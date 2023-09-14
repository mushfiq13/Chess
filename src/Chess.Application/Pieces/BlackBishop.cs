using Chess.Domain;
using Chess.Domain.Entities.Base;
using Chess.Domain.Services.Extensions;
using Chess.Domain.Utils;

namespace Chess.Application.Pieces;

internal class BlackBishop : Piece
{
	public static IPiece Bishop1 = new BlackBishop(2);
	public static IPiece Bishop2 = new BlackBishop(5);

	public override string Name { get; }

	private BlackBishop(int file)
			: base(7, file, Color.Black)
	{
		Name = "black-bishop";
	}

	public override bool CanMove(IBoard board, int toRank, int toFile)
	{
		// Bishop can go only to these directoins.
		var xDir = new[] { +1, +1, -1, -1 };
		var yDir = new[] { +1, -1, +1, -1 };

		for (var i = 0; i < xDir.Length; ++i) {
			var isValid = this.CanMoveToDestinition(
				 xDir[i], yDir[i], toRank, toFile, board, Constants.RANKS);

			if (isValid)
				return true;
		}

		return false;
	}
}