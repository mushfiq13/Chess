using Chess.Web.Domain.Piece.Base;

namespace Chess.Web.Domain.Board;

public class Board : IBoard
{
	/// <summary>
	/// Board has NxN tiles, and those tiles can hold a single
	/// piece only.
	/// </summary>
	public IPiece[,] Tiles { get; }

	/// <summary>
	/// Number of rows.
	/// </summary>
	public int Ranks { get; }

	/// <summary>
	/// Number of columns.
	/// </summary>
	public int Files { get; }

	public Board(IPiece[,] tiles)
	{
		Tiles = tiles;
		Ranks = tiles.GetLength(0);
		Files = tiles.Length / Ranks;
	}

	public IPiece this[int rank, int file] => Tiles[rank, file] ?? default!;

	/// <summary>
	/// 1. Remove the piece from its current location.
	/// 2. Place that piece to the new location.	
	/// 3. Change the tile of the piece.
	/// </summary>
	/// <exception cref="InvalidDataException">When piece or the board is null.</exception>
	/// <exception cref="InvalidOperationException">When the target location is alreay occupied by another piece.</exception>
	public void Move(IPiece piece, int toRank, int toFile)
	{
		if (piece == null || Tiles == null)
			throw new InvalidDataException("Selected piece is null!");
		if (piece?.Color == Tiles[toRank, toFile]?.Color)
			throw new InvalidOperationException(
				$"{toRank}x{toFile} is already occupied by another piece");

		// Remove the piece from its current location.
		Tiles[piece.Rank, piece.File] = null!;
		// Place that piece to the new location.
		Tiles[toRank, toFile] = piece!;
		// Change the tile of the piece.
		piece.ChangeTile(toRank, toFile);
	}

	/// <summary>
	/// 1. Kill the piece in the given rank and file.
	/// 2. As the piece is dead, that location should be null.S
	/// </summary>
	/// <param name="rank"></param>
	/// <param name="file"></param>
	public void KillPiece(int rank, int file)
	{
		Tiles[rank, file].Kill();
		// Make null the location due to piece is dead.
		Tiles[rank, file] = null!;
	}

	/// <summary>
	/// Empty the tiles.
	/// (TODO, not yet implemented) Assign the pieces.
	/// </summary>
	public void Reset()
	{
		for (var i = 0; i < Ranks; i++)
			for (var j = 0; j < Files; ++j)
				Tiles[i, j] = null!;
	}

	/// <summary>
	/// Check the tile boundary validation.
	/// </summary>
	public bool TileExists(int rank, int file)
		=> rank > -1 && rank < Ranks
				&& file > -1 && file < Files;
}
