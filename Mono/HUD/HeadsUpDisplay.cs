using Godot;

namespace IsolationInterstellarGameJam.Mono.HUD;

public partial class HeadsUpDisplay : Control
{
	// External Node References
	private Player.PlayerCharacter _player;
	private GlobalScripts.GameStateSignals _gameStateSignals;
	
	// Internal Node References
	private Label _speedLabel;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Set external references
		_player = GetTree().Root.GetChild(-1).GetNode<Player.PlayerCharacter>("Player");
		_gameStateSignals = GetNode<GlobalScripts.GameStateSignals>("/root/GameStateSignals");
		
		// Set internal references
		_speedLabel = GetNode<Label>("SpeedLabel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{ 
		_speedLabel.Text = "Speed: " + (int)_player.LinearVelocity.Length();
	}
}