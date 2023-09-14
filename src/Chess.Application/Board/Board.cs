using Chess.Application.Pieces;
using Chess.Domain.Entities.Base;
using Chess.Domain.Utils;

namespace Chess.Application.Board;

internal class Board : IBoard
{
	private IPiece[,] _tiles;

	public Board()
	{
		_tiles = new IPiece[Constants.RANKS, Constants.FILES];

		_tiles[King.White.Rank, King.White.File] = King.White;
		_tiles[King.Black.Rank, King.Black.File] = King.Black;

		_tiles[Queen.White.Rank, Queen.White.File] = Queen.White;
		_tiles[Queen.Black.Rank, Queen.Black.File] = Queen.Black;

		_tiles[WhiteBishop.Bishop1.Rank, WhiteBishop.Bishop1.File] = WhiteBishop.Bishop1;
		_tiles[WhiteBishop.Bishop2.Rank, WhiteBishop.Bishop2.File] = WhiteBishop.Bishop2;

		_tiles[BlackBishop.Bishop1.Rank, BlackBishop.Bishop1.File] = BlackBishop.Bishop1;
		_tiles[BlackBishop.Bishop2.Rank, BlackBishop.Bishop2.File] = BlackBishop.Bishop2;

		_tiles[WhiteKnight.Knight1.Rank, WhiteKnight.Knight1.File] = WhiteKnight.Knight1;
		_tiles[WhiteKnight.Knight2.Rank, WhiteKnight.Knight2.File] = WhiteKnight.Knight2;

		_tiles[BlackKnight.Knight1.Rank, BlackKnight.Knight1.File] = BlackKnight.Knight1;
		_tiles[BlackKnight.Knight2.Rank, BlackKnight.Knight2.File] = BlackKnight.Knight2;

		_tiles[WhiteRook.Rook1.Rank, WhiteRook.Rook1.File] = WhiteRook.Rook1;
		_tiles[WhiteRook.Rook2.Rank, WhiteRook.Rook2.File] = WhiteRook.Rook2;

		_tiles[BlackRook.Rook1.Rank, BlackRook.Rook1.File] = BlackRook.Rook1;
		_tiles[BlackRook.Rook2.Rank, BlackRook.Rook2.File] = BlackRook.Rook2;

		for (var i = 0; i < Constants.FILES; ++i) {
			var pawn = WhitePawn.CreateInstance(i);
			_tiles[pawn.Rank, pawn.File] = pawn;
		}

		for (var i = 0; i < Constants.FILES; ++i) {
			var pawn = BlackPawn.CreateInstance(i);
			_tiles[pawn.Rank, pawn.File] = pawn;
		}
	}

	public IPiece[,] GetPieces()
	{
		return _tiles;
	}

	public IPiece this[int rank, int file]
	{
		get
		{
			try {
				return _tiles[rank, file];
			}
			catch {
				throw new IndexOutOfRangeException();
			}
		}
		set
		{
			Put(value, rank, file);
		}
	}

	public void Put(IPiece src, int destTile, int toFile)
	{
		if (src == null)
			throw new InvalidDataException("Not possible to put the source piece into the target");

		if (src?.Color == _tiles[destTile, toFile]?.Color)
			throw new InvalidOperationException(
							$"New location is [{destTile}, {toFile}]" +
							$"which is already occupied by same type of chess.");

		_tiles[destTile, toFile] = src!;
	}

	public void Remove(int rank, int file) => _tiles[rank, file] = default!;

	public void Reset()
					=> _tiles = new IPiece[Constants.RANKS, Constants.FILES];
}
