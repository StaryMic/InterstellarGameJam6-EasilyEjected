using Godot;

namespace IsolationInterstellarGameJam.Mono.HUD;

public partial class HeadsUpDisplay : Control
{
	// External Node References
	private Player.PlayerCharacter _player;
	private GlobalScripts.GameStateSignals _gameStateSignals;
	private GameStateTracker _gameState;
	
	// Internal Node References
    private Label _timeLabel;

    private Button _retryButton;
    private Button _nextLevelButton;
	
    // Level trackers
    [Export]private string CurrentLevel;
    [Export]private string NextLevel;
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Set external references
		_player = GetTree().Root.GetChild(-1).GetNode<Player.PlayerCharacter>("Player");
		_gameStateSignals = GetNode<GlobalScripts.GameStateSignals>("/root/GameStateSignals");
		_gameState = GetTree().Root.GetChild(-1).GetNode<GameStateTracker>("GameStateTracker");
		
		// Set internal references
		_timeLabel = GetNode<Label>("TimeLeftLabel");
		_retryButton = GetNode<Button>("FailScreen/VBoxContainer/Button");
		_nextLevelButton = GetNode<Button>("WinScreen/VBoxContainer/NextLevel");
		
		// Connect signals
		_retryButton.Pressed += () => 
		{
			GetTree().Paused = false;
			GetTree().ChangeSceneToFile(CurrentLevel);
		};
		_nextLevelButton.Pressed += () =>
		{
			GetTree().Paused = false;
			GetTree().ChangeSceneToFile(NextLevel);
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{ 
		_timeLabel.Text = _gameState._timer.TimeLeft.ToString().PadDecimals(2);
	}
	

	public void ShowFailScreen()
	{
		this.GetNode<CenterContainer>("FailScreen").Visible = true;
	}

	public void ShowWinScreen()
	{
		this.GetNode<CenterContainer>("WinScreen").Visible = true;
	}
}