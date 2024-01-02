using Chess.Web;
using Chess.Web.Application;
using Chess.Web.Domain;
using Chess.Web.Domain.Board;
using Chess.Web.Domain.Piece;
using Chess.Web.Domain.Piece.Base;
using Chess.Web.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
	BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
builder.Services.AddScoped<AppState>();
builder.Services.AddScoped<IGameHandler, GameHandler>();
builder.Services.AddScoped<IBoard, Board>(_ =>
{
	var tiles = new IPiece[8, 8];

	tiles[0, 4] = new King(Color.Black, "black-king", 0, 4);
	tiles[7, 4] = new King(Color.White, "white-king", 7, 4);

	tiles[0, 3] = new Queen(Color.Black, "black-queen", 0, 3);
	tiles[7, 3] = new Queen(Color.White, "white-queen", 7, 3);

	tiles[0, 2] = new Bishop(Color.Black, "black-bishop", 0, 2);
	tiles[0, 5] = new Bishop(Color.Black, "black-bishop", 0, 5);
	tiles[7, 2] = new Bishop(Color.White, "white-bishop", 7, 2);
	tiles[7, 5] = new Bishop(Color.White, "white-bishop", 7, 5);

	tiles[0, 1] = new Knight(Color.Black, "black-knight", 0, 1);
	tiles[0, 6] = new Knight(Color.Black, "black-knight", 0, 6);
	tiles[7, 1] = new Knight(Color.White, "white-knight", 7, 1);
	tiles[7, 6] = new Knight(Color.White, "white-knight", 7, 6);

	tiles[0, 0] = new Rook(Color.Black, "black-rook", 0, 0);
	tiles[0, 7] = new Rook(Color.Black, "black-rook", 0, 7);
	tiles[7, 0] = new Rook(Color.White, "white-rook", 7, 0);
	tiles[7, 7] = new Rook(Color.White, "white-rook", 7, 7);

	for (var i = 0; i < 8; ++i) {
		tiles[1, i] = new Pawn(Color.Black, "black-pawn", 1, i);
		tiles[6, i] = new Pawn(Color.White, "white-pawn", 6, i);
	}

	return new Board(tiles);
});

await builder.Build().RunAsync();
