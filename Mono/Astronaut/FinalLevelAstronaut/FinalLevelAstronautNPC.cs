using Godot;
using IsolationInterstellarGameJam.Mono.GlobalScripts;

namespace IsolationInterstellarGameJam.Mono.Astronaut.FinalLevelAstronaut;

public partial class FinalLevelAstronautNPC : RigidBody2D
{
	// Variables
	// States
	public bool IsConscious = true;
	
	// Asset references
	[Export] private Texture2D _consciousImage;
	[Export] private Texture2D _unconsciousImage;
	
	// Node references
	private Sprite2D _astronautSprite;
	private Player.PlayerCharacter _playerCharacter;
	private GlobalScripts.JuiceEffects _juice;
	private AudioStreamPlayer2D _impactAudio;
	private AnimationPlayer _animation;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Set Node references
		_astronautSprite = GetNode<Sprite2D>("Sprite2D");
		_playerCharacter = GetTree().Root.GetChild(-1).GetNode<Player.PlayerCharacter>("Player");
		_juice = GetTree().Root.GetChild(-1).GetNode<JuiceEffects>("JuiceEffects");
		_impactAudio = GetNode<AudioStreamPlayer2D>("ImpactAudio");
		_animation = GetTree().Root.GetChild(-1).GetNode<AnimationPlayer>("HUD/AnimationPlayer");
		
		// Connect events
		this.BodyEntered += OnCollide;
	}

	private void OnCollide(Node body)
	{
		if(!IsConscious) return;
		
		if (body is Player.PlayerCharacter playerCharacter)
		{
			float impactSpeed = playerCharacter.LastFrameSpeed;
			GD.Print("IMPACT SPEED: " + impactSpeed);
			if (impactSpeed >= 300f)
			{
				// Knock out
				KnockOut();
				
				// Slow down time for impact
				_juice.BulletTime(1f);
				
				// Toss a bit for fun :)
				ApplyCentralForce((playerCharacter.Position - this.Position) * -500f);
				
				// Play impact audio
				_impactAudio.Play();
				
				// FADE TO BLACK
				_animation.Play("FadeToBlack");
			}
		}
	}

	public void KnockOut()
	{
		IsConscious = false;
		_astronautSprite.Texture = _unconsciousImage;
	}
}