using Godot;

namespace IsolationInterstellarGameJam.Mono.AirlockHatch;

public partial class AirlockHatch : Area2D
{
	// Node References
	private AudioStreamPlayer2D _thuumpSound;
	private GlobalScripts.GameStateSignals _gameStateSignals;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Set node references
		_thuumpSound = GetNode<AudioStreamPlayer2D>("ThuumpAudio");
		_gameStateSignals = GetNode<GlobalScripts.GameStateSignals>("/root/GameStateSignals");
		
		this.BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Player.PlayerCharacter player)
		{
			player.Visible = false;
			_thuumpSound.Play();
		}

		if (body is Astronaut.AstronautBasic astronaut)
		{
			astronaut.QueueFree();
			_thuumpSound.Play();
			
			// Play success signal
			EmitSignal(GlobalScripts.GameStateSignals.SignalName.Victory);
		}
	}
}