using Godot;
using IsolationInterstellarGameJam.Mono.HUD;

namespace IsolationInterstellarGameJam.Mono.GlobalScripts;

public partial class JuiceEffects : Node
{
	// Node References
	private Player.PlayerCharacter _player;
	private HeadsUpDisplay _hud;
	
	public override void _Ready()
	{
		_player = GetTree().Root.GetChild(-1).GetNode<Player.PlayerCharacter>("Player");
		_hud = GetTree().Root.GetChild(-1).GetNode<HeadsUpDisplay>("HUD/HeadsUpDisplay");
	}
	
	
	private float _bulletTimeLeft;
	public void BulletTime(float time, float timeScale = 0.5f)
	{
		_bulletTimeLeft = time;
		Engine.TimeScale = timeScale;
	}

	public void VignetteImpact()
	{
		AnimationPlayer _vignetteAnimationPlayer = _hud.GetNode<AnimationPlayer>("AnimationPlayer");
		_vignetteAnimationPlayer.Play("VignetteImpact");
	}

	public override void _Process(double delta)
	{
		_bulletTimeLeft -= (float)delta;
		if (_bulletTimeLeft <= 0)
		{
			Engine.TimeScale = 1.0f;
		}
	}
}