using Godot;

namespace IsolationInterstellarGameJam.Mono.HUD;

public partial class HeadsUpDisplay : Control
{
	// External Node References
	private Player.PlayerCharacter _player;
	private GlobalScripts.GameStateSignals _gameStateSignals;
	private GameStateTracker _gameState;
	
	// Internal Node References
	private Label _speedLabel;
    private Label _timeLabel;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Set external references
		_player = GetTree().Root.GetChild(-1).GetNode<Player.PlayerCharacter>("Player");
		_gameStateSignals = GetNode<GlobalScripts.GameStateSignals>("/root/GameStateSignals");
		_gameState = GetTree().Root.GetChild(-1).GetNode<GameStateTracker>("GameStateTracker");
		
		// Set internal references
		_speedLabel = GetNode<Label>("SpeedLabel");
		_timeLabel = GetNode<Label>("TimeLeftLabel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{ 
		_speedLabel.Text = "Speed: " + (int)_player.LinearVelocity.Length();
		
		// _timeLabel.Text = Mathf.Snapped(_gameState._timer.TimeLeft,0.01).ToString();
		_timeLabel.Text = _gameState._timer.TimeLeft.ToString().PadDecimals(2);
	}
	

	public void ShowFailScreen()
	{
		this.GetNode<CenterContainer>("FailScreen").Visible = true;
	}

	public void ShowWinScreen()
	{
		
	}
}