using Chess.Web.Application;
using Chess.Web.Models.PieceHistory;

namespace Chess.Web.Models;

public class AppState
{
	public string BgColor { get; set; } = "light-bg";

	public MovingPieceCurrentLocation Location { get; private set; } = new();
	public PieceMovingHistory PieceMovingHistory { get; set; } = new();
	public IGameHandler GameHandler { get; }

	public event Action OnChange = () => { };

	public AppState(IGameHandler gameRunnerService)
	{
		GameHandler = gameRunnerService;
	}

	public async Task MovePiece()
	{
		var curInfo = await GameHandler.HandleAsync(
			Location.FromRank,
			Location.FromFile,
			Location.ToRank,
			Location.ToFile);

		if (curInfo?.IsPieceMoved == true) {
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
