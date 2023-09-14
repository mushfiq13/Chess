using Chess.Application.Services;
using Chess.Web.Models.PieceHistory;

namespace Chess.Web.Models;

public class AppState
{
	public string BgColor { get; set; } = "light-bg";

	public MovingPieceCurrentLocation Location { get; private set; } = new();
	public PieceMovingHistory PieceMovingHistory { get; set; } = new();
	public IGameRunnerService GameService { get; }

	public event Action OnChange = () => { };

	public AppState(IGameRunnerService gameRunnerService)
	{
		GameService = gameRunnerService;
	}

	public async Task MovePiece()
	{
		var curInfo = await GameService.MovePieceAsync(
			Location.FromRank,
			Location.FromFile,
			Location.ToRank,
			Location.ToFile);

		if (curInfo?.IsPieceMoved is true) {
			PieceMovingHistory.AddNew(
				curInfo?.PieceRef!,
				Location.ToRank,
				Location.ToFile);
			OnChange();
		}

		Location.ClearLocation();
	}

	public void ToggleBgColor()
	{
		BgColor = BgColor == "dark-bg" ? "light-bg" : "dark-bg";

		NotifyStateChanged();
	}

	public string HightlightSourcePiece()
	{
		return BgColor == "light-bg"
			? "lightwood-highlight"
			: "darkwood-highlight";
	}

	private void NotifyStateChanged() => OnChange?.Invoke();
}
